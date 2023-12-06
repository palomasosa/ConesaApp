using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConesaApp.Database.Migrations
{
    public partial class inicio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    ClienteID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ciudad = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.ClienteID);
                });

            migrationBuilder.CreateTable(
                name: "Coberturas",
                columns: table => new
                {
                    CoberturaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coberturas", x => x.CoberturaID);
                });

            migrationBuilder.CreateTable(
                name: "Empresas",
                columns: table => new
                {
                    EmpresaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresas", x => x.EmpresaID);
                });

            migrationBuilder.CreateTable(
                name: "MetodoPagos",
                columns: table => new
                {
                    MetodoPagoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Metodo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetodoPagos", x => x.MetodoPagoID);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    usuarioID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    mail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    contraseña = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.usuarioID);
                });

            migrationBuilder.CreateTable(
                name: "Polizas",
                columns: table => new
                {
                    PolizaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NroPoliza = table.Column<int>(type: "int", nullable: false),
                    Actualizado = table.Column<bool>(type: "bit", nullable: false),
                    ValorAsegurado = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InicioVigencia = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FinVigencia = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ValorCuota = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EmpresaID = table.Column<int>(type: "int", nullable: false),
                    CoberturaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Polizas", x => x.PolizaID);
                    table.ForeignKey(
                        name: "FK_Polizas_Coberturas_CoberturaID",
                        column: x => x.CoberturaID,
                        principalTable: "Coberturas",
                        principalColumn: "CoberturaID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Polizas_Empresas_EmpresaID",
                        column: x => x.EmpresaID,
                        principalTable: "Empresas",
                        principalColumn: "EmpresaID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pagos",
                columns: table => new
                {
                    PagoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Monto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PolizaID = table.Column<int>(type: "int", nullable: false),
                    ClienteID = table.Column<int>(type: "int", nullable: false),
                    UsuarioID = table.Column<int>(type: "int", nullable: false),
                    MetodoPagoID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pagos", x => x.PagoID);
                    table.ForeignKey(
                        name: "FK_Pagos_Clientes_ClienteID",
                        column: x => x.ClienteID,
                        principalTable: "Clientes",
                        principalColumn: "ClienteID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pagos_MetodoPagos_MetodoPagoID",
                        column: x => x.MetodoPagoID,
                        principalTable: "MetodoPagos",
                        principalColumn: "MetodoPagoID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pagos_Polizas_PolizaID",
                        column: x => x.PolizaID,
                        principalTable: "Polizas",
                        principalColumn: "PolizaID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pagos_Usuarios_UsuarioID",
                        column: x => x.UsuarioID,
                        principalTable: "Usuarios",
                        principalColumn: "usuarioID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Vehiculos",
                columns: table => new
                {
                    VehiculoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Año = table.Column<int>(type: "int", nullable: true),
                    Patente = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Marca = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClienteID = table.Column<int>(type: "int", nullable: false),
                    PolizaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehiculos", x => x.VehiculoID);
                    table.ForeignKey(
                        name: "FK_Vehiculos_Clientes_ClienteID",
                        column: x => x.ClienteID,
                        principalTable: "Clientes",
                        principalColumn: "ClienteID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vehiculos_Polizas_PolizaID",
                        column: x => x.PolizaID,
                        principalTable: "Polizas",
                        principalColumn: "PolizaID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "clienteID_UQ",
                table: "Clientes",
                column: "ClienteID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "coberturaID_UQ",
                table: "Coberturas",
                column: "CoberturaID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "empresaID_UQ",
                table: "Empresas",
                column: "EmpresaID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "metodoPagoID_UQ",
                table: "MetodoPagos",
                column: "MetodoPagoID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pagos_ClienteID",
                table: "Pagos",
                column: "ClienteID");

            migrationBuilder.CreateIndex(
                name: "IX_Pagos_MetodoPagoID",
                table: "Pagos",
                column: "MetodoPagoID");

            migrationBuilder.CreateIndex(
                name: "IX_Pagos_PolizaID",
                table: "Pagos",
                column: "PolizaID");

            migrationBuilder.CreateIndex(
                name: "IX_Pagos_UsuarioID",
                table: "Pagos",
                column: "UsuarioID");

            migrationBuilder.CreateIndex(
                name: "pagoID_UQ",
                table: "Pagos",
                column: "PagoID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Polizas_CoberturaID",
                table: "Polizas",
                column: "CoberturaID");

            migrationBuilder.CreateIndex(
                name: "IX_Polizas_EmpresaID",
                table: "Polizas",
                column: "EmpresaID");

            migrationBuilder.CreateIndex(
                name: "polizaID_UQ",
                table: "Polizas",
                column: "PolizaID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "usuarioID_UQ",
                table: "Usuarios",
                column: "usuarioID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vehiculos_ClienteID",
                table: "Vehiculos",
                column: "ClienteID");

            migrationBuilder.CreateIndex(
                name: "IX_Vehiculos_PolizaID",
                table: "Vehiculos",
                column: "PolizaID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "vehiculoID_UQ",
                table: "Vehiculos",
                column: "VehiculoID",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pagos");

            migrationBuilder.DropTable(
                name: "Vehiculos");

            migrationBuilder.DropTable(
                name: "MetodoPagos");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Polizas");

            migrationBuilder.DropTable(
                name: "Coberturas");

            migrationBuilder.DropTable(
                name: "Empresas");
        }
    }
}
