using AutoMapper;
using GolfCourseManager.Models;
using GolfCourseManager.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace GolfCourseManager.Controllers
{
    public class UserManagementController : Controller
    {
		private SignInManager<Member> _signInManager;
		private UserManager<Member> _userManager;

		private GCMRepository _gcmRepo { get; set; }

		public UserManagementController(GCMRepository gcmContext, SignInManager<Member> signInManager, UserManager<Member> userManager)
		{
			_gcmRepo = gcmContext;
			_signInManager = signInManager;
			_userManager = userManager;
		}

		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Register(MemberViewModel vm)
		{
			if (vm.Password != vm.PasswordConfirm)
			{
				ModelState.AddModelError("PasswordConfirm", "Passwords must match.");
			}

			if (ModelState.IsValid)
			{
				var newMember = Mapper.Map<Member>(vm);
				newMember.GolfCourse = _gcmRepo.GetGolfCourse();

				var result = await _userManager.CreateAsync(newMember, vm.Password);

				if (result.Succeeded)
				{
					//don't try to sign in when an admin is registering a new user
					if (User.Identity.IsAuthenticated && User.IsInRole("admin"))
					{
						return RedirectToAction("Index", "Home");
					}

					var signInResult = await _signInManager.PasswordSignInAsync(newMember.UserName, vm.Password, true, false);

					if (signInResult.Succeeded)
					{
						return RedirectToAction("Index", "Home");
					}
					else
					{
						return RedirectToAction("Login", "UserManagement");
					}
				}
				else
				{
					ModelState.AddModelError("", result.Errors.FirstOrDefault().Description);
				}
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

		[Authorize]
		[HttpGet]
		public async Task<IActionResult> ViewMember(Member member)
		{
			string email;

			if (member == null || string.IsNullOrEmpty(member.Email))
			{
				//TODO: Figure out how to default to the current user.
				email = "admin@gcm.com";
			}
			else
			{
				email = member.Email;
			}

			member = await _userManager.FindByEmailAsync(email);
			
			var memberVM = Mapper.Map<MemberViewModel>(member);

			return View(memberVM);
		}

		[Authorize]
		[HttpPost]
		public async Task<IActionResult> UpdateMember(MemberViewModel vm)
		{
			var dbMember = await _userManager.FindByEmailAsync(vm.Email);
			dbMember.Address1 = vm.Address1;
			dbMember.Address2 = vm.Address2;
			dbMember.Address3 = vm.Address3;
			dbMember.City = vm.City;
			dbMember.FirstName = vm.FirstName;
			dbMember.LastName = vm.LastName;	
			dbMember.PostalCode = vm.PostalCode;
			dbMember.Province = vm.Province;

			var result = await _userManager.UpdateAsync(dbMember);
			var resultsVM = new MemberUpdateResultsViewModel()
			{
				Success = result.Succeeded,
				Name = dbMember.NormalizedUserName
			};

			return View(resultsVM);
		}
	}
}
