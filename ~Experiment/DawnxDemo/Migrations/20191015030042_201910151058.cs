using Microsoft.EntityFrameworkCore.Migrations;

namespace DawnxDemo.Migrations
{
    public partial class _201910151058 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NormalA1",
                table: "OneClasses",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalA2",
                table: "OneClasses",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UniqueA1",
                table: "OneClasses",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UniqueA2",
                table: "OneClasses",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OneClasses_NormalA1_NormalA2",
                table: "OneClasses",
                columns: new[] { "NormalA1", "NormalA2" });

            migrationBuilder.CreateIndex(
                name: "IX_OneClasses_UniqueA1_UniqueA2",
                table: "OneClasses",
                columns: new[] { "UniqueA1", "UniqueA2" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_OneClasses_NormalA1_NormalA2",
                table: "OneClasses");

            migrationBuilder.DropIndex(
                name: "IX_OneClasses_UniqueA1_UniqueA2",
                table: "OneClasses");

            migrationBuilder.DropColumn(
                name: "NormalA1",
                table: "OneClasses");

            migrationBuilder.DropColumn(
                name: "NormalA2",
                table: "OneClasses");

            migrationBuilder.DropColumn(
                name: "UniqueA1",
                table: "OneClasses");

            migrationBuilder.DropColumn(
                name: "UniqueA2",
                table: "OneClasses");
        }
    }
}
