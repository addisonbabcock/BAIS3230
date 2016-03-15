using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GolfCourseManager.ViewModels
{
    public class ReservationCreatedViewModel
    {
		public List<DateTime> Reservations { get; set; } = new List<DateTime>();
		public List<DateTime> Failures { get; set; } = new List<DateTime>();
		public string FailureReason { get; set; }
    }
}
