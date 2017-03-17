using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentFinder.Migrations
{
    public partial class ChangedScheduleModelPropertyNamesEnjoyFriends : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FromHh",
                table: "Schedule");

            migrationBuilder.DropColumn(
                name: "FromMm",
                table: "Schedule");

            migrationBuilder.DropColumn(
                name: "ToHh",
                table: "Schedule");

            migrationBuilder.DropColumn(
                name: "ToMm",
                table: "Schedule");

            migrationBuilder.AddColumn<int>(
                name: "From",
                table: "Schedule",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "To",
                table: "Schedule",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "From",
                table: "Schedule");

            migrationBuilder.DropColumn(
                name: "To",
                table: "Schedule");

            migrationBuilder.AddColumn<int>(
                name: "FromHh",
                table: "Schedule",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FromMm",
                table: "Schedule",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ToHh",
                table: "Schedule",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ToMm",
                table: "Schedule",
                nullable: false,
                defaultValue: 0);
        }
    }
}
