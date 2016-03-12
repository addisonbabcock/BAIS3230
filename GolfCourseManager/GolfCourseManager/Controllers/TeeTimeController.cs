using GolfCourseManager.Models;
using GolfCourseManager.ViewModels;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

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
		public IActionResult Reserve()
		{
			return View();
		}

		[Authorize]
		[HttpPost]
		public IActionResult Reserve(ReserveViewModel vm)
		{
			if (ModelState.IsValid)
			{
				var teeTime = Mapper.Map<TeeTime>(vm);
				teeTime.GolfCourse = _gcmRepo.GetGolfCourse();
			}

			return View();
		}
    }
}
