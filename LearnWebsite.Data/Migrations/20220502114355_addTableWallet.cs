using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LearnWebsite.Data.Migrations
{
    public partial class addTableWallet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CashTypes",
                columns: table => new
                {
                    CashTypeId = table.Column<int>(type: "int", nullable: false),
                    TypeTitle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashTypes", x => x.CashTypeId);
                });

            migrationBuilder.CreateTable(
                name: "CashWallets",
                columns: table => new
                {
                    CashWalletId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CashTypeId = table.Column<int>(type: "int", nullable: false),
                    Cash = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsPay = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashWallets", x => x.CashWalletId);
                    table.ForeignKey(
                        name: "FK_CashWallets_CashTypes_CashTypeId",
                        column: x => x.CashTypeId,
                        principalTable: "CashTypes",
                        principalColumn: "CashTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CashWallets_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CashWallets_CashTypeId",
                table: "CashWallets",
                column: "CashTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CashWallets_UserId",
                table: "CashWallets",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CashWallets");

            migrationBuilder.DropTable(
                name: "CashTypes");
        }
    }
}
