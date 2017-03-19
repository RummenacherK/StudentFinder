using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentFinder.Migrations
{
    public partial class UpdatedVirtualstoLevelsandStudent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GradeLevel",
                table: "Student");

            //migrationBuilder.AddColumn<int>(
            //    name: "GradeLevelId",
            //    table: "Student",
            //    nullable: false,
            //    defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LevelId",
                table: "Student",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Student_LevelId",
                table: "Student",
                column: "LevelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Level_LevelId",
                table: "Student",
                column: "LevelId",
                principalTable: "Level",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Student_Level_LevelId",
                table: "Student");

            migrationBuilder.DropIndex(
                name: "IX_Student_LevelId",
                table: "Student");

            //migrationBuilder.DropColumn(
            //    name: "GradeLevelId",
            //    table: "Student");

            migrationBuilder.DropColumn(
                name: "LevelId",
                table: "Student");

            migrationBuilder.AddColumn<string>(
                name: "GradeLevel",
                table: "Student",
                nullable: false,
                defaultValue: "");
        }
    }
}
