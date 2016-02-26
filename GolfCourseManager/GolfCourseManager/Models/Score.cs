using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GolfCourseManager.Models
{
    public class Score
    {
		public int GolfCourseId { get; set; }
		public int HoleNumber { get; set; }
		public int MemberId { get; set; }
		public string PlayerName { get; set; }
		public int Strokes { get; set; }
    }
}
