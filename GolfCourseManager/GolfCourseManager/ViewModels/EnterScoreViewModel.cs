using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GolfCourseManager.ViewModels
{
	public class EnterScoreViewModel
    {
		//Hidden input
		public List<DateTime> SelectableTeeTimes { get; set; }

		[Required]
		public DateTime TeeTime { get; set; }

		//TODO: Find a better way
		public int Hole1 { get; set; }
		public int Hole2 { get; set; }
		public int Hole3 { get; set; }
		public int Hole4 { get; set; }
		public int Hole5 { get; set; }
		public int Hole6 { get; set; }
		public int Hole7 { get; set; }
		public int Hole8 { get; set; }
		public int Hole9 { get; set; }
		public int Hole10 { get; set; }
		public int Hole11 { get; set; }
		public int Hole12 { get; set; }
		public int Hole13 { get; set; }
		public int Hole14 { get; set; }
		public int Hole15 { get; set; }
		public int Hole16 { get; set; }
		public int Hole17 { get; set; }
		public int Hole18 { get; set; }
	}
}
