using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GolfCourseManager.ViewModels
{
    public class ReserveViewModel
    {
		[Required]
		public string Player1Name { get; set; }
		public string Player2Name { get; set; }
		public string Player3Name { get; set; }
		public string Player4Name { get; set; }
		[Required]
		public DateTime StartTime { get; set; }

		public DateTime SelectedDate { get; set; }		//hidden
		public int Id { get; set; }
		public string MemberId { get; set; }
	}
}
