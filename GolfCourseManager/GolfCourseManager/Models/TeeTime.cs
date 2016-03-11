using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GolfCourseManager.Models
{
    public class TeeTime
    {
		[Key]
		public int Id { get; set; }
//		public int MemberId { get; set; }
//		public int GolfCourseId { get; set; }
		[Required]
		public string Player1Name { get; set; }
		public string Player2Name { get; set; }
		public string Player3Name { get; set; }
		public string Player4Name { get; set; }
		[Required]
		public DateTime Start { get; set; }

		public Member Member { get; set; }
		public GolfCourse GolfCourse { get; set; }
    }
}
