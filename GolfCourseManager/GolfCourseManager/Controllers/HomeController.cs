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
		private SignInManager<Member> _signInManager;
		private UserManager<Member> _userManager;

		public HomeController(SignInManager<Member> signinManager, UserManager<Member> userManager)
		{
			_signInManager = signinManager;
			_userManager = userManager;
		}

		public async Task<IActionResult> Index()
		{
			if (User.Identity.IsAuthenticated)
			{
				var member = await _userManager.FindByNameAsync(User.Identity.Name);
				ViewBag.memberName = member.GetFullName();
			}

			return View();
		}
    }
}
