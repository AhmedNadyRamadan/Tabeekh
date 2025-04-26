using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tabeekh.Migrations
{
    /// <inheritdoc />
    public partial class add_Category_Relation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CategoryId",
                table: "Meals",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MealId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CategoryMeal",
                columns: table => new
                {
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MealId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryMeal", x => new { x.CategoryId, x.MealId });
                    table.ForeignKey(
                        name: "FK_CategoryMeal_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryMeal_Meals_MealId",
                        column: x => x.MealId,
                        principalTable: "Meals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Meals_Categories",
                columns: table => new
                {
                    MealId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meals_Categories", x => new { x.MealId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_Meals_Categories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Meals_Categories_Meals_MealId",
                        column: x => x.MealId,
                        principalTable: "Meals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Delivery_Cust_Meal_Orders_Customer_Id",
                table: "Delivery_Cust_Meal_Orders",
                column: "Customer_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Delivery_Cust_Meal_Orders_Meal_Id",
                table: "Delivery_Cust_Meal_Orders",
                column: "Meal_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Name",
                table: "Categories",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryMeal_MealId",
                table: "CategoryMeal",
                column: "MealId");

            migrationBuilder.CreateIndex(
                name: "IX_Meals_Categories_CategoryId",
                table: "Meals_Categories",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Delivery_Cust_Meal_Orders_Customers_Customer_Id",
                table: "Delivery_Cust_Meal_Orders",
                column: "Customer_Id",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Delivery_Cust_Meal_Orders_Meals_Meal_Id",
                table: "Delivery_Cust_Meal_Orders",
                column: "Meal_Id",
                principalTable: "Meals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Delivery_Cust_Meal_Orders_Customers_Customer_Id",
                table: "Delivery_Cust_Meal_Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Delivery_Cust_Meal_Orders_Meals_Meal_Id",
                table: "Delivery_Cust_Meal_Orders");

            migrationBuilder.DropTable(
                name: "CategoryMeal");

            migrationBuilder.DropTable(
                name: "Meals_Categories");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Delivery_Cust_Meal_Orders_Customer_Id",
                table: "Delivery_Cust_Meal_Orders");

            migrationBuilder.DropIndex(
                name: "IX_Delivery_Cust_Meal_Orders_Meal_Id",
                table: "Delivery_Cust_Meal_Orders");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Meals");
        }
    }
}
