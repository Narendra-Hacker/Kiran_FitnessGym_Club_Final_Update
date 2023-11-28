using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kiran_FitnessGym_Club.Migrations
{
    public partial class narendra_fittness_clb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExerciseType",
                columns: table => new
                {
                    ExercciseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExerciseName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseType", x => x.ExercciseId);
                });

            migrationBuilder.CreateTable(
                name: "Schedule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FromTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    ToTime = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedule", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrainerRegt",
                columns: table => new
                {
                    TrainerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobileNo = table.Column<long>(type: "bigint", nullable: false),
                    Experience = table.Column<int>(type: "int", nullable: true),
                    Salary = table.Column<int>(type: "int", nullable: true),
                    TrainingFees = table.Column<int>(type: "int", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ScheduleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainerRegt", x => x.TrainerId);
                    table.ForeignKey(
                        name: "FK_TrainerRegt_Schedule_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedule",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MemberRegt",
                columns: table => new
                {
                    MemberId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobileNo = table.Column<long>(type: "bigint", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfJoin = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TrainerId = table.Column<int>(type: "int", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberRegt", x => x.MemberId);
                    table.ForeignKey(
                        name: "FK_MemberRegt_TrainerRegt_TrainerId",
                        column: x => x.TrainerId,
                        principalTable: "TrainerRegt",
                        principalColumn: "TrainerId");
                });

            migrationBuilder.CreateTable(
                name: "Feedback",
                columns: table => new
                {
                    FeedbackId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrainerId = table.Column<int>(type: "int", nullable: false),
                    MemberId = table.Column<int>(type: "int", nullable: false),
                    FeedbackText = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedback", x => x.FeedbackId);
                    table.ForeignKey(
                        name: "FK_Feedback_MemberRegt_MemberId",
                        column: x => x.MemberId,
                        principalTable: "MemberRegt",
                        principalColumn: "MemberId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Feedback_TrainerRegt_TrainerId",
                        column: x => x.TrainerId,
                        principalTable: "TrainerRegt",
                        principalColumn: "TrainerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FeeDetails",
                columns: table => new
                {
                    FeeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemberId = table.Column<int>(type: "int", nullable: true),
                    Subscription = table.Column<int>(type: "int", nullable: true),
                    TotalFees = table.Column<int>(type: "int", nullable: true),
                    AmountPaid = table.Column<int>(type: "int", nullable: true),
                    FeeDue = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeeDetails", x => x.FeeId);
                    table.ForeignKey(
                        name: "FK_FeeDetails_MemberRegt_MemberId",
                        column: x => x.MemberId,
                        principalTable: "MemberRegt",
                        principalColumn: "MemberId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_MemberId",
                table: "Feedback",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_TrainerId",
                table: "Feedback",
                column: "TrainerId");

            migrationBuilder.CreateIndex(
                name: "IX_FeeDetails_MemberId",
                table: "FeeDetails",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_MemberRegt_TrainerId",
                table: "MemberRegt",
                column: "TrainerId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainerRegt_ScheduleId",
                table: "TrainerRegt",
                column: "ScheduleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExerciseType");

            migrationBuilder.DropTable(
                name: "Feedback");

            migrationBuilder.DropTable(
                name: "FeeDetails");

            migrationBuilder.DropTable(
                name: "MemberRegt");

            migrationBuilder.DropTable(
                name: "TrainerRegt");

            migrationBuilder.DropTable(
                name: "Schedule");
        }
    }
}
