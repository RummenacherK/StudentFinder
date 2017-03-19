using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentFinder.Migrations
{
    public partial class UpdatedStudentModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Student_Level_LevelId",
                table: "Student");

            //migrationBuilder.DropColumn(
            //    name: "GradeLevelId",
            //    table: "Student");

            //migrationBuilder.AlterColumn<int>(
            //    name: "LevelId",
            //    table: "Student",
            //    nullable: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Level_LevelId",
                table: "Student",
                column: "LevelId",
                principalTable: "Level",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Student_Level_LevelId",
                table: "Student");

            //migrationBuilder.AddColumn<int>(
            //    name: "GradeLevelId",
            //    table: "Student",
            //    nullable: false,
            //    defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "LevelId",
                table: "Student",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Level_LevelId",
                table: "Student",
                column: "LevelId",
                principalTable: "Level",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
