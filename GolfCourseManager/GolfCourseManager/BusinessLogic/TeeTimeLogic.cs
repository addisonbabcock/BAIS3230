using GolfCourseManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GolfCourseManager.BusinessLogic
{
    public class TeeTimeLogic
	{
		private GCMRepository _gcmRepo;

		public TeeTimeLogic(GCMRepository gcmRepo)
		{
			_gcmRepo = gcmRepo;
		}

		public bool IsTeeTimeReserved(DateTime time)
		{
			return _gcmRepo.GetReservedTeeTimeByStart(time) != null;
		}

		public bool IsValidTeeTimeStart(DateTime startTime)
		{
			var validTimes = GetValidTeeTimesForDate(startTime);
			var found = validTimes.Find(t => t.Ticks == startTime.Ticks);
			if (found == DateTime.MinValue)
			{
				return false;
			}
			return true;
		}

		public List<DateTime> GetAvailableTeeTimesForDate(DateTime date)
		{
			var reservedTeeTimes = GetReservedTeeTimesForDate(date);
			var validTeeTimes = GetValidTeeTimesForDate(date);
			var availableTeeTimes = new List<DateTime>();

			foreach (var valid in validTeeTimes)
			{
				if (reservedTeeTimes.Find(reserved => reserved.Start == valid) == null)
				{
					availableTeeTimes.Add(valid);
				}
			}

			return availableTeeTimes;
		}

		public List<DateTime> GetValidTeeTimesForDate(DateTime date)
		{
			var teeTimes = new List<DateTime>();
			var golfCourse = _gcmRepo.GetGolfCourse();
			DateTime open = GetOpeningTimeForDate(date);
			DateTime close = GetClosingTimeForDate(date);

			open = new DateTime(date.Year, date.Month, date.Day, open.Hour, open.Minute, open.Second);
			close = new DateTime(date.Year, date.Month, date.Day, close.Hour, close.Minute, close.Second);

			for (var teeTime = open; teeTime <= close; teeTime += golfCourse.TeeTimeInterval)
			{
				teeTimes.Add(teeTime);
			}

			return teeTimes;
		}

		public List<TeeTime> GetReservedTeeTimesForDate(DateTime date)
		{
			return _gcmRepo.GetReservedTeeTimesForDate(date);
		}

		public DateTime GetOpeningTimeForDate(DateTime date)
		{
			var golfCourse = _gcmRepo.GetGolfCourse();

			switch (date.DayOfWeek)
			{
				case DayOfWeek.Friday:
					return golfCourse.FridayOpen;

				case DayOfWeek.Monday:
					return golfCourse.MondayOpen;

				case DayOfWeek.Saturday:
					return golfCourse.SaturdayOpen;

				case DayOfWeek.Sunday:
					return golfCourse.SundayOpen;

				case DayOfWeek.Thursday:
					return golfCourse.ThursdayOpen;

				case DayOfWeek.Tuesday:
					return golfCourse.TuesdayOpen;

				case DayOfWeek.Wednesday:
					return golfCourse.WednesdayOpen;
			}

			return DateTime.Now;
		}

		public DateTime GetClosingTimeForDate(DateTime date)
		{
			var golfCourse = _gcmRepo.GetGolfCourse();

			switch (date.DayOfWeek)
			{
				case DayOfWeek.Friday:
					return golfCourse.FridayClose;

				case DayOfWeek.Monday:
					return golfCourse.MondayClose;

				case DayOfWeek.Saturday:
					return golfCourse.SaturdayClose;

				case DayOfWeek.Sunday:
					return golfCourse.SundayClose;

				case DayOfWeek.Thursday:
					return golfCourse.ThursdayClose;

				case DayOfWeek.Tuesday:
					return golfCourse.TuesdayClose;

				case DayOfWeek.Wednesday:
					return golfCourse.WednesdayClose;
			}

			return DateTime.Now;
		}
	}
}
