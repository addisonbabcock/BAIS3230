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

		public IEnumerable<TeeTime> GetTeeTimesForMember(int Id)
		{
			return _context.TeeTimes.Where(t => t.MemberId == Id).OrderBy(t => t.Start);
		}
    }
}
