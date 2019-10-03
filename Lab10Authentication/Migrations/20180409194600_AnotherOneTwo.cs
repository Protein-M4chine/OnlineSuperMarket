using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Lab10Authentication.Migrations
{
    public partial class AnotherOneTwo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Profiles",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddUniqueConstraint(
                name: "AlternateKey_UserName",
                table: "Profiles",
                column: "UserName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AlternateKey_UserName",
                table: "Profiles");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Profiles",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
