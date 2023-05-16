using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using System;

namespace DataAccess.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Professors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: false),
                    Age = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Lectures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ProfessorId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lectures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lectures_Professors_ProfessorId",
                        column: x => x.ProfessorId,
                        principalTable: "Professors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentAttendances",
                columns: table => new
                {
                    LectureId = table.Column<int>(type: "integer", nullable: false),
                    StudentId = table.Column<int>(type: "integer", nullable: false),
                    HomeworkMark = table.Column<int>(type: "integer", nullable: false),
                    IsPresent = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentAttendances", x => new { x.LectureId, x.StudentId });
                    table.ForeignKey(
                        name: "FK_StudentAttendances_Lectures_LectureId",
                        column: x => x.LectureId,
                        principalTable: "Lectures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentAttendances_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Professors",
                columns: new[] { "Id", "Email", "Name" },
                values: new object[,]
                {
                    { 1, "a.einstein@university.com", "Albert Einstein" },
                    { 2, "d.mendeleev@university.com", "Dmitrii Mendeleev" },
                    { 3, "a.popov@university.com", "Aleksandr Popov" }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "Age", "Email", "Name", "Phone" },
                values: new object[,]
                {
                    { 1, 18, "v.pupkin@university.com", "Vasya Pupkin", "89543876435" },
                    { 2, 19, "i.ivanov@university.com", "Ivan Ivanov", "89253781286" },
                    { 3, 20, "s.sidorov@university.com", "Sergei Sidorov", "89115732138" }
                });

            migrationBuilder.InsertData(
                table: "Lectures",
                columns: new[] { "Id", "Date", "Name", "ProfessorId" },
                values: new object[,]
                {
                    { 1, new DateTime(2021, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Physics", 1 },
                    { 2, new DateTime(2021, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Chemistry", 2 },
                    { 3, new DateTime(2021, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "History", 3 }
                });

            migrationBuilder.InsertData(
                table: "StudentAttendances",
                columns: new[] { "LectureId", "StudentId", "HomeworkMark", "IsPresent" },
                values: new object[,]
                {
                    { 1, 1, 5, true },
                    { 1, 2, 0, true },
                    { 1, 3, 0, false },
                    { 2, 1, 4, true },
                    { 2, 2, 0, false },
                    { 2, 3, 3, true },
                    { 3, 1, 0, false },
                    { 3, 2, 4, true },
                    { 3, 3, 0, false }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lectures_ProfessorId",
                table: "Lectures",
                column: "ProfessorId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAttendances_StudentId",
                table: "StudentAttendances",
                column: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentAttendances");

            migrationBuilder.DropTable(
                name: "Lectures");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Professors");
        }
    }
}