using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Videogames_Store.Data.Migrations
{
    public partial class _1Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contactos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CódigoArea = table.Column<int>(type: "int", nullable: false),
                    NúmeroTeléfono = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contactos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tarjetas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Clave = table.Column<int>(type: "int", nullable: false),
                    Marca = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NombreBanco = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tarjetas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Videojuegos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Imagen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descripción = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Precio = table.Column<float>(type: "real", nullable: false),
                    AñoLanzamiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CategoriaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Videojuegos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Videojuegos_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dni = table.Column<int>(type: "int", nullable: false),
                    Imagen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactoId = table.Column<int>(type: "int", nullable: true),
                    TarjetaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuarios_Contactos_ContactoId",
                        column: x => x.ContactoId,
                        principalTable: "Contactos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Usuarios_Tarjetas_TarjetaId",
                        column: x => x.TarjetaId,
                        principalTable: "Tarjetas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UsuariosContactos",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    DomicilioId = table.Column<int>(type: "int", nullable: false),
                    ContactoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuariosContactos", x => new { x.UsuarioId, x.DomicilioId });
                    table.ForeignKey(
                        name: "FK_UsuariosContactos_Contactos_ContactoId",
                        column: x => x.ContactoId,
                        principalTable: "Contactos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsuariosContactos_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuariosTarjetas",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    TarjetaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuariosTarjetas", x => new { x.UsuarioId, x.TarjetaId });
                    table.ForeignKey(
                        name: "FK_UsuariosTarjetas_Tarjetas_TarjetaId",
                        column: x => x.TarjetaId,
                        principalTable: "Tarjetas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuariosTarjetas_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_ContactoId",
                table: "Usuarios",
                column: "ContactoId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_TarjetaId",
                table: "Usuarios",
                column: "TarjetaId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosContactos_ContactoId",
                table: "UsuariosContactos",
                column: "ContactoId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosTarjetas_TarjetaId",
                table: "UsuariosTarjetas",
                column: "TarjetaId");

            migrationBuilder.CreateIndex(
                name: "IX_Videojuegos_CategoriaId",
                table: "Videojuegos",
                column: "CategoriaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsuariosContactos");

            migrationBuilder.DropTable(
                name: "UsuariosTarjetas");

            migrationBuilder.DropTable(
                name: "Videojuegos");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropTable(
                name: "Contactos");

            migrationBuilder.DropTable(
                name: "Tarjetas");
        }
    }
}
