using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tabeekh.Migrations
{
    /// <inheritdoc />
    public partial class addMealsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meal_Chiefs_Chief_Id",
                table: "Meal");

            migrationBuilder.DropForeignKey(
                name: "FK_Meals_Reviews_Meal_Meal_Id",
                table: "Meals_Reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Meal",
                table: "Meal");

            migrationBuilder.RenameTable(
                name: "Meal",
                newName: "Meals");

            migrationBuilder.RenameIndex(
                name: "IX_Meal_Chief_Id",
                table: "Meals",
                newName: "IX_Meals_Chief_Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Meals",
                table: "Meals",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Meals_Chiefs_Chief_Id",
                table: "Meals",
                column: "Chief_Id",
                principalTable: "Chiefs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Meals_Reviews_Meals_Meal_Id",
                table: "Meals_Reviews",
                column: "Meal_Id",
                principalTable: "Meals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meals_Chiefs_Chief_Id",
                table: "Meals");

            migrationBuilder.DropForeignKey(
                name: "FK_Meals_Reviews_Meals_Meal_Id",
                table: "Meals_Reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Meals",
                table: "Meals");

            migrationBuilder.RenameTable(
                name: "Meals",
                newName: "Meal");

            migrationBuilder.RenameIndex(
                name: "IX_Meals_Chief_Id",
                table: "Meal",
                newName: "IX_Meal_Chief_Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Meal",
                table: "Meal",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Meal_Chiefs_Chief_Id",
                table: "Meal",
                column: "Chief_Id",
                principalTable: "Chiefs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Meals_Reviews_Meal_Meal_Id",
                table: "Meals_Reviews",
                column: "Meal_Id",
                principalTable: "Meal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
