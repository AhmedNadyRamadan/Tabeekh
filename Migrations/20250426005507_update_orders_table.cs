using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tabeekh.Migrations
{
    /// <inheritdoc />
    public partial class update_orders_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Delivery_Cust_Meal_Orders_Meals_Meal_Id",
                table: "Delivery_Cust_Meal_Orders");

            migrationBuilder.DropIndex(
                name: "IX_Delivery_Cust_Meal_Orders_Meal_Id",
                table: "Delivery_Cust_Meal_Orders");

            migrationBuilder.DropColumn(
                name: "Meal_Id",
                table: "Delivery_Cust_Meal_Orders");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Delivery_Cust_Meal_Orders");

            migrationBuilder.AlterColumn<string>(
                name: "Note",
                table: "Delivery_Cust_Meal_Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Note",
                table: "Delivery_Cust_Meal_Orders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<Guid>(
                name: "Meal_Id",
                table: "Delivery_Cust_Meal_Orders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<float>(
                name: "Quantity",
                table: "Delivery_Cust_Meal_Orders",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.CreateIndex(
                name: "IX_Delivery_Cust_Meal_Orders_Meal_Id",
                table: "Delivery_Cust_Meal_Orders",
                column: "Meal_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Delivery_Cust_Meal_Orders_Meals_Meal_Id",
                table: "Delivery_Cust_Meal_Orders",
                column: "Meal_Id",
                principalTable: "Meals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
