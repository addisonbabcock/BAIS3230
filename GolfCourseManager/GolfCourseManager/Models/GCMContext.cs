using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;

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

//			modelBuilder.Entity<Score>().HasAlternateKey(t => new { t.GolfCourseId, t.HoleNumber, t.MemberId, t.TeeTimeId });
//			modelBuilder.Entity<TeeTime>().HasAlternateKey(t => t.Start);
//			modelBuilder.Entity<Hole>().HasAlternateKey(t => new { t.GolfCourseId, t.HoleNumber });
		}

		public GCMContext()
		{
		//	Database.EnsureCreated();
		}
	}
}
