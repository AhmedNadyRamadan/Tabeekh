using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tabeekh.Migrations
{
    /// <inheritdoc />
    public partial class update_rate_to_totalRate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Rate",
                table: "Cust_Meal_Reviews",
                newName: "totalRate");

            migrationBuilder.RenameColumn(
                name: "Rate",
                table: "Cust_Chief_Reviews",
                newName: "totalRate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "totalRate",
                table: "Cust_Meal_Reviews",
                newName: "Rate");

            migrationBuilder.RenameColumn(
                name: "totalRate",
                table: "Cust_Chief_Reviews",
                newName: "Rate");
        }
    }
}
