using Microsoft.EntityFrameworkCore.Migrations;

namespace Videogames_Store.Data.Migrations
{
    public partial class _3Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Contactos_ContactoId",
                table: "Usuarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Tarjetas_TarjetaId",
                table: "Usuarios");

            migrationBuilder.DropTable(
                name: "UsuariosContactos");

            migrationBuilder.DropTable(
                name: "UsuariosTarjetas");

            migrationBuilder.DropTable(
                name: "Contactos");

            migrationBuilder.DropTable(
                name: "Tarjetas");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_ContactoId",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "ContactoId",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Dni",
                table: "Usuarios");

            migrationBuilder.RenameColumn(
                name: "TarjetaId",
                table: "Usuarios",
                newName: "ResidenciaId");

            migrationBuilder.RenameIndex(
                name: "IX_Usuarios_TarjetaId",
                table: "Usuarios",
                newName: "IX_Usuarios_ResidenciaId");

            migrationBuilder.AlterColumn<string>(
                name: "Apellido",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Residencias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreProvincia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NombreCiudad = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Residencias", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Residencias_ResidenciaId",
                table: "Usuarios",
                column: "ResidenciaId",
                principalTable: "Residencias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Residencias_ResidenciaId",
                table: "Usuarios");

            migrationBuilder.DropTable(
                name: "Residencias");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Usuarios");

            migrationBuilder.RenameColumn(
                name: "ResidenciaId",
                table: "Usuarios",
                newName: "TarjetaId");

            migrationBuilder.RenameIndex(
                name: "IX_Usuarios_ResidenciaId",
                table: "Usuarios",
                newName: "IX_Usuarios_TarjetaId");

            migrationBuilder.AlterColumn<string>(
                name: "Apellido",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "ContactoId",
                table: "Usuarios",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Dni",
                table: "Usuarios",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Contactos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CódigoArea = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NúmeroTeléfono = table.Column<int>(type: "int", nullable: false)
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
                name: "IX_UsuariosContactos_ContactoId",
                table: "UsuariosContactos",
                column: "ContactoId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosTarjetas_TarjetaId",
                table: "UsuariosTarjetas",
                column: "TarjetaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Contactos_ContactoId",
                table: "Usuarios",
                column: "ContactoId",
                principalTable: "Contactos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Tarjetas_TarjetaId",
                table: "Usuarios",
                column: "TarjetaId",
                principalTable: "Tarjetas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
