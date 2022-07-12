using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FInDashboardWASM.Server.Migrations
{
    public partial class Initialize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "companyData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LogoLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QueryDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Exchange = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ticker = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Locale = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_companyData", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ohlc",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Open = table.Column<double>(type: "float", nullable: false),
                    Low = table.Column<double>(type: "float", nullable: false),
                    High = table.Column<double>(type: "float", nullable: false),
                    Close = table.Column<double>(type: "float", nullable: false),
                    CompanyDataId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ohlc", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ohlc_companyData_CompanyDataId",
                        column: x => x.CompanyDataId,
                        principalTable: "companyData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompanyDataUser",
                columns: table => new
                {
                    CompanyDatasId = table.Column<int>(type: "int", nullable: false),
                    UsersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyDataUser", x => new { x.CompanyDatasId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_CompanyDataUser_companyData_CompanyDatasId",
                        column: x => x.CompanyDatasId,
                        principalTable: "companyData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyDataUser_user_UsersId",
                        column: x => x.UsersId,
                        principalTable: "user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyDataUser_UsersId",
                table: "CompanyDataUser",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_ohlc_CompanyDataId",
                table: "ohlc",
                column: "CompanyDataId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyDataUser");

            migrationBuilder.DropTable(
                name: "ohlc");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "companyData");
        }
    }
}
