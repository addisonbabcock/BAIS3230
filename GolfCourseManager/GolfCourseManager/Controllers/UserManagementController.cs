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
		GCMDbContext _dbContext { get; set; }

		UserManagementController(GCMDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Register(Member member)
		{
			return View();
		}
    }
}
