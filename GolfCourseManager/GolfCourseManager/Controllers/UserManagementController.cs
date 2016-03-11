using AutoMapper;
using GolfCourseManager.Models;
using GolfCourseManager.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GolfCourseManager.Controllers
{
    public class UserManagementController : Controller
    {
		private SignInManager<Member> _signInManager;

		private GCMRepository _gcmRepo { get; set; }

		public UserManagementController(GCMRepository gcmContext, SignInManager<Member> signInManager)
		{
			_gcmRepo = gcmContext;
			_signInManager = signInManager;
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

		public IActionResult Login()
		{
			if (User.Identity.IsAuthenticated)
			{
				return RedirectToAction("Index", "Home");
			}

			return View();
		}

		[HttpPost]
		public async Task<ActionResult> Login(LoginViewModel loginVM, string returnUrl)
		{
			if (ModelState.IsValid)
			{
				var signInResult = await _signInManager.PasswordSignInAsync(
					loginVM.Username,
					loginVM.Password,
					true, false);

				if (signInResult.Succeeded)
				{
					if (string.IsNullOrWhiteSpace(returnUrl))
					{
						return RedirectToAction("Index", "Home");
					}
					else
					{
						return Redirect(returnUrl);
					}
				}
				else
				{
					ModelState.AddModelError(String.Empty, "Invalid username or password");
				}
			}

			return View();
		}

		public async Task<ActionResult> Logout()
		{
			if (User.Identity.IsAuthenticated)
			{
				await _signInManager.SignOutAsync();
			}

			return RedirectToAction("Index", "Home");
		}
	}
}
