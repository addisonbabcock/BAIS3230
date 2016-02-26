using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Metadata;

namespace GolfCourseManager.Migrations
{
    public partial class modelWork : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GolfCourse",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FridayClose = table.Column<DateTime>(nullable: false),
                    FridayOpen = table.Column<DateTime>(nullable: false),
                    MondayClose = table.Column<DateTime>(nullable: false),
                    MondayOpen = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    SaturdayClose = table.Column<DateTime>(nullable: false),
                    SaturdayOpen = table.Column<DateTime>(nullable: false),
                    SundayClose = table.Column<DateTime>(nullable: false),
                    SundayOpen = table.Column<DateTime>(nullable: false),
                    TeeTimeInterval = table.Column<TimeSpan>(nullable: false),
                    ThursdayClose = table.Column<DateTime>(nullable: false),
                    ThursdayOpen = table.Column<DateTime>(nullable: false),
                    TuesdayClose = table.Column<DateTime>(nullable: false),
                    TuesdayOpen = table.Column<DateTime>(nullable: false),
                    WednesdayClose = table.Column<DateTime>(nullable: false),
                    WednesdayOpen = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GolfCourse", x => x.Id);
                });
            migrationBuilder.CreateTable(
                name: "Hole",
                columns: table => new
                {
                    HoleNumber = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GolfCourseId = table.Column<int>(nullable: false),
                    Par = table.Column<int>(nullable: false),
                    YardsBlue = table.Column<int>(nullable: false),
                    YardsRed = table.Column<int>(nullable: false),
                    YardsWhite = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hole", x => x.HoleNumber);
                    table.UniqueConstraint("AK_Hole_GolfCourseId_HoleNumber", x => new { x.GolfCourseId, x.HoleNumber });
                    table.ForeignKey(
                        name: "FK_Hole_GolfCourse_GolfCourseId",
                        column: x => x.GolfCourseId,
                        principalTable: "GolfCourse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });
            migrationBuilder.CreateTable(
                name: "TeeTime",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GolfCourseId = table.Column<int>(nullable: false),
                    MemberId = table.Column<int>(nullable: false),
                    Player1Name = table.Column<string>(nullable: false),
                    Player2Name = table.Column<string>(nullable: true),
                    Player3Name = table.Column<string>(nullable: true),
                    Player4Name = table.Column<string>(nullable: true),
                    Start = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeeTime", x => x.Id);
                    table.UniqueConstraint("AK_TeeTime_Start", x => x.Start);
                    table.ForeignKey(
                        name: "FK_TeeTime_GolfCourse_GolfCourseId",
                        column: x => x.GolfCourseId,
                        principalTable: "GolfCourse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_TeeTime_Member_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Member",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });
            migrationBuilder.CreateTable(
                name: "Score",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GolfCourseId = table.Column<int>(nullable: false),
                    HoleNumber = table.Column<int>(nullable: false),
                    MemberId = table.Column<int>(nullable: false),
                    PlayerName = table.Column<string>(nullable: true),
                    Strokes = table.Column<int>(nullable: false),
                    TeeTimeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Score", x => x.Id);
                    table.UniqueConstraint("AK_Score_GolfCourseId_HoleNumber_MemberId_TeeTimeId", x => new { x.GolfCourseId, x.HoleNumber, x.MemberId, x.TeeTimeId });
                    table.ForeignKey(
                        name: "FK_Score_GolfCourse_GolfCourseId",
                        column: x => x.GolfCourseId,
                        principalTable: "GolfCourse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Score_Hole_HoleNumber",
                        column: x => x.HoleNumber,
                        principalTable: "Hole",
                        principalColumn: "HoleNumber",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Score_Member_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Member",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Score_TeeTime_TeeTimeId",
                        column: x => x.TeeTimeId,
                        principalTable: "TeeTime",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });
            migrationBuilder.AddColumn<string>(
                name: "Address1",
                table: "Member",
                nullable: true);
            migrationBuilder.AddColumn<string>(
                name: "Address2",
                table: "Member",
                nullable: true);
            migrationBuilder.AddColumn<string>(
                name: "Address3",
                table: "Member",
                nullable: true);
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Member",
                nullable: true);
            migrationBuilder.AddColumn<int>(
                name: "GolfCourseId",
                table: "Member",
                nullable: false,
                defaultValue: 0);
            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "Member",
                nullable: true);
            migrationBuilder.AddColumn<string>(
                name: "Province",
                table: "Member",
                nullable: true);
            migrationBuilder.AddForeignKey(
                name: "FK_Member_GolfCourse_GolfCourseId",
                table: "Member",
                column: "GolfCourseId",
                principalTable: "GolfCourse",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_Member_GolfCourse_GolfCourseId", table: "Member");
            migrationBuilder.DropColumn(name: "Address1", table: "Member");
            migrationBuilder.DropColumn(name: "Address2", table: "Member");
            migrationBuilder.DropColumn(name: "Address3", table: "Member");
            migrationBuilder.DropColumn(name: "City", table: "Member");
            migrationBuilder.DropColumn(name: "GolfCourseId", table: "Member");
            migrationBuilder.DropColumn(name: "PostalCode", table: "Member");
            migrationBuilder.DropColumn(name: "Province", table: "Member");
            migrationBuilder.DropTable("Score");
            migrationBuilder.DropTable("Hole");
            migrationBuilder.DropTable("TeeTime");
            migrationBuilder.DropTable("GolfCourse");
        }
    }
}
