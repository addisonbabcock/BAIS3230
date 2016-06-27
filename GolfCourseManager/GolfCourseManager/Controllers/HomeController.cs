using GolfCourseManager.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

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

			if (member != null && await _gcmRepo.IsAdminAsync(member))
			{
				ViewBag.isAdmin = true;
			}
			else
			{
				ViewBag.isAdmin = false;
			}

			return View();
		}
    }
}
