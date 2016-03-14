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
		public IActionResult SelectDate(SelectDateViewModel vm)
		{
			//Second the user selects a time and enters the party information
			//This will be done on the reserve view
			return RedirectToAction("Reserve", vm);
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
		public async Task<IActionResult> Reserve(ReserveViewModel rvm)
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
					//redirect to success page
				}

				ModelState.AddModelError("StartTime", "Start Time already reserved.");
				return RedirectToAction("Reserve", new SelectDateViewModel(rvm.SelectedDate));
			}

			//do something..?
			return RedirectToAction("Reserve", new SelectDateViewModel(rvm.SelectedDate));
		}
    }
}
