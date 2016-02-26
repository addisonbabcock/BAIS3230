using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GolfCourseManager.Models
{
    public class TeeTime
    {
		public int MemberId { get; set; }
		public List<string> PlayerNames { get; set; }
		public DateTime Start { get; set; }
    }
}
