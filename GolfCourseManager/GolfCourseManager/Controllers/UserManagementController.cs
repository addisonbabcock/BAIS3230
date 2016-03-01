using AutoMapper;
using GolfCourseManager.Models;
using GolfCourseManager.ViewModels;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GolfCourseManager.Controllers
{
    public class UserManagementController : Controller
    {
		private GCMRepository _gcmRepo { get; set; }

		public UserManagementController(GCMRepository gcmContext)
		{
			_gcmRepo = gcmContext;
		}

		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Register(MemberViewModel vm)
		{
			if (ModelState.IsValid)
			{
				var newMember = Mapper.Map<Member>(vm);

				_gcmRepo.AddMember(newMember);
				ViewBag.Message = "Success!";
			}
			return View();
		}
    }
}
