using GolfCourseManager.Models;
using GolfCourseManager.ViewModels;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GolfCourseManager.BusinessLogic;

namespace GolfCourseManager.Controllers
{
    public class TeeTimeController : Controller
    {
		private GCMRepository _gcmRepo { get; set; }

		public TeeTimeController(GCMRepository gcmRepo)
		{
			_gcmRepo = gcmRepo;
		}

		[Authorize]
		[HttpGet]
		public IActionResult SelectDate()
		{
			//First the user selects a date
			return View();
		}

		[Authorize]
		[HttpPost]
		public IActionResult SelectDate(SelectDateViewModel sdvm)
		{
			//Second the user selects a time and enters the party information
			//This will be done on the reserve view
			return RedirectToAction("Reserve", sdvm);
		}

		[Authorize]
		[HttpGet]
		public IActionResult Reserve(SelectDateViewModel sdvm)
		{
			var rivm = new ReserveInputViewModel();
			var logic = new TeeTimeLogic(_gcmRepo);
			var availableTeeTimes = logic.GetAvailableTeeTimesForDate(sdvm.Date.Date);

			rivm.AvailableTeeTimes = availableTeeTimes;
			rivm.SelectedDate = sdvm.Date;
			ViewBag.InputViewModel = rivm;
			return View();
		}
		
		[Authorize]
		[HttpPost]
		public async Task<IActionResult> CreateReservation(ReserveViewModel rvm)
		{
			//Third the reservation is created.
			if (ModelState.IsValid)
			{
				var teeTime = Mapper.Map<TeeTime>(rvm);
				teeTime.GolfCourse = _gcmRepo.GetGolfCourse();
				teeTime.Member = await _gcmRepo.GetLoggedInMemberAsync(User);
				teeTime.Start = new DateTime(
					rvm.SelectedDate.Year,
					rvm.SelectedDate.Month,
					rvm.SelectedDate.Day,
					rvm.StartTime.Hour,
					rvm.StartTime.Minute,
					rvm.StartTime.Second);

				var success = _gcmRepo.ReserveTeeTime(teeTime);

				if (success)
				{
					//show success page
					var rcvm = new ReservationCreatedViewModel();
					rcvm.Reservations = new List<DateTime>()
					{
						new DateTime(teeTime.Start.Ticks)
					};

					return View(rcvm);
				}

				ModelState.AddModelError("StartTime", "Start Time already reserved.");
				return RedirectToAction("Reserve", new SelectDateViewModel(rvm.SelectedDate));
			}

			//do something..?
			return RedirectToAction("Reserve", new SelectDateViewModel(rvm.SelectedDate));
		}

		[Authorize]
		[HttpGet]
		public IActionResult StandingReserve()
		{
			var logic = new TeeTimeLogic(_gcmRepo);
			var rivm = new ReserveInputViewModel();
			rivm.AvailableTeeTimes = logic.GetValidTeeTimesForDate(DateTime.Now);
			ViewBag.InputViewModel = rivm;

			return View();
		}
		
		[Authorize]
		[HttpPost]
		public async Task<IActionResult> CreateStandingReservation(StandingReservationViewModel srvm)
		{
			var rcvm = new ReservationCreatedViewModel();

			if (srvm.StartDate < DateTime.Now)
			{
				rcvm.FailureReason = "Start Date cannot be in the past.";
				return View(rcvm);
			}

			if (srvm.EndDate < DateTime.Now)
			{
				rcvm.FailureReason = "End Date cannot be in the past.";
				return View(rcvm);
			}

			if (srvm.EndDate <= srvm.StartDate)
			{
				rcvm.FailureReason = "End Date must be after Start Date.";
				return View(rcvm);
			}

			for (var current = srvm.StartDate; current <= srvm.EndDate; current += TimeSpan.FromDays(7))
			{
				var teeTime = Mapper.Map<TeeTime>(srvm);
				teeTime.GolfCourse = _gcmRepo.GetGolfCourse();
				teeTime.Member = await _gcmRepo.GetLoggedInMemberAsync(User);
				teeTime.Start = new DateTime(
					current.Year,
					current.Month,
					current.Day,
					srvm.StartTime.Hour,
					srvm.StartTime.Minute,
					srvm.StartTime.Second);

				var success = _gcmRepo.ReserveTeeTime(teeTime);

				if (success)
				{
					rcvm.Reservations.Add(teeTime.Start);
				}
				else
				{
					rcvm.Failures.Add(teeTime.Start);
				}
			}

			return View(rcvm);
		}
	}
}
