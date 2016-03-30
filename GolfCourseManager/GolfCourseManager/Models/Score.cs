using System.ComponentModel.DataAnnotations;

namespace GolfCourseManager.Models
{
	public class Score
    {
		[Key]
		public int Id { get; set; }
		public string PlayerName { get; set; }
		public int Strokes { get; set; }

		public GolfCourse GolfCourse { get; set; }
		public Hole Hole { get; set; }
		public Member Member { get; set; }
		public TeeTime TeeTime { get; set; }
    }
}
