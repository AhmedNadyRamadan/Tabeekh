using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tabeekh.Migrations
{
    /// <inheritdoc />
    public partial class intialize_DB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Chiefs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chiefs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Deliveries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Available = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deliveries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Delivery_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Customer_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Meal_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Quantity = table.Column<float>(type: "real", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Meal",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Photo = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Prepration_Time = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Available = table.Column<bool>(type: "bit", nullable: false),
                    Ingredients = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Recipe = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Measure_unit = table.Column<int>(type: "int", nullable: false),
                    Chief_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Meal_Chiefs_Chief_Id",
                        column: x => x.Chief_Id,
                        principalTable: "Chiefs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Chiefs_Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Chief_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Customer_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Rate = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chiefs_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Chiefs_Reviews_Chiefs_Chief_Id",
                        column: x => x.Chief_Id,
                        principalTable: "Chiefs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Chiefs_Reviews_Customers_Customer_Id",
                        column: x => x.Customer_Id,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Meals_Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Meal_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Customer_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Rate = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meals_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Meals_Reviews_Customers_Customer_Id",
                        column: x => x.Customer_Id,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Meals_Reviews_Meal_Meal_Id",
                        column: x => x.Meal_Id,
                        principalTable: "Meal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Chiefs_Reviews_Chief_Id",
                table: "Chiefs_Reviews",
                column: "Chief_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Chiefs_Reviews_Customer_Id",
                table: "Chiefs_Reviews",
                column: "Customer_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Meal_Chief_Id",
                table: "Meal",
                column: "Chief_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Meals_Reviews_Customer_Id",
                table: "Meals_Reviews",
                column: "Customer_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Meals_Reviews_Meal_Id",
                table: "Meals_Reviews",
                column: "Meal_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Chiefs_Reviews");

            migrationBuilder.DropTable(
                name: "Deliveries");

            migrationBuilder.DropTable(
                name: "Meals_Reviews");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Meal");

            migrationBuilder.DropTable(
                name: "Chiefs");
        }
    }
}
