using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BudgetMe.API.Migrations
{
    /// <inheritdoc />
    public partial class TransactionTypeOnBinding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Binding_Category_CategoryId",
                table: "Binding");

            migrationBuilder.AlterColumn<string>(
                name: "Keyword",
                table: "Binding",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<Guid>(
                name: "TransactionTypeId",
                table: "Binding",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Binding_TransactionTypeId",
                table: "Binding",
                column: "TransactionTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Binding_Category_CategoryId",
                table: "Binding",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Binding_TransactionType_TransactionTypeId",
                table: "Binding",
                column: "TransactionTypeId",
                principalTable: "TransactionType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Binding_Category_CategoryId",
                table: "Binding");

            migrationBuilder.DropForeignKey(
                name: "FK_Binding_TransactionType_TransactionTypeId",
                table: "Binding");

            migrationBuilder.DropIndex(
                name: "IX_Binding_TransactionTypeId",
                table: "Binding");

            migrationBuilder.DropColumn(
                name: "TransactionTypeId",
                table: "Binding");

            migrationBuilder.AlterColumn<string>(
                name: "Keyword",
                table: "Binding",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddForeignKey(
                name: "FK_Binding_Category_CategoryId",
                table: "Binding",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
