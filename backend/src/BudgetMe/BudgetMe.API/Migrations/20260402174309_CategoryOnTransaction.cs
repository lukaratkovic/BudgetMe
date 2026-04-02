using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BudgetMe.API.Migrations
{
    /// <inheritdoc />
    public partial class CategoryOnTransaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CategoryId",
                table: "BankTransaction",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_BankTransaction_CategoryId",
                table: "BankTransaction",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_BankTransaction_Category_CategoryId",
                table: "BankTransaction",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankTransaction_Category_CategoryId",
                table: "BankTransaction");

            migrationBuilder.DropIndex(
                name: "IX_BankTransaction_CategoryId",
                table: "BankTransaction");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "BankTransaction");
        }
    }
}
