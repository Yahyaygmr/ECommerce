using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.Api.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "Cfiles",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cfiles_ProductId",
                table: "Cfiles",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cfiles_Products_ProductId",
                table: "Cfiles",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cfiles_Products_ProductId",
                table: "Cfiles");

            migrationBuilder.DropIndex(
                name: "IX_Cfiles_ProductId",
                table: "Cfiles");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Cfiles");
        }
    }
}
