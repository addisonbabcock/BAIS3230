using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GolfCourseManager.Models
{
    public class GCMContextSeedData
    {
		private GCMContext _context;

		public GCMContextSeedData(GCMContext context)
		{
			_context = context;
		}

		public void EnsureSeedData()
		{
			if (_context.GolfCourses.Any())
			{
				//seed data
				var clubBaist = new GolfCourse()
				{
					Name = "Club BAIST",
					TeeTimeInterval = new TimeSpan(hours: 0, minutes: 7, seconds: 30),
					MondayOpen = new DateTime(year: 0, month: 0, day: 0, hour: 10, minute: 0, second: 0),
					MondayClose = new DateTime(year:0, month: 0, day: 0, hour: 20, minute: 0, second: 0),
				};

				clubBaist.TuesdayOpen = clubBaist.WednesdayOpen = clubBaist.ThursdayOpen = clubBaist.FridayOpen = clubBaist.SaturdayOpen = clubBaist.SundayOpen = clubBaist.MondayOpen;
				clubBaist.TuesdayClose = clubBaist.WednesdayClose = clubBaist.ThursdayClose = clubBaist.FridayClose = clubBaist.SaturdayClose = clubBaist.SundayClose = clubBaist.MondayClose;

				clubBaist.Holes = new List<Hole>()
				{
					new Hole()
					{
						HoleNumber = 1,
						Par = 3,
						YardsWhite = 300,
						YardsBlue = 310,
						YardsRed = 320
					}

				};

				_context.GolfCourses.Add(clubBaist);
				_context.Holes.AddRange(clubBaist.Holes);

				_context.SaveChanges();
			}
		}
    }
}
