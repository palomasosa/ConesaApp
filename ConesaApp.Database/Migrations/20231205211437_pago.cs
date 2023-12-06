using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConesaApp.Database.Migrations
{
    public partial class pago : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "nombre",
                table: "Usuarios",
                newName: "Nombre");

            migrationBuilder.RenameColumn(
                name: "mail",
                table: "Usuarios",
                newName: "Mail");

            migrationBuilder.RenameColumn(
                name: "contraseña",
                table: "Usuarios",
                newName: "Contraseña");

            migrationBuilder.RenameColumn(
                name: "usuarioID",
                table: "Usuarios",
                newName: "UsuarioID");

            migrationBuilder.RenameIndex(
                name: "usuarioID_UQ",
                table: "Usuarios",
                newName: "UsuarioID_UQ");

            migrationBuilder.AddColumn<string>(
                name: "Apellido",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "Clientes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Apellido",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "Clientes");

            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "Usuarios",
                newName: "nombre");

            migrationBuilder.RenameColumn(
                name: "Mail",
                table: "Usuarios",
                newName: "mail");

            migrationBuilder.RenameColumn(
                name: "Contraseña",
                table: "Usuarios",
                newName: "contraseña");

            migrationBuilder.RenameColumn(
                name: "UsuarioID",
                table: "Usuarios",
                newName: "usuarioID");

            migrationBuilder.RenameIndex(
                name: "UsuarioID_UQ",
                table: "Usuarios",
                newName: "usuarioID_UQ");
        }
    }
}
