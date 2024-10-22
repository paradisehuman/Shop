using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Infrastructure.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ConfigureCustomerIdInDiscountAndStatusInBasket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CustomerId",
                table: "Discounts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Baskets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Discounts_CustomerId",
                table: "Discounts",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Discounts_Customers_CustomerId",
                table: "Discounts",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Discounts_Customers_CustomerId",
                table: "Discounts");

            migrationBuilder.DropIndex(
                name: "IX_Discounts_CustomerId",
                table: "Discounts");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Discounts");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Baskets");
        }
    }
}
