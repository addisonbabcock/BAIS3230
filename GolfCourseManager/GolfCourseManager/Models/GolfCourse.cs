using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GolfCourseManager.Models
{
	public class GolfCourse
    {
		[Key]
		public int Id { get; set; }
		public string Name { get; set; }
		public TimeSpan TeeTimeInterval { get; set; }

		public DateTime MondayOpen { get; set; }
		public DateTime MondayClose { get; set; }
		public DateTime TuesdayOpen { get; set; }
		public DateTime TuesdayClose { get; set; }
		public DateTime WednesdayOpen { get; set; }
		public DateTime WednesdayClose { get; set; }
		public DateTime ThursdayOpen { get; set; }
		public DateTime ThursdayClose { get; set; }
		public DateTime FridayOpen { get; set; }
		public DateTime FridayClose { get; set; }
		public DateTime SaturdayOpen { get; set; }
		public DateTime SaturdayClose { get; set; }
		public DateTime SundayOpen { get; set; }
		public DateTime SundayClose { get; set; }

		public ICollection<Hole> Holes { get; set; }
		public ICollection<Member> Members { get; set; }
    }
}
