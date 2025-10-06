using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CarteiraVacinacaoApi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vaccine",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    DosesRequired = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vaccine", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VaccineRecord",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PersonId = table.Column<int>(type: "INTEGER", nullable: false),
                    VaccineId = table.Column<int>(type: "INTEGER", nullable: false),
                    DoseNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    AppliedDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VaccineRecord", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VaccineRecord_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VaccineRecord_Vaccine_VaccineId",
                        column: x => x.VaccineId,
                        principalTable: "Vaccine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Person",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Leonardo Gomes" },
                    { 2, "Larissa Vancini" }
                });

            migrationBuilder.InsertData(
                table: "Vaccine",
                columns: new[] { "Id", "DosesRequired", "Name" },
                values: new object[,]
                {
                    { 1, 2, "Covid-19" },
                    { 2, 2, "Febre Amarela" },
                    { 3, 1, "Gripe 2025" }
                });

            migrationBuilder.InsertData(
                table: "VaccineRecord",
                columns: new[] { "Id", "AppliedDate", "DoseNumber", "PersonId", "VaccineId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, 1 },
                    { 2, new DateTime(2025, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 1, 1 },
                    { 3, new DateTime(2025, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 2, 1 },
                    { 4, new DateTime(2025, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 2, 1 },
                    { 5, new DateTime(2025, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_VaccineRecord_PersonId",
                table: "VaccineRecord",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_VaccineRecord_VaccineId",
                table: "VaccineRecord",
                column: "VaccineId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VaccineRecord");

            migrationBuilder.DropTable(
                name: "Person");

            migrationBuilder.DropTable(
                name: "Vaccine");
        }
    }
}
