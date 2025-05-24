using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inmobiliaria_Benito.Migrations
{
    /// <inheritdoc />
    public partial class AgregarCampoFotoPerfil : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "inquilino",
                columns: table => new
                {
                    InquilinoId = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nombre = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    apellido = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    dni = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    telefono = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.InquilinoId);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "propietario",
                columns: table => new
                {
                    PropietarioId = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nombre = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    apellido = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    dni = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    telefono = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.PropietarioId);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "tipo_inmueble",
                columns: table => new
                {
                    TipoId = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nombre = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.TipoId);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "usuario",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    password_hash = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    rol = table.Column<string>(type: "enum('administrador','empleado')", nullable: true, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    nombre = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    apellido = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.UsuarioId);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "inmueble",
                columns: table => new
                {
                    InmuebleId = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    id_propietario = table.Column<int>(type: "int(11)", nullable: true),
                    id_tipo = table.Column<int>(type: "int(11)", nullable: true),
                    uso = table.Column<string>(type: "enum('comercial','residencial')", nullable: true, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    direccion = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ambientes = table.Column<int>(type: "int(11)", nullable: true),
                    coordenadas = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    precio = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.InmuebleId);
                    table.ForeignKey(
                        name: "inmueble_ibfk_1",
                        column: x => x.id_propietario,
                        principalTable: "propietario",
                        principalColumn: "PropietarioId");
                    table.ForeignKey(
                        name: "inmueble_ibfk_2",
                        column: x => x.id_tipo,
                        principalTable: "tipo_inmueble",
                        principalColumn: "TipoId");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "contrato",
                columns: table => new
                {
                    ContratoId = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    id_inquilino = table.Column<int>(type: "int(11)", nullable: true),
                    id_inmueble = table.Column<int>(type: "int(11)", nullable: true),
                    fecha_desde = table.Column<DateOnly>(type: "date", nullable: true),
                    fecha_hasta = table.Column<DateOnly>(type: "date", nullable: true),
                    monto = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.ContratoId);
                    table.ForeignKey(
                        name: "contrato_ibfk_1",
                        column: x => x.id_inquilino,
                        principalTable: "inquilino",
                        principalColumn: "InquilinoId");
                    table.ForeignKey(
                        name: "contrato_ibfk_2",
                        column: x => x.id_inmueble,
                        principalTable: "inmueble",
                        principalColumn: "InmuebleId");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "pago",
                columns: table => new
                {
                    PagoId = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    id_contrato = table.Column<int>(type: "int(11)", nullable: true),
                    numero_pago = table.Column<int>(type: "int(11)", nullable: true),
                    fecha_pago = table.Column<DateOnly>(type: "date", nullable: true),
                    importe = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.PagoId);
                    table.ForeignKey(
                        name: "pago_ibfk_1",
                        column: x => x.id_contrato,
                        principalTable: "contrato",
                        principalColumn: "ContratoId");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateIndex(
                name: "id_inmueble",
                table: "contrato",
                column: "id_inmueble");

            migrationBuilder.CreateIndex(
                name: "id_inquilino",
                table: "contrato",
                column: "id_inquilino");

            migrationBuilder.CreateIndex(
                name: "id_propietario",
                table: "inmueble",
                column: "id_propietario");

            migrationBuilder.CreateIndex(
                name: "id_tipo",
                table: "inmueble",
                column: "id_tipo");

            migrationBuilder.CreateIndex(
                name: "dni",
                table: "inquilino",
                column: "dni",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "id_contrato",
                table: "pago",
                column: "id_contrato");

            migrationBuilder.CreateIndex(
                name: "dni1",
                table: "propietario",
                column: "dni",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "nombre",
                table: "tipo_inmueble",
                column: "nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "email",
                table: "usuario",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "pago");

            migrationBuilder.DropTable(
                name: "usuario");

            migrationBuilder.DropTable(
                name: "contrato");

            migrationBuilder.DropTable(
                name: "inquilino");

            migrationBuilder.DropTable(
                name: "inmueble");

            migrationBuilder.DropTable(
                name: "propietario");

            migrationBuilder.DropTable(
                name: "tipo_inmueble");
        }
    }
}
