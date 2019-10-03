using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Lab10Authentication.Migrations
{
    public partial class AnotherOne : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Profiles",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "Last",
                table: "Profiles",
                newName: "LastName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Profiles",
                newName: "Username");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Profiles",
                newName: "Last");
        }
    }
}
