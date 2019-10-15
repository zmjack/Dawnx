using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DawnxDemo.Migrations
{
    public partial class _201910151110 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_OneClasses_NormalA1_NormalA2",
                table: "OneClasses");

            migrationBuilder.DropColumn(
                name: "NormalA1",
                table: "OneClasses");

            migrationBuilder.DropColumn(
                name: "NormalA2",
                table: "OneClasses");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "OneClasses",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UniqueA2",
                table: "OneClasses",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UniqueA1",
                table: "OneClasses",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UniqueB1",
                table: "OneClasses",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UniqueB2",
                table: "OneClasses",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_OneClasses_UniqueB1_UniqueB2",
                table: "OneClasses",
                columns: new[] { "UniqueB1", "UniqueB2" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_OneClasses_UniqueB1_UniqueB2",
                table: "OneClasses");

            migrationBuilder.DropColumn(
                name: "UniqueB1",
                table: "OneClasses");

            migrationBuilder.DropColumn(
                name: "UniqueB2",
                table: "OneClasses");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "OneClasses",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "UniqueA2",
                table: "OneClasses",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "UniqueA1",
                table: "OneClasses",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "NormalA1",
                table: "OneClasses",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalA2",
                table: "OneClasses",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OneClasses_NormalA1_NormalA2",
                table: "OneClasses",
                columns: new[] { "NormalA1", "NormalA2" });
        }
    }
}
