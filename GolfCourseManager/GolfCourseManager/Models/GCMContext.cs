using Microsoft.Data.Entity;

namespace GolfCourseManager.Models
{
	public class GCMContext : DbContext
    {
		public DbSet<Member> Members { get; set; }

		public bool AddMember(Member member)
		{
			return false;
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			var connectionString = Startup.Configuration["Data:GCMConnection:ConnectionString"];
			optionsBuilder.UseSqlServer(connectionString);

			base.OnConfiguring(optionsBuilder);
		}
    }
}
