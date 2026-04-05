using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BudgetMe.API.Migrations
{
    /// <inheritdoc />
    public partial class CategoryIsSystemFlag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSystem",
                table: "Category",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("0ecb7853-21b1-4e54-9bc2-8e2099e0f2f9"),
                columns: new[] { "IsSystem", "Name" },
                values: new object[] { true, "Other" });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("13237788-358d-48ae-a686-7100aa4e1333"),
                column: "IsSystem",
                value: true);

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("13921db7-47f2-4e1a-b87e-c75530164533"),
                column: "IsSystem",
                value: true);

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("1d65afc4-0edf-464e-8cab-24f7a175f043"),
                column: "IsSystem",
                value: true);

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("32b90ecf-51e6-48a8-a9db-cf8ca9a79ab5"),
                column: "IsSystem",
                value: true);

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("3a75b398-be8f-410c-a3d0-3f9ca395ef98"),
                column: "IsSystem",
                value: true);

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("8e19c4f0-6051-4ec3-b306-b43616379436"),
                column: "IsSystem",
                value: true);

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("8e9f5d5d-fbed-4748-8d5c-53b1d4abb84f"),
                column: "IsSystem",
                value: true);

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("e7b9dd86-7c8e-48f1-acd6-ace5e8909c23"),
                column: "IsSystem",
                value: true);

            migrationBuilder.CreateIndex(
                name: "IX_Category_Name_TransactionTypeId",
                table: "Category",
                columns: new[] { "Name", "TransactionTypeId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Category_Name_TransactionTypeId",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "IsSystem",
                table: "Category");

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("0ecb7853-21b1-4e54-9bc2-8e2099e0f2f9"),
                column: "Name",
                value: "Transportation");
        }
    }
}
