using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GolfCourseManager.ViewModels
{
    public class ReserveInputViewModel
    {
		public List<DateTime> AvailableTeeTimes { get; set; }
		public DateTime SelectedDate { get; set; }
	}
}
