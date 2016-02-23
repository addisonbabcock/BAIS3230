using GolfCourseManager.Models;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GolfCourseManager.Controllers
{
    public class UserManagementController : Controller
    {
		private GCMContext _gcmContext { get; set; }

		public UserManagementController(GCMContext gcmContext)
		{
			_gcmContext = gcmContext;
		}

		public IActionResult Register()
		{
			var members = _gcmContext.Members.OrderBy(m => m.GetFullName()).ToList();
			return View();
		}

		[HttpPost]
		public IActionResult Register(Member member)
		{
			return View();
		}
    }
}
