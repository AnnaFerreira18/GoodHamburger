using Application.DTOs;
using Application.Services;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.Services
{
    public class PedidoServiceTests
    {
        private readonly PedidoService _pedidoService;

        public PedidoServiceTests()
        {
            _pedidoService = new PedidoService();
        }

        [Fact]
        public void CalcularTotais_DeveAplicar20PorcentoDesconto_QuandoTiverTodasAsCategorias()
        {
            var itens = new List<ItemCardapio>
            {
                new ItemCardapio { Categoria = ItemCategoria.Sanduiche, Preco = 5.00m }, // X Burger
                new ItemCardapio { Categoria = ItemCategoria.BatataFrita, Preco = 2.00m },    
                new ItemCardapio { Categoria = ItemCategoria.Refrigerante, Preco = 2.50m }  
            };
            // Subtotal esperado: 9.50. 
            // 20% de desconto (Combo Completo): 1.90. 
            // Total esperado: 7.60.

            var resultado = _pedidoService.CalcularTotais(itens);

            Assert.Equal(9.50m, resultado.Subtotal);
            Assert.Equal(1.90m, resultado.ValorDesconto);
            Assert.Equal(7.60m, resultado.TotalFinal);
        }

        [Fact]
        public void CalcularTotais_DeveAplicar15PorcentoDesconto_QuandoTiverSanduicheERefrigerante()
        {
            var itens = new List<ItemCardapio>
            {
                new ItemCardapio { Categoria = ItemCategoria.Sanduiche, Preco = 5.00m },
                new ItemCardapio { Categoria = ItemCategoria.Refrigerante, Preco = 2.50m }
            };
            // Subtotal: 7.50 | 15% = 1.125 
            var resultado = _pedidoService.CalcularTotais(itens);
            Assert.Equal(1.12m, Math.Round(resultado.ValorDesconto, 2));
        }

        [Fact]
        public void CalcularTotais_DeveAplicar10PorcentoDesconto_QuandoTiverSanduicheEBatata()
        {
            var itens = new List<ItemCardapio>
            {
                new ItemCardapio { Categoria = ItemCategoria.Sanduiche, Preco = 5.00m },
                new ItemCardapio { Categoria = ItemCategoria.BatataFrita, Preco = 2.00m }
            };
            // Subtotal: 7.00 | 10% = 0.70
            var resultado = _pedidoService.CalcularTotais(itens);
            Assert.Equal(0.70m, resultado.ValorDesconto);
        }

        [Fact]
        public void CalcularTotais_NaoDeveAplicarDesconto_QuandoNaoTiverSanduiche()
        {
            var itens = new List<ItemCardapio>
            {
                new ItemCardapio { Categoria = ItemCategoria.BatataFrita, Preco = 2.00m },
                new ItemCardapio { Categoria = ItemCategoria.Refrigerante, Preco = 2.50m }
            };
            var resultado = _pedidoService.CalcularTotais(itens);
            Assert.Equal(0m, resultado.ValorDesconto);
            Assert.Equal(4.50m, resultado.TotalFinal);
        }

        [Fact]
        public void ValidarItensPedido_DeveLancarExcecao_QuandoItensEstiveremDuplicados()
        {
            var itemId = Guid.NewGuid();
            var request = new CriarPedidoRequest
            {
                // Simula um cliente enviando o mesmo ID duas vezes
                ItensIds = new List<Guid> { itemId, itemId }
            };
            var itensDoBanco = new List<ItemCardapio>(); 

            var excecao = Assert.Throws<ArgumentException>(() =>
                _pedidoService.ValidarItensPedido(request, itensDoBanco));

            Assert.Equal("Itens duplicados não são permitidos no mesmo pedido.", excecao.Message);
        }

        [Fact]
        public void ValidarItensPedido_DeveLancarExcecao_QuandoTiverDoisSanduichesDiferentes()
        {
            var request = new CriarPedidoRequest { ItensIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() } };
            var itensDoBanco = new List<ItemCardapio>
            {
                new ItemCardapio { Categoria = ItemCategoria.Sanduiche, Nome = "X-Burger" },
                new ItemCardapio { Categoria = ItemCategoria.Sanduiche, Nome = "X-Egg" }
            };

            Assert.Throws<ArgumentException>(() => _pedidoService.ValidarItensPedido(request, itensDoBanco));
        }
    }
}
