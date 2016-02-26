﻿using System;
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

		public string Address1 { get; set; }
		public string Address2 { get; set; }
		public string Address3 { get; set; }
		public string City { get; set; }
		public string Province { get; set; }
		public string PostalCode { get; set; }

		public string GetFullName()
		{
			return LastName + ", " + FirstName;
		}
    }
}