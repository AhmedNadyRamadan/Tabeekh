using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tabeekh.Migrations
{
    /// <inheritdoc />
    public partial class change_day_emun_in_meal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Day",
                table: "Meals");

            migrationBuilder.AddColumn<string>(
                name: "Days",
                table: "Meals",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "EndUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Days",
                table: "Meals");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "EndUsers");

            migrationBuilder.AddColumn<int>(
                name: "Day",
                table: "Meals",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
