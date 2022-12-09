using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ImpInfApi.Migrations
{
    public partial class LessonsAndTimesAddImportance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LessonsAndTimes_Lessons_LessonId",
                table: "LessonsAndTimes");

            migrationBuilder.DropIndex(
                name: "IX_LessonsAndTimes_LessonId",
                table: "LessonsAndTimes");

            migrationBuilder.AlterColumn<int>(
                name: "LessonId",
                table: "LessonsAndTimes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Importance",
                table: "LessonsAndTimes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_LessonsAndTimes_LessonId_Time_Type",
                table: "LessonsAndTimes",
                columns: new[] { "LessonId", "Time", "Type" });

            migrationBuilder.AddForeignKey(
                name: "FK_LessonsAndTimes_Lessons_LessonId",
                table: "LessonsAndTimes",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LessonsAndTimes_Lessons_LessonId",
                table: "LessonsAndTimes");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_LessonsAndTimes_LessonId_Time_Type",
                table: "LessonsAndTimes");

            migrationBuilder.DropColumn(
                name: "Importance",
                table: "LessonsAndTimes");

            migrationBuilder.AlterColumn<int>(
                name: "LessonId",
                table: "LessonsAndTimes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_LessonsAndTimes_LessonId",
                table: "LessonsAndTimes",
                column: "LessonId");

            migrationBuilder.AddForeignKey(
                name: "FK_LessonsAndTimes_Lessons_LessonId",
                table: "LessonsAndTimes",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id");
        }
    }
}
