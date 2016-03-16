using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using GolfCourseManager.BusinessLogic;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GolfCourseManager.Models
{
    public class GCMRepository
    {
		public GCMRepository(GCMContext context, UserManager<Member> userManager, RoleManager<IdentityRole> roleManager)
		{
			_context = context;
			_userManager = userManager;
			_roleManager = roleManager;
		}

		private GCMContext _context;
		private UserManager<Member> _userManager;
		private RoleManager<IdentityRole> _roleManager;

		public GolfCourse GetGolfCourse()
		{
			return _context.GolfCourses.FirstOrDefault();
		}

		public async Task<bool> IsAdminAsync(Member member)
		{
			var roles = await _userManager.GetRolesAsync(member);
			if (roles.Contains("admin"))
			{
				return true;
			}

			return false;
		}

		public async Task<bool> IsUserAsync(Member member)
		{
			var roles = await _userManager.GetRolesAsync(member);
			if (roles.Contains("user"))
			{
				return true;
			}

			return false;
		}

		public async Task<Member> GetLoggedInMemberAsync(ClaimsPrincipal user)
		{
			if (user.Identity.IsAuthenticated)
			{
				var member = await _userManager.FindByNameAsync(user.Identity.Name);
				return member;
			}
			else
			{
				return null;
			}
		}

		public List<TeeTime> GetReservedTeeTimesForDate(DateTime date)
		{
			return _context.TeeTimes.Where(t => t.Start.Date == date.Date).OrderBy(t => t.Start).ToList();
		}

		public TeeTime GetReservedTeeTimeByStart(DateTime start)
		{
			return _context.TeeTimes.Where(t => t.Start == start).FirstOrDefault();
		}

		public bool ReserveTeeTime(TeeTime teeTime)
		{
			var logic = new TeeTimeLogic(this);
			bool validTime = logic.IsValidTeeTimeStart(teeTime.Start);

			if (!validTime)
			{
				return false;
			}

			bool alreadyReserved = logic.IsTeeTimeReserved(teeTime.Start);

			if (alreadyReserved)
			{
				return false;
			}

			_context.TeeTimes.Add(teeTime);
			return _context.SaveChanges() != 0;
		}
	}
}
