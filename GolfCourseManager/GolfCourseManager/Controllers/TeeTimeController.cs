using GolfCourseManager.Models;
using GolfCourseManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using GolfCourseManager.BusinessLogic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace GolfCourseManager.Controllers
{
    public class TeeTimeController : Controller
    {
		private UserManager<Member> _userManager;

		private GCMRepository _gcmRepo { get; set; }

		public TeeTimeController(GCMRepository gcmRepo, UserManager<Member> userManager)
		{
			_gcmRepo = gcmRepo;
			_userManager = userManager;
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

		[Authorize]
		[HttpGet]
		public async Task<IActionResult> TeeTimes(DateTime date)
		{
			var vm = new DailyTeeTimesViewModel();
			if (date == DateTime.MinValue)
			{
				vm.Date = DateTime.Today;
			}
			else
			{
				vm.Date = date;
			}

			List<TeeTime> reservations;
			if (User.IsInRole("admin"))
				reservations = _gcmRepo.GetReservedTeeTimesForDate(vm.Date);
			else
				reservations = _gcmRepo.GetReservedTeeTimesForMember(await _gcmRepo.GetLoggedInMemberAsync(User));

			vm.Reservations = new List<ReserveViewModel>();
			foreach (var reservation in reservations)
			{
				var reserveVM = new ReserveViewModel()
				{
					Player1Name = reservation.Player1Name,
					Player2Name = reservation.Player2Name,
					Player3Name = reservation.Player3Name,
					Player4Name = reservation.Player4Name,
					StartTime = reservation.Start,
					Id = reservation.Id,
					SelectedDate = reservation.Start.Date
				};
				vm.Reservations.Add(reserveVM);
			}

			return View(vm);
		}

		[Authorize]
		[HttpGet]
		public IActionResult Update(int id)
		{
			var teeTime = _gcmRepo.GetTeeTime(id);

			if (teeTime == null)
			{
				ModelState.AddModelError("", "Could not find reserved tee time.");
				return View(null);
			}

			var vm = Mapper.Map<ReserveViewModel>(teeTime);
			vm.StartTime = DateTime.MinValue.Add(teeTime.Start.TimeOfDay);
			vm.SelectedDate = teeTime.Start.Date;
			vm.MemberId = teeTime.Member.Id;

			return View(vm);
		}

		[Authorize]
		[HttpPost]
		public async Task<IActionResult> Update(ReserveViewModel vm)
		{
			var teeTime = Mapper.Map<TeeTime>(vm);
			teeTime.Start = vm.SelectedDate.Add(vm.StartTime.TimeOfDay);
			teeTime.GolfCourse = _gcmRepo.GetGolfCourse();
			teeTime.Member = await _userManager.FindByIdAsync(vm.MemberId);

			_gcmRepo.UpdateTeeTime(teeTime);

			return RedirectToAction("TeeTimes", DateTime.MinValue);
		}
	}
}
