namespace GolfCourseManagerModel
{
	using System;
	using System.Data.Entity;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Linq;

	public partial class GCMEntityModel : DbContext
	{
		public GCMEntityModel()
			: base("name=GCMEntityModel")
		{
		}


		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
		}
	}
}
