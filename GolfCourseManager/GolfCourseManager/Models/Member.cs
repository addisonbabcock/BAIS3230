using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GolfCourseManager.Models
{
	public class Member
	{
		[Key]
		public int Id { get; set; }
		public int GolfCourseId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }

		public enum MemberStatus { Active, Unpaid, Applied, Inactive, Closed }
		public MemberStatus Status { get; set; } = MemberStatus.Applied;

		public string Address1 { get; set; }
		public string Address2 { get; set; } = String.Empty;
		public string Address3 { get; set; } = String.Empty;
		public string City { get; set; }
		public string Province { get; set; }
		public string PostalCode { get; set; }

		public GolfCourse GolfCourse { get; set; }

		public string GetFullName()
		{
			return LastName + ", " + FirstName;
		}
    }
}
