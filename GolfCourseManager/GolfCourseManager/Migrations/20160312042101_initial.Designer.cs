using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using GolfCourseManager.Models;

namespace GolfCourseManager.Migrations
{
    [DbContext(typeof(GCMContext))]
    [Migration("20160312042101_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GolfCourseManager.Models.GolfCourse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("FridayClose");

                    b.Property<DateTime>("FridayOpen");

                    b.Property<DateTime>("MondayClose");

                    b.Property<DateTime>("MondayOpen");

                    b.Property<string>("Name");

                    b.Property<DateTime>("SaturdayClose");

                    b.Property<DateTime>("SaturdayOpen");

                    b.Property<DateTime>("SundayClose");

                    b.Property<DateTime>("SundayOpen");

                    b.Property<TimeSpan>("TeeTimeInterval");

                    b.Property<DateTime>("ThursdayClose");

                    b.Property<DateTime>("ThursdayOpen");

                    b.Property<DateTime>("TuesdayClose");

                    b.Property<DateTime>("TuesdayOpen");

                    b.Property<DateTime>("WednesdayClose");

                    b.Property<DateTime>("WednesdayOpen");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("GolfCourseManager.Models.Hole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("GolfCourseId");

                    b.Property<int>("HoleNumber");

                    b.Property<int>("Par");

                    b.Property<int>("YardsBlue");

                    b.Property<int>("YardsRed");

                    b.Property<int>("YardsWhite");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("GolfCourseManager.Models.Member", b =>
                {
                    b.Property<string>("Id");

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("Address1");

                    b.Property<string>("Address2");

                    b.Property<string>("Address3");

                    b.Property<string>("City");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName");

                    b.Property<int?>("GolfCourseId");

                    b.Property<string>("LastName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedUserName")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("PostalCode");

                    b.Property<string>("Province");

                    b.Property<string>("SecurityStamp");

                    b.Property<int>("Status");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasAnnotation("Relational:Name", "EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .HasAnnotation("Relational:Name", "UserNameIndex");

                    b.HasAnnotation("Relational:TableName", "AspNetUsers");
                });

            modelBuilder.Entity("GolfCourseManager.Models.Score", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("GolfCourseId");

                    b.Property<int?>("HoleId");

                    b.Property<string>("MemberId");

                    b.Property<string>("PlayerName");

                    b.Property<int>("Strokes");

                    b.Property<int?>("TeeTimeId");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("GolfCourseManager.Models.TeeTime", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("GolfCourseId");

                    b.Property<string>("MemberId");

                    b.Property<string>("Player1Name")
                        .IsRequired();

                    b.Property<string>("Player2Name");

                    b.Property<string>("Player3Name");

                    b.Property<string>("Player4Name");

                    b.Property<DateTime>("Start");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityRole", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .HasAnnotation("Relational:Name", "RoleNameIndex");

                    b.HasAnnotation("Relational:TableName", "AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasAnnotation("Relational:TableName", "AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasAnnotation("Relational:TableName", "AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasAnnotation("Relational:TableName", "AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasAnnotation("Relational:TableName", "AspNetUserRoles");
                });

            modelBuilder.Entity("GolfCourseManager.Models.Hole", b =>
                {
                    b.HasOne("GolfCourseManager.Models.GolfCourse")
                        .WithMany()
                        .HasForeignKey("GolfCourseId");
                });

            modelBuilder.Entity("GolfCourseManager.Models.Member", b =>
                {
                    b.HasOne("GolfCourseManager.Models.GolfCourse")
                        .WithMany()
                        .HasForeignKey("GolfCourseId");
                });

            modelBuilder.Entity("GolfCourseManager.Models.Score", b =>
                {
                    b.HasOne("GolfCourseManager.Models.GolfCourse")
                        .WithMany()
                        .HasForeignKey("GolfCourseId");

                    b.HasOne("GolfCourseManager.Models.Hole")
                        .WithMany()
                        .HasForeignKey("HoleId");

                    b.HasOne("GolfCourseManager.Models.Member")
                        .WithMany()
                        .HasForeignKey("MemberId");

                    b.HasOne("GolfCourseManager.Models.TeeTime")
                        .WithMany()
                        .HasForeignKey("TeeTimeId");
                });

            modelBuilder.Entity("GolfCourseManager.Models.TeeTime", b =>
                {
                    b.HasOne("GolfCourseManager.Models.GolfCourse")
                        .WithMany()
                        .HasForeignKey("GolfCourseId");

                    b.HasOne("GolfCourseManager.Models.Member")
                        .WithMany()
                        .HasForeignKey("MemberId");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNet.Identity.EntityFramework.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("GolfCourseManager.Models.Member")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("GolfCourseManager.Models.Member")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNet.Identity.EntityFramework.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId");

                    b.HasOne("GolfCourseManager.Models.Member")
                        .WithMany()
                        .HasForeignKey("UserId");
                });
        }
    }
}
