using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GolfCourseManager.Models
{
	public class GCMContext : IdentityDbContext<Member>
    {
		public DbSet<Member> Members { get; set; }
		public DbSet<GolfCourse> GolfCourses { get; set; }
		public DbSet<Hole> Holes { get; set; }
		public DbSet<Score> Scores { get; set; }
		public DbSet<TeeTime> TeeTimes { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			var connectionString = Startup.Configuration["Data:GCMConnection:ConnectionString"];
			optionsBuilder.UseSqlServer(connectionString);

			base.OnConfiguring(optionsBuilder);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
		}

		public GCMContext()
		{
		//	Database.EnsureCreated();
		}
	}
}
