using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tabeekh.Migrations
{
    /// <inheritdoc />
    public partial class Add_CustomerController : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chiefs_Reviews_Chiefs_Chief_Id",
                table: "Chiefs_Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Chiefs_Reviews_Customers_Customer_Id",
                table: "Chiefs_Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Meals_Reviews_Customers_Customer_Id",
                table: "Meals_Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Meals_Reviews_Meals_Meal_Id",
                table: "Meals_Reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Meals_Reviews",
                table: "Meals_Reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Chiefs_Reviews",
                table: "Chiefs_Reviews");

            migrationBuilder.RenameTable(
                name: "Orders",
                newName: "Delivery_Cust_Meal_Orders");

            migrationBuilder.RenameTable(
                name: "Meals_Reviews",
                newName: "Cust_Meal_Reviews");

            migrationBuilder.RenameTable(
                name: "Chiefs_Reviews",
                newName: "Cust_Chief_Reviews");

            migrationBuilder.RenameIndex(
                name: "IX_Meals_Reviews_Meal_Id",
                table: "Cust_Meal_Reviews",
                newName: "IX_Cust_Meal_Reviews_Meal_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Meals_Reviews_Customer_Id",
                table: "Cust_Meal_Reviews",
                newName: "IX_Cust_Meal_Reviews_Customer_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Chiefs_Reviews_Customer_Id",
                table: "Cust_Chief_Reviews",
                newName: "IX_Cust_Chief_Reviews_Customer_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Chiefs_Reviews_Chief_Id",
                table: "Cust_Chief_Reviews",
                newName: "IX_Cust_Chief_Reviews_Chief_Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Delivery_Cust_Meal_Orders",
                table: "Delivery_Cust_Meal_Orders",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cust_Meal_Reviews",
                table: "Cust_Meal_Reviews",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cust_Chief_Reviews",
                table: "Cust_Chief_Reviews",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cust_Chief_Reviews_Chiefs_Chief_Id",
                table: "Cust_Chief_Reviews",
                column: "Chief_Id",
                principalTable: "Chiefs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cust_Chief_Reviews_Customers_Customer_Id",
                table: "Cust_Chief_Reviews",
                column: "Customer_Id",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cust_Meal_Reviews_Customers_Customer_Id",
                table: "Cust_Meal_Reviews",
                column: "Customer_Id",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cust_Meal_Reviews_Meals_Meal_Id",
                table: "Cust_Meal_Reviews",
                column: "Meal_Id",
                principalTable: "Meals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cust_Chief_Reviews_Chiefs_Chief_Id",
                table: "Cust_Chief_Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Cust_Chief_Reviews_Customers_Customer_Id",
                table: "Cust_Chief_Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Cust_Meal_Reviews_Customers_Customer_Id",
                table: "Cust_Meal_Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Cust_Meal_Reviews_Meals_Meal_Id",
                table: "Cust_Meal_Reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Delivery_Cust_Meal_Orders",
                table: "Delivery_Cust_Meal_Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cust_Meal_Reviews",
                table: "Cust_Meal_Reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cust_Chief_Reviews",
                table: "Cust_Chief_Reviews");

            migrationBuilder.RenameTable(
                name: "Delivery_Cust_Meal_Orders",
                newName: "Orders");

            migrationBuilder.RenameTable(
                name: "Cust_Meal_Reviews",
                newName: "Meals_Reviews");

            migrationBuilder.RenameTable(
                name: "Cust_Chief_Reviews",
                newName: "Chiefs_Reviews");

            migrationBuilder.RenameIndex(
                name: "IX_Cust_Meal_Reviews_Meal_Id",
                table: "Meals_Reviews",
                newName: "IX_Meals_Reviews_Meal_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Cust_Meal_Reviews_Customer_Id",
                table: "Meals_Reviews",
                newName: "IX_Meals_Reviews_Customer_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Cust_Chief_Reviews_Customer_Id",
                table: "Chiefs_Reviews",
                newName: "IX_Chiefs_Reviews_Customer_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Cust_Chief_Reviews_Chief_Id",
                table: "Chiefs_Reviews",
                newName: "IX_Chiefs_Reviews_Chief_Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                table: "Orders",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Meals_Reviews",
                table: "Meals_Reviews",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Chiefs_Reviews",
                table: "Chiefs_Reviews",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Chiefs_Reviews_Chiefs_Chief_Id",
                table: "Chiefs_Reviews",
                column: "Chief_Id",
                principalTable: "Chiefs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Chiefs_Reviews_Customers_Customer_Id",
                table: "Chiefs_Reviews",
                column: "Customer_Id",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Meals_Reviews_Customers_Customer_Id",
                table: "Meals_Reviews",
                column: "Customer_Id",
                principalTable: "Customers",
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
    }
}
