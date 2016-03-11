using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using GolfCourseManager.Models;

namespace GolfCourseManager.Migrations
{
    [DbContext(typeof(GCMContext))]
    [Migration("20160226030549_modelWork")]
    partial class modelWork
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
                    b.Property<int>("HoleNumber")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("GolfCourseId");

                    b.Property<int>("Par");

                    b.Property<int>("YardsBlue");

                    b.Property<int>("YardsRed");

                    b.Property<int>("YardsWhite");

                    b.HasKey("HoleNumber");

                    b.HasAlternateKey("GolfCourseId", "HoleNumber");
                });

            modelBuilder.Entity("GolfCourseManager.Models.Member", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address1");

                    b.Property<string>("Address2");

                    b.Property<string>("Address3");

                    b.Property<string>("City");

                    b.Property<string>("FirstName");

                    b.Property<int>("GolfCourseId");

                    b.Property<string>("LastName");

                    b.Property<string>("PostalCode");

                    b.Property<string>("Province");

                    b.Property<int>("Status");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("GolfCourseManager.Models.Score", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("GolfCourseId");

                    b.Property<int>("HoleNumber");

                    b.Property<int>("MemberId");

                    b.Property<string>("PlayerName");

                    b.Property<int>("Strokes");

                    b.Property<int>("TeeTimeId");

                    b.HasKey("Id");

                    b.HasAlternateKey("GolfCourseId", "HoleNumber", "MemberId", "TeeTimeId");
                });

            modelBuilder.Entity("GolfCourseManager.Models.TeeTime", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("GolfCourseId");

                    b.Property<int>("MemberId");

                    b.Property<string>("Player1Name")
                        .IsRequired();

                    b.Property<string>("Player2Name");

                    b.Property<string>("Player3Name");

                    b.Property<string>("Player4Name");

                    b.Property<DateTime>("Start");

                    b.HasKey("Id");

                    b.HasAlternateKey("Start");
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
                        .HasForeignKey("HoleNumber");

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
        }
    }
}