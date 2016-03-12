using System.ComponentModel.DataAnnotations;

namespace GolfCourseManager.Models
{
	public class Hole
    {
		[Key]
		public int Id { get; set; }
		public int GolfCourseId { get; set; }
		public int HoleNumber { get; set; }
		public int Par { get; set; }
		public int YardsWhite { get; set; }
		public int YardsRed { get; set; }
		public int YardsBlue { get; set; }

		public GolfCourse GolfCourse { get; set; }
    }
}
