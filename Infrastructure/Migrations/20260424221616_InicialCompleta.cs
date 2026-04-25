using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InicialCompleta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pedidos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Subtotal = table.Column<decimal>(type: "TEXT", nullable: false),
                    ValorDesconto = table.Column<decimal>(type: "TEXT", nullable: false),
                    TotalFinal = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedidos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ItensCardapio",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    Preco = table.Column<decimal>(type: "TEXT", nullable: false),
                    Categoria = table.Column<int>(type: "INTEGER", nullable: false),
                    PedidoId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItensCardapio", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItensCardapio_Pedidos_PedidoId",
                        column: x => x.PedidoId,
                        principalTable: "Pedidos",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "ItensCardapio",
                columns: new[] { "Id", "Categoria", "Nome", "PedidoId", "Preco" },
                values: new object[,]
                {
                    { new Guid("1176047c-658b-49fc-9168-96144b202457"), 1, "X Bacon", null, 7.00m },
                    { new Guid("22f462a7-5827-4638-89c0-141a20689b94"), 2, "Batata frita", null, 2.00m },
                    { new Guid("79402543-5242-4f36-963d-4959146f338d"), 3, "Refrigerante", null, 2.50m },
                    { new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b35"), 1, "X Burger", null, 5.00m },
                    { new Guid("da2fd609-6684-488b-830c-8f84830e0471"), 1, "X Egg", null, 4.50m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItensCardapio_PedidoId",
                table: "ItensCardapio",
                column: "PedidoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItensCardapio");

            migrationBuilder.DropTable(
                name: "Pedidos");
        }
    }
}
