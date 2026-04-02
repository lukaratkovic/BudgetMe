using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BudgetMe.API.Migrations
{
    /// <inheritdoc />
    public partial class Categories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "TransactionType",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    TransactionTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Category_TransactionType_TransactionTypeId",
                        column: x => x.TransactionTypeId,
                        principalTable: "TransactionType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "Description", "Name", "TransactionTypeId" },
                values: new object[,]
                {
                    { new Guid("0ecb7853-21b1-4e54-9bc2-8e2099e0f2f9"), null, "Transportation", new Guid("a96bcd4c-c469-430f-951e-e3492f1de2db") },
                    { new Guid("13237788-358d-48ae-a686-7100aa4e1333"), null, "Gift", new Guid("520a21ea-e710-4eb6-abef-5c4a93d55720") },
                    { new Guid("13921db7-47f2-4e1a-b87e-c75530164533"), null, "Other", new Guid("520a21ea-e710-4eb6-abef-5c4a93d55720") },
                    { new Guid("1d65afc4-0edf-464e-8cab-24f7a175f043"), null, "Transportation", new Guid("a96bcd4c-c469-430f-951e-e3492f1de2db") },
                    { new Guid("32b90ecf-51e6-48a8-a9db-cf8ca9a79ab5"), null, "Household", new Guid("a96bcd4c-c469-430f-951e-e3492f1de2db") },
                    { new Guid("3a75b398-be8f-410c-a3d0-3f9ca395ef98"), null, "Gift", new Guid("a96bcd4c-c469-430f-951e-e3492f1de2db") },
                    { new Guid("8e19c4f0-6051-4ec3-b306-b43616379436"), null, "Earnings", new Guid("520a21ea-e710-4eb6-abef-5c4a93d55720") },
                    { new Guid("8e9f5d5d-fbed-4748-8d5c-53b1d4abb84f"), null, "Food", new Guid("a96bcd4c-c469-430f-951e-e3492f1de2db") },
                    { new Guid("e7b9dd86-7c8e-48f1-acd6-ace5e8909c23"), null, "Entertainment", new Guid("a96bcd4c-c469-430f-951e-e3492f1de2db") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Category_TransactionTypeId",
                table: "Category",
                column: "TransactionTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "TransactionType",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);
        }
    }
}
