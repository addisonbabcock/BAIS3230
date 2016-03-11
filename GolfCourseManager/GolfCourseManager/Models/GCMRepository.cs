using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GolfCourseManager.Models
{
    public class GCMRepository
    {
		public GCMRepository(GCMContext context)
		{
			_context = context;
		}

		private GCMContext _context;

		public GolfCourse GetGolfCourse()
		{
			return _context.GolfCourses.FirstOrDefault();		//TODO: Figure out how to specify...
		}

//		public IEnumerable<TeeTime> GetTeeTimesForMember(int Id)
//		{
//			return _context.TeeTimes.Where(t => t.MemberId == Id).OrderBy(t => t.Start);
//		}

		public void AddMember(Member member)
		{
			member.GolfCourse = GetGolfCourse();
			_context.Members.Add(member);
			_context.SaveChanges();
		}
    }
}
