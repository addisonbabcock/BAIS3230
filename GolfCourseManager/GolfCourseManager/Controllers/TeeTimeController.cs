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
		public IActionResult Reserve(SelectDateViewModel vm)
		{
			//Second the user selects a time and enters the party information
			var logic = new TeeTimeLogic(_gcmRepo);
			var availableTeeTimes = logic.GetAvailableTeeTimesForDate(vm.Date.Date);

			return View(availableTeeTimes);
		}

		[Authorize]
		[HttpPost]
		public async Task<IActionResult> Reserve(ReserveViewModel vm)
		{
			//Third the reservation is created.
			if (ModelState.IsValid)
			{
				var teeTime = Mapper.Map<TeeTime>(vm);
				teeTime.GolfCourse = _gcmRepo.GetGolfCourse();
				teeTime.Member = await _gcmRepo.GetLoggedInMemberAsync(User);
			}

			return View();
		}
    }
}
