using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BudgetMe.API.Migrations
{
    /// <inheritdoc />
    public partial class CategoryManyToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "BankTransactionCategory",
                columns: table => new
                {
                    CategoriesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TransactionsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankTransactionCategory", x => new { x.CategoriesId, x.TransactionsId });
                    table.ForeignKey(
                        name: "FK_BankTransactionCategory_BankTransaction_TransactionsId",
                        column: x => x.TransactionsId,
                        principalTable: "BankTransaction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BankTransactionCategory_Category_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BankTransactionCategory_TransactionsId",
                table: "BankTransactionCategory",
                column: "TransactionsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BankTransactionCategory");

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
    }
}
