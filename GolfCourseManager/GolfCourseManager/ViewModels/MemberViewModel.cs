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
    }
}
