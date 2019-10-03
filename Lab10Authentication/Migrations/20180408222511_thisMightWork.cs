using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Lab10Authentication.Migrations
{
    public partial class thisMightWork : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AmountInStock",
                table: "CartItems",
                newName: "CartQuantity");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CartQuantity",
                table: "CartItems",
                newName: "AmountInStock");
        }
    }
}
