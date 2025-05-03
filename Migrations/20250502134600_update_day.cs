using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tabeekh.Migrations
{
    /// <inheritdoc />
    public partial class update_day : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Days",
                table: "Meals",
                newName: "Day");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Day",
                table: "Meals",
                newName: "Days");
        }
    }
}
