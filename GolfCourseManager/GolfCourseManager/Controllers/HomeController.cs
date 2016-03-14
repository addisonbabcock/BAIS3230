using GolfCourseManager.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GolfCourseManager.Controllers
{
    public class HomeController : Controller
    {
		private GCMRepository _gcmRepo;

		public HomeController(GCMRepository gcmRepo)
		{
			_gcmRepo = gcmRepo;
		}

		public async Task<IActionResult> Index()
		{
			var member = await _gcmRepo.GetLoggedInMemberAsync(User);

			if (member != null)
			{
				ViewBag.memberName = member.GetFullName();
			}

			return View();
		}
    }
}
