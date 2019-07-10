using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace DawnxDemo.Migrations
{
    public partial class _201907091857 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OneClasses",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    Password = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OneClasses", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OneClasses_UserName",
                table: "OneClasses",
                column: "UserName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OneClasses");
        }
    }
}
