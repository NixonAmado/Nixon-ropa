using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Cargo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Descripcion = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SueldoBase = table.Column<decimal>(type: "decimal(15,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cargo", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Color",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Descripcion = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Color", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Forma_Pago",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Descripcion = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Forma_Pago", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Genero",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Descripcion = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genero", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Insumo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ValorUnitCop = table.Column<decimal>(type: "decimal(20,2)", nullable: false),
                    StockMin = table.Column<int>(type: "int", maxLength: 10, nullable: false),
                    StockMax = table.Column<int>(type: "int", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Insumo", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Pais",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pais", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Talla",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Descripcion = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Talla", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Tipo_estado",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Descripcion = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tipo_estado", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Tipo_persona",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tipo_persona", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Tipo_Proteccion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Descripcion = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tipo_Proteccion", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    user_name = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    password = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Departamento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IdPaisFk = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departamento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Departamento_Pais_IdPaisFk",
                        column: x => x.IdPaisFk,
                        principalTable: "Pais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Estado",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Descripcion = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IdTipoEstadoFk = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estado", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Estado_Tipo_estado_IdTipoEstadoFk",
                        column: x => x.IdTipoEstadoFk,
                        principalTable: "Tipo_estado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RefreshToken",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdUserFk = table.Column<int>(type: "int", nullable: false),
                    Token = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Expires = table.Column<DateTime>(type: "DateTime", nullable: false),
                    Created = table.Column<DateTime>(type: "DateTime", nullable: false),
                    Revoked = table.Column<DateTime>(type: "DateTime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshToken_User_IdUserFk",
                        column: x => x.IdUserFk,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UsersRoles",
                columns: table => new
                {
                    IdUserFk = table.Column<int>(type: "int", nullable: false),
                    IdRolFk = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersRoles", x => new { x.IdRolFk, x.IdUserFk });
                    table.ForeignKey(
                        name: "FK_UsersRoles_Role_IdRolFk",
                        column: x => x.IdRolFk,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersRoles_User_IdUserFk",
                        column: x => x.IdUserFk,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Municipio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IdDepartamentoFk = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Municipio", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Municipio_Departamento_IdDepartamentoFk",
                        column: x => x.IdDepartamentoFk,
                        principalTable: "Departamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Prenda",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdPrenda = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ValorUnitCop = table.Column<decimal>(type: "decimal(15,2)", maxLength: 40, nullable: false),
                    ValorUnitUsd = table.Column<decimal>(type: "decimal(15,2)", maxLength: 40, nullable: false),
                    IdTipoProteccionFk = table.Column<int>(type: "int", nullable: false),
                    IdGeneroFk = table.Column<int>(type: "int", nullable: false),
                    IdEstadoFk = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prenda", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prenda_Estado_IdEstadoFk",
                        column: x => x.IdEstadoFk,
                        principalTable: "Estado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prenda_Genero_IdGeneroFk",
                        column: x => x.IdGeneroFk,
                        principalTable: "Genero",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prenda_Tipo_Proteccion_IdTipoProteccionFk",
                        column: x => x.IdTipoProteccionFk,
                        principalTable: "Tipo_Proteccion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Identificacion = table.Column<int>(type: "int", nullable: false),
                    IdTipoPersonaFk = table.Column<int>(type: "int", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "DateTime", nullable: false),
                    IdMunicipioFk = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cliente_Municipio_IdMunicipioFk",
                        column: x => x.IdMunicipioFk,
                        principalTable: "Municipio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cliente_Tipo_persona_IdTipoPersonaFk",
                        column: x => x.IdTipoPersonaFk,
                        principalTable: "Tipo_persona",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Empleado",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Identificacion = table.Column<int>(type: "int", nullable: false),
                    FechaIngreso = table.Column<DateTime>(type: "DateTime", nullable: false),
                    IdCargoFk = table.Column<int>(type: "int", nullable: false),
                    IdMunicipioFk = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empleado", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Empleado_Cargo_IdCargoFk",
                        column: x => x.IdCargoFk,
                        principalTable: "Cargo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Empleado_Municipio_IdMunicipioFk",
                        column: x => x.IdMunicipioFk,
                        principalTable: "Municipio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Empresa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nit = table.Column<int>(type: "int", nullable: false),
                    RazonSocial = table.Column<string>(type: "varchar(70)", maxLength: 70, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RepresentanteLegal = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FechaCreacion = table.Column<DateTime>(type: "DateTime", nullable: false),
                    IdMunicipioFk = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresa", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Empresa_Municipio_IdMunicipioFk",
                        column: x => x.IdMunicipioFk,
                        principalTable: "Municipio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Proveedor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NitProveedor = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IdTipoPersonaFk = table.Column<int>(type: "int", nullable: false),
                    IdMunicipioFk = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proveedor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Proveedor_Municipio_IdMunicipioFk",
                        column: x => x.IdMunicipioFk,
                        principalTable: "Municipio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Proveedor_Tipo_persona_IdTipoPersonaFk",
                        column: x => x.IdTipoPersonaFk,
                        principalTable: "Tipo_persona",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Insumo_Prenda",
                columns: table => new
                {
                    IdInsumoFk = table.Column<int>(type: "int", nullable: false),
                    IdPrendaFk = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Insumo_Prenda", x => new { x.IdInsumoFk, x.IdPrendaFk });
                    table.ForeignKey(
                        name: "FK_Insumo_Prenda_Insumo_IdInsumoFk",
                        column: x => x.IdInsumoFk,
                        principalTable: "Insumo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Insumo_Prenda_Prenda_IdPrendaFk",
                        column: x => x.IdPrendaFk,
                        principalTable: "Prenda",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Inventario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CodInventario = table.Column<int>(type: "int", nullable: false),
                    ValorVtaCop = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    ValorVtaUsd = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    IdPrendaFk = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inventario_Prenda_IdPrendaFk",
                        column: x => x.IdPrendaFk,
                        principalTable: "Prenda",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Orden",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Fecha = table.Column<DateTime>(type: "DateTime", maxLength: 40, nullable: false),
                    IdEmpleadoFk = table.Column<int>(type: "int", nullable: false),
                    IdClienteFk = table.Column<int>(type: "int", nullable: false),
                    IdEstadoFk = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orden", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orden_Cliente_IdClienteFk",
                        column: x => x.IdClienteFk,
                        principalTable: "Cliente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orden_Empleado_IdEmpleadoFk",
                        column: x => x.IdEmpleadoFk,
                        principalTable: "Empleado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orden_Estado_IdEstadoFk",
                        column: x => x.IdEstadoFk,
                        principalTable: "Estado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Venta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Fecha = table.Column<DateTime>(type: "DateTime", nullable: false),
                    IdEmpleadoFk = table.Column<int>(type: "int", nullable: false),
                    IdClienteFk = table.Column<int>(type: "int", nullable: false),
                    IdFormaPagoFk = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Venta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Venta_Cliente_IdClienteFk",
                        column: x => x.IdClienteFk,
                        principalTable: "Cliente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Venta_Empleado_IdEmpleadoFk",
                        column: x => x.IdEmpleadoFk,
                        principalTable: "Empleado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Venta_Forma_Pago_IdFormaPagoFk",
                        column: x => x.IdFormaPagoFk,
                        principalTable: "Forma_Pago",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "InsumosProveedores",
                columns: table => new
                {
                    IdInsumoFk = table.Column<int>(type: "int", nullable: false),
                    IdProveedorFk = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsumosProveedores", x => new { x.IdProveedorFk, x.IdInsumoFk });
                    table.ForeignKey(
                        name: "FK_InsumosProveedores_Insumo_IdInsumoFk",
                        column: x => x.IdInsumoFk,
                        principalTable: "Insumo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InsumosProveedores_Proveedor_IdProveedorFk",
                        column: x => x.IdProveedorFk,
                        principalTable: "Proveedor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Inventario_Talla",
                columns: table => new
                {
                    IdInventarioFk = table.Column<int>(type: "int", nullable: false),
                    IdTallaFk = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventario_Talla", x => new { x.IdInventarioFk, x.IdTallaFk });
                    table.ForeignKey(
                        name: "FK_Inventario_Talla_Inventario_IdInventarioFk",
                        column: x => x.IdInventarioFk,
                        principalTable: "Inventario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Inventario_Talla_Talla_IdTallaFk",
                        column: x => x.IdTallaFk,
                        principalTable: "Talla",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Detalle_orden",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdOrden = table.Column<int>(type: "int", nullable: false),
                    CantidadProducir = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CantidadProducida = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IdEstadoFk = table.Column<int>(type: "int", nullable: false),
                    IdPrendaFk = table.Column<int>(type: "int", nullable: false),
                    IdColorFk = table.Column<int>(type: "int", nullable: false),
                    OrdenId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Detalle_orden", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Detalle_orden_Color_IdColorFk",
                        column: x => x.IdColorFk,
                        principalTable: "Color",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Detalle_orden_Estado_IdEstadoFk",
                        column: x => x.IdEstadoFk,
                        principalTable: "Estado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Detalle_orden_Orden_OrdenId",
                        column: x => x.OrdenId,
                        principalTable: "Orden",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Detalle_orden_Prenda_IdPrendaFk",
                        column: x => x.IdPrendaFk,
                        principalTable: "Prenda",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Detalle_venta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Cantidad = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ValorUnit = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    IdInventarioFk = table.Column<int>(type: "int", nullable: false),
                    IdTallaFk = table.Column<int>(type: "int", nullable: false),
                    IdVentaFk = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Detalle_venta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Detalle_venta_Inventario_IdInventarioFk",
                        column: x => x.IdInventarioFk,
                        principalTable: "Inventario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Detalle_venta_Talla_IdTallaFk",
                        column: x => x.IdTallaFk,
                        principalTable: "Talla",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Detalle_venta_Venta_IdVentaFk",
                        column: x => x.IdVentaFk,
                        principalTable: "Venta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Description" },
                values: new object[,]
                {
                    { 1, "Administrador" },
                    { 2, "Empleado" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_Identificacion",
                table: "Cliente",
                column: "Identificacion",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_IdMunicipioFk",
                table: "Cliente",
                column: "IdMunicipioFk");

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_IdTipoPersonaFk",
                table: "Cliente",
                column: "IdTipoPersonaFk");

            migrationBuilder.CreateIndex(
                name: "IX_Departamento_IdPaisFk",
                table: "Departamento",
                column: "IdPaisFk");

            migrationBuilder.CreateIndex(
                name: "IX_Detalle_orden_IdColorFk",
                table: "Detalle_orden",
                column: "IdColorFk");

            migrationBuilder.CreateIndex(
                name: "IX_Detalle_orden_IdEstadoFk",
                table: "Detalle_orden",
                column: "IdEstadoFk");

            migrationBuilder.CreateIndex(
                name: "IX_Detalle_orden_IdPrendaFk",
                table: "Detalle_orden",
                column: "IdPrendaFk");

            migrationBuilder.CreateIndex(
                name: "IX_Detalle_orden_OrdenId",
                table: "Detalle_orden",
                column: "OrdenId");

            migrationBuilder.CreateIndex(
                name: "IX_Detalle_venta_IdInventarioFk",
                table: "Detalle_venta",
                column: "IdInventarioFk");

            migrationBuilder.CreateIndex(
                name: "IX_Detalle_venta_IdTallaFk",
                table: "Detalle_venta",
                column: "IdTallaFk");

            migrationBuilder.CreateIndex(
                name: "IX_Detalle_venta_IdVentaFk",
                table: "Detalle_venta",
                column: "IdVentaFk");

            migrationBuilder.CreateIndex(
                name: "IX_Empleado_IdCargoFk",
                table: "Empleado",
                column: "IdCargoFk");

            migrationBuilder.CreateIndex(
                name: "IX_Empleado_Identificacion",
                table: "Empleado",
                column: "Identificacion",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Empleado_IdMunicipioFk",
                table: "Empleado",
                column: "IdMunicipioFk");

            migrationBuilder.CreateIndex(
                name: "IX_Empresa_IdMunicipioFk",
                table: "Empresa",
                column: "IdMunicipioFk");

            migrationBuilder.CreateIndex(
                name: "IX_Empresa_Nit",
                table: "Empresa",
                column: "Nit",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Estado_IdTipoEstadoFk",
                table: "Estado",
                column: "IdTipoEstadoFk");

            migrationBuilder.CreateIndex(
                name: "IX_Insumo_Nombre",
                table: "Insumo",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Insumo_Prenda_IdPrendaFk",
                table: "Insumo_Prenda",
                column: "IdPrendaFk");

            migrationBuilder.CreateIndex(
                name: "IX_InsumosProveedores_IdInsumoFk",
                table: "InsumosProveedores",
                column: "IdInsumoFk");

            migrationBuilder.CreateIndex(
                name: "IX_Inventario_CodInventario",
                table: "Inventario",
                column: "CodInventario",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Inventario_IdPrendaFk",
                table: "Inventario",
                column: "IdPrendaFk");

            migrationBuilder.CreateIndex(
                name: "IX_Inventario_Talla_IdTallaFk",
                table: "Inventario_Talla",
                column: "IdTallaFk");

            migrationBuilder.CreateIndex(
                name: "IX_Municipio_IdDepartamentoFk",
                table: "Municipio",
                column: "IdDepartamentoFk");

            migrationBuilder.CreateIndex(
                name: "IX_Orden_IdClienteFk",
                table: "Orden",
                column: "IdClienteFk");

            migrationBuilder.CreateIndex(
                name: "IX_Orden_IdEmpleadoFk",
                table: "Orden",
                column: "IdEmpleadoFk");

            migrationBuilder.CreateIndex(
                name: "IX_Orden_IdEstadoFk",
                table: "Orden",
                column: "IdEstadoFk");

            migrationBuilder.CreateIndex(
                name: "IX_Prenda_IdEstadoFk",
                table: "Prenda",
                column: "IdEstadoFk");

            migrationBuilder.CreateIndex(
                name: "IX_Prenda_IdGeneroFk",
                table: "Prenda",
                column: "IdGeneroFk");

            migrationBuilder.CreateIndex(
                name: "IX_Prenda_IdPrenda",
                table: "Prenda",
                column: "IdPrenda",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Prenda_IdTipoProteccionFk",
                table: "Prenda",
                column: "IdTipoProteccionFk");

            migrationBuilder.CreateIndex(
                name: "IX_Proveedor_IdMunicipioFk",
                table: "Proveedor",
                column: "IdMunicipioFk");

            migrationBuilder.CreateIndex(
                name: "IX_Proveedor_IdTipoPersonaFk",
                table: "Proveedor",
                column: "IdTipoPersonaFk");

            migrationBuilder.CreateIndex(
                name: "IX_Proveedor_NitProveedor",
                table: "Proveedor",
                column: "NitProveedor",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_IdUserFk",
                table: "RefreshToken",
                column: "IdUserFk");

            migrationBuilder.CreateIndex(
                name: "IX_Talla_Descripcion",
                table: "Talla",
                column: "Descripcion",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsersRoles_IdUserFk",
                table: "UsersRoles",
                column: "IdUserFk");

            migrationBuilder.CreateIndex(
                name: "IX_Venta_IdClienteFk",
                table: "Venta",
                column: "IdClienteFk");

            migrationBuilder.CreateIndex(
                name: "IX_Venta_IdEmpleadoFk",
                table: "Venta",
                column: "IdEmpleadoFk");

            migrationBuilder.CreateIndex(
                name: "IX_Venta_IdFormaPagoFk",
                table: "Venta",
                column: "IdFormaPagoFk");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Detalle_orden");

            migrationBuilder.DropTable(
                name: "Detalle_venta");

            migrationBuilder.DropTable(
                name: "Empresa");

            migrationBuilder.DropTable(
                name: "Insumo_Prenda");

            migrationBuilder.DropTable(
                name: "InsumosProveedores");

            migrationBuilder.DropTable(
                name: "Inventario_Talla");

            migrationBuilder.DropTable(
                name: "RefreshToken");

            migrationBuilder.DropTable(
                name: "UsersRoles");

            migrationBuilder.DropTable(
                name: "Color");

            migrationBuilder.DropTable(
                name: "Orden");

            migrationBuilder.DropTable(
                name: "Venta");

            migrationBuilder.DropTable(
                name: "Insumo");

            migrationBuilder.DropTable(
                name: "Proveedor");

            migrationBuilder.DropTable(
                name: "Inventario");

            migrationBuilder.DropTable(
                name: "Talla");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Cliente");

            migrationBuilder.DropTable(
                name: "Empleado");

            migrationBuilder.DropTable(
                name: "Forma_Pago");

            migrationBuilder.DropTable(
                name: "Prenda");

            migrationBuilder.DropTable(
                name: "Tipo_persona");

            migrationBuilder.DropTable(
                name: "Cargo");

            migrationBuilder.DropTable(
                name: "Municipio");

            migrationBuilder.DropTable(
                name: "Estado");

            migrationBuilder.DropTable(
                name: "Genero");

            migrationBuilder.DropTable(
                name: "Tipo_Proteccion");

            migrationBuilder.DropTable(
                name: "Departamento");

            migrationBuilder.DropTable(
                name: "Tipo_estado");

            migrationBuilder.DropTable(
                name: "Pais");
        }
    }
}
