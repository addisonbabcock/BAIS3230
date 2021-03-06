﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GolfCourseManager.ViewModels
{
    public class ScoresIndexViewModel
    {
		public class Row
		{
			public string MemberName { get; set; }
			public DateTime StartTime { get; set; }
			public int Strokes { get; set; }
			public int TeeTimeId { get; set; }
		}

		public List<Row> Results { get; set; }
		public int GolfCourseId { get; set; }
	}
}
