using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ItensCardapio",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    Preco = table.Column<decimal>(type: "TEXT", nullable: false),
                    Categoria = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItensCardapio", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pedidos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Subtotal = table.Column<decimal>(type: "TEXT", nullable: false),
                    ValorDesconto = table.Column<decimal>(type: "TEXT", nullable: false),
                    TotalFinal = table.Column<decimal>(type: "TEXT", nullable: false),
                    Cancelado = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedidos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PedidoItens",
                columns: table => new
                {
                    ItensId = table.Column<Guid>(type: "TEXT", nullable: false),
                    PedidosId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidoItens", x => new { x.ItensId, x.PedidosId });
                    table.ForeignKey(
                        name: "FK_PedidoItens_ItensCardapio_ItensId",
                        column: x => x.ItensId,
                        principalTable: "ItensCardapio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PedidoItens_Pedidos_PedidosId",
                        column: x => x.PedidosId,
                        principalTable: "Pedidos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ItensCardapio",
                columns: new[] { "Id", "Categoria", "Nome", "Preco" },
                values: new object[,]
                {
                    { new Guid("1176047c-658b-49fc-9168-96144b202457"), 1, "X Bacon", 7.00m },
                    { new Guid("22f462a7-5827-4638-89c0-141a20689b94"), 2, "Batata frita", 2.00m },
                    { new Guid("79402543-5242-4f36-963d-4959146f338d"), 3, "Refrigerante", 2.50m },
                    { new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b35"), 1, "X Burger", 5.00m },
                    { new Guid("da2fd609-6684-488b-830c-8f84830e0471"), 1, "X Egg", 4.50m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PedidoItens_PedidosId",
                table: "PedidoItens",
                column: "PedidosId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PedidoItens");

            migrationBuilder.DropTable(
                name: "ItensCardapio");

            migrationBuilder.DropTable(
                name: "Pedidos");
        }
    }
}
