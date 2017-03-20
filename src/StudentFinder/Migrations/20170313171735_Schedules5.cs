using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentFinder.Migrations
{
    public partial class Schedules5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FromValue",
                table: "Schedule",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ToValue",
                table: "Schedule",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "To",
                table: "Schedule",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "From",
                table: "Schedule",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FromValue",
                table: "Schedule");

            migrationBuilder.DropColumn(
                name: "ToValue",
                table: "Schedule");

            migrationBuilder.AlterColumn<string>(
                name: "To",
                table: "Schedule",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "From",
                table: "Schedule",
                nullable: false);
        }
    }
}
