using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using GolfCourseManager.BusinessLogic;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;

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
			return _context.TeeTimes
				.Where(t => t.Start.Date == date.Date)
				.OrderBy(t => t.Start)
				.ToList();
		}

		public TeeTime GetTeeTime(int teeTimeId)
		{
			return _context.TeeTimes
				.Where(teeTime => teeTime.Id == teeTimeId)
				.FirstOrDefault();
		}

		public TeeTime GetReservedTeeTimeByStart(DateTime start)
		{
			return _context.TeeTimes
				.Where(t => t.Start == start)
				.FirstOrDefault();
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

		public List<TeeTime> GetTeeTimesWithoutScore(Member member)
		{
			return _context.TeeTimes
				.Where(teeTime => teeTime.Member.Id == member.Id)
				.Where(teeTime =>
					_context.Scores.Where(score => score.TeeTime.Start == teeTime.Start).Count() == 0)
				.OrderBy(teeTime => teeTime.Start)
				.ToList();
		}

		public List<TeeTime> GetTeeTimesWithScore(Member member)
		{
			return _context.TeeTimes
				.Where(teeTime => teeTime.Member.Id == member.Id)
				.Where(teeTime =>
					_context.Scores.Where(score => score.TeeTime.Start == teeTime.Start).Count() != 0)
				.OrderBy(teeTime => teeTime.Start)
				.ToList();
		}

		public List<Score> GetScoresForTeeTime(TeeTime teeTime)
		{
			return GetScoresForTeeTime(GetGolfCourse().Id, teeTime);
		}

		public List<Score> GetScoresForTeeTime(int golfCourseId, TeeTime teeTime)
		{
			var scores = _context.Scores
				.Include(score => score.Hole);

			return scores
				.Where(score => score.GolfCourse.Id == golfCourseId)
				.Where(score => score.TeeTime.Start == teeTime.Start)
			//	.OrderBy(score => score.Hole.HoleNumber)
				.ToList();
		}

		public TeeTime GetTeeTime(DateTime startTime)
		{
			return GetTeeTime(GetGolfCourse().Id, startTime);
		}

		public TeeTime GetTeeTime(int golfCourseId, DateTime startTime)
		{
			return _context.TeeTimes
				.Where(teeTime => teeTime.GolfCourse.Id == golfCourseId)
				.Where(teeTime => teeTime.Start == startTime)
				.FirstOrDefault();
		}

		public void AddScore(Score score)
		{
			_context.Scores.Add(score);
		}

		public async Task<Member> GetMemberFromUserAsync(ClaimsPrincipal user)
		{
			return await _userManager.FindByNameAsync(user.Identity.Name);
		}

		public async Task SaveScoresAsync()
		{
			await _context.SaveChangesAsync();
		}

		public Hole GetHole(GolfCourse golfCourse, int holeNumber)
		{
			return _context.Holes
				.Where(hole => hole.GolfCourse.Id == golfCourse.Id)
				.Where(hole => hole.HoleNumber == holeNumber)
				.FirstOrDefault();
		}
	}
}
