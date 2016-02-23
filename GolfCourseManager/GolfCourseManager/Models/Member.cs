using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GolfCourseManager.Models
{
    public class Member
    {
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		
		public enum MemberStatus { Active, Unpaid, Applied, Inactive, Closed }
		public MemberStatus Status { get; set; }

		public string GetFullName()
		{
			return LastName + ", " + FirstName;
		}
    }
}
