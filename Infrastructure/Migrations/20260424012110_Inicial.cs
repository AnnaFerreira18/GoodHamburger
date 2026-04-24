using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ItemCardapio",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    Preco = table.Column<decimal>(type: "TEXT", nullable: false),
                    Categoria = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemCardapio", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "ItemCardapio",
                columns: new[] { "Id", "Categoria", "Nome", "Preco" },
                values: new object[,]
                {
                    { new Guid("1176047c-658b-49fc-9168-96144b202457"), 1, "X Bacon", 7.00m },
                    { new Guid("22f462a7-5827-4638-89c0-141a20689b94"), 2, "Batata frita", 2.00m },
                    { new Guid("79402543-5242-4f36-963d-4959146f338d"), 3, "Refrigerante", 2.50m },
                    { new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b35"), 1, "X Burger", 5.00m },
                    { new Guid("da2fd609-6684-488b-830c-8f84830e0471"), 1, "X Egg", 4.50m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemCardapio");
        }
    }
}
