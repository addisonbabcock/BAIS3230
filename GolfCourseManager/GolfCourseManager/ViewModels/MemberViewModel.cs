using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GolfCourseManager.ViewModels
{
    public class MemberViewModel
    {
		[Required]
		[StringLength(255, MinimumLength =5)]
		public string FirstName { get; set; }
		[Required]
		[StringLength(255, MinimumLength =5)]
		public string LastName { get; set; }
		[Required]
		[EmailAddress]
		public string Email { get; set; }

		[Required]
		[StringLength(255, MinimumLength = 5)]
		public string Address1 { get; set; }
		public string Address2 { get; set; }
		public string Address3 { get; set; }
		[Required]
		[StringLength(255, MinimumLength = 3)]
		public string City { get; set; }
		[Required]
		[StringLength(255, MinimumLength = 2)]
		public string Country { get; set; }
		[Required]
		[StringLength(255, MinimumLength = 2)]
		public string Province { get; set; }
		[Required]
		[StringLength(7, MinimumLength = 7)]
		public string PostalCode { get; set; }
    }
}
