using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Videogames_Store.Data.Migrations
{
    public partial class _2Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
            name: "AñoLanzamiento",
            table: "Videojuegos");

            migrationBuilder.AddColumn<int>(
                name: "AñoLanzamiento",
                table: "Videojuegos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AñoLanzamiento",
                table: "Videojuegos");

            migrationBuilder.AddColumn<DateTime>(
                name: "AñoLanzamiento",
                table: "Videojuegos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2000, 1, 1));
        }
    }
}
