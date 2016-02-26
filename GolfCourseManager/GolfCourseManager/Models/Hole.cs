using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GolfCourseManager.Models
{
    public class Hole
    {
		public int HoleNumber { get; set; }
		public int Par { get; set; }
		public int YardsWhite { get; set; }
		public int YardsRed { get; set; }
		public int YardsBlue { get; set; }
    }
}
