using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GolfCourseManager.Models
{
    public class GCMContextSeedData
    {
		private GCMContext _context;
		private UserManager<Member> _userManager;

		public GCMContextSeedData(GCMContext context, UserManager<Member> userManager)
		{
			_context = context;
			_userManager = userManager;
		}

		public async Task EnsureSeedData()
		{
			if (!_context.GolfCourses.Any())
			{
				//seed data
				var clubBaist = new GolfCourse()
				{
					Name = "Club BAIST",
					TeeTimeInterval = new TimeSpan(hours: 0, minutes: 7, seconds: 30),
					MondayOpen = new DateTime(year: 1900, month: 1, day: 1, hour: 10, minute: 0, second: 0),
					MondayClose = new DateTime(year:1900, month: 1, day: 1, hour: 20, minute: 0, second: 0),
				};

				clubBaist.TuesdayOpen = clubBaist.WednesdayOpen = clubBaist.ThursdayOpen = clubBaist.FridayOpen = clubBaist.SaturdayOpen = clubBaist.SundayOpen = clubBaist.MondayOpen;
				clubBaist.TuesdayClose = clubBaist.WednesdayClose = clubBaist.ThursdayClose = clubBaist.FridayClose = clubBaist.SaturdayClose = clubBaist.SundayClose = clubBaist.MondayClose;

				clubBaist.Holes = new List<Hole>()
				{
					new Hole()
					{
						HoleNumber = 1,
						Par = 3,
						YardsWhite = 300,
						YardsBlue = 310,
						YardsRed = 320
					},
					new Hole()
					{
						HoleNumber = 2,
						Par = 4,
						YardsWhite = 350,
						YardsBlue = 360,
						YardsRed = 370
					}
				};

				_context.GolfCourses.Add(clubBaist);
				_context.Holes.AddRange(clubBaist.Holes);

				_context.SaveChanges();

				if (await _userManager.FindByEmailAsync("test.email@hotmail.com") == null)
				{
					var member = new Member()
					{
						UserName = "testlogin",
						Email = "test.email@hotmail.com",
						GolfCourse = clubBaist,
					};

					var result = await _userManager.CreateAsync(member, "P@ssword1");       //yup...
					var waithere = true;
				}
			}
		}
	}
}
