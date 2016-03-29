using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GolfCourseManager.ViewModels
{
    public class DailyTeeTimesViewModel
    {
		public DateTime Date { get; set; }
		public List<ReserveViewModel> Reservations { get; set; }
    }
}
