using Microsoft.Data.Entity;

namespace GolfCourseManager.Models
{
	public class GCMDbContext : DbContext
    {
		public DbSet<Member> Members { get; set; }

		public bool AddMember(Member member)
		{
			return false;
		}

		GCMDbContext()
		{
		}
    }
}
