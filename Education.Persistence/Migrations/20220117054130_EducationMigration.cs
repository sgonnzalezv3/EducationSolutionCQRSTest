using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Education.Persistence.Migrations
{
    public partial class EducationMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cursos",
                columns: table => new
                {
                    CursoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    FechaPublicacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Precio = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cursos", x => x.CursoId);
                });

            migrationBuilder.InsertData(
                table: "Cursos",
                columns: new[] { "CursoId", "Descripcion", "FechaCreacion", "FechaPublicacion", "Precio", "Titulo" },
                values: new object[] { new Guid("90f405bb-49d1-4382-9cda-38ae8f3757a8"), "Docker", new DateTime(2022, 1, 17, 0, 41, 30, 549, DateTimeKind.Local).AddTicks(4754), new DateTime(2072, 1, 17, 0, 41, 30, 549, DateTimeKind.Local).AddTicks(4756), 560m, "Curso de Docker" });

            migrationBuilder.InsertData(
                table: "Cursos",
                columns: new[] { "CursoId", "Descripcion", "FechaCreacion", "FechaPublicacion", "Precio", "Titulo" },
                values: new object[] { new Guid("d2255fcf-51fb-4b53-8469-494c2ddfa201"), "Curso de Test", new DateTime(2022, 1, 17, 0, 41, 30, 549, DateTimeKind.Local).AddTicks(4682), new DateTime(2024, 1, 17, 0, 41, 30, 549, DateTimeKind.Local).AddTicks(4694), 56m, "Curso de pruebas" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cursos");
        }
    }
}
