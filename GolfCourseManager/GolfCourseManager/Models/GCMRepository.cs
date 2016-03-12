using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;

namespace GolfCourseManager.Models
{
    public class GCMRepository
    {
		public GCMRepository(GCMContext context, UserManager<Member> userManager)
		{
			_context = context;
			_userManager = userManager;
		}

		private GCMContext _context;
		private UserManager<Member> _userManager;

		public GolfCourse GetGolfCourse()
		{
			return _context.GolfCourses.FirstOrDefault();		//TODO: Figure out how to specify...
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
	}
}
