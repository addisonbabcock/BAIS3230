using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GolfCourseManager.ViewModels
{
	public class SelectDateViewModel
	{
		[Required]
		public DateTime Date { get; set; }

		public SelectDateViewModel()
		{
		}

		public SelectDateViewModel(DateTime date)
		{
			Date = date;
		}
    }
}
