using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DietitianWebApplication.Data.Migrations
{
    public partial class AddMedicalProfileTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MedicalProfiles",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Weight = table.Column<float>(type: "real", nullable: false),
                    Height = table.Column<float>(type: "real", nullable: false),
                    GoalOfDiet = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExerciseTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LevelOfExercise = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DailyFood = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Vitamins = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProblemHistory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalProfiles", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MedicalProfiles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MedicalProfiles_UserId",
                table: "MedicalProfiles",
                column: "UserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MedicalProfiles");
        }
    }
}
