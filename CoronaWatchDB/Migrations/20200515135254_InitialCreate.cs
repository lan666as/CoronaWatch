using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace CoronaWatchDB.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RegionDBs",
                columns: table => new
                {
                    ISOCode = table.Column<string>(unicode: false, maxLength: 4, nullable: false),
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Slug = table.Column<string>(unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegionDBs", x => x.ISOCode);
                });

            migrationBuilder.CreateTable(
                name: "ReportDBs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ISOCode = table.Column<string>(unicode: false, maxLength: 4, nullable: true),
                    Confirmed = table.Column<int>(nullable: true),
                    Recovered = table.Column<int>(nullable: true),
                    Death = table.Column<int>(nullable: true),
                    Active = table.Column<int>(nullable: true),
                    Date = table.Column<DateTime>(type: "date", nullable: true),
                    RegionDBISOCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportDBs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportDBs_RegionDBs_RegionDBISOCode",
                        column: x => x.RegionDBISOCode,
                        principalTable: "RegionDBs",
                        principalColumn: "ISOCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReportDBs_RegionDBISOCode",
                table: "ReportDBs",
                column: "RegionDBISOCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReportDBs");

            migrationBuilder.DropTable(
                name: "RegionDBs");
        }
    }
}
