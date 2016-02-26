using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GolfCourseManager.Models
{
    public class Score
    {
		[Key]
		public int Id { get; set; }
		public int GolfCourseId { get; set; }
		public int HoleNumber { get; set; }
		public int MemberId { get; set; }
		public int TeeTimeId { get; set; }
		public string PlayerName { get; set; }
		public int Strokes { get; set; }

		public GolfCourse GolfCourse { get; set; }
		public Hole Hole { get; set; }
		public Member Member { get; set; }
		public TeeTime TeeTime { get; set; }
    }
}
