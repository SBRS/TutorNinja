using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TutorNinja.Migrations
{
    public partial class Datanotations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CategoryName",
                table: "Category",
                maxLength: 50,
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Ad",
                maxLength: 50,
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Ad",
                maxLength: 500,
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CategoryName",
                table: "Category",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Ad",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Ad",
                nullable: true);
        }
    }
}
