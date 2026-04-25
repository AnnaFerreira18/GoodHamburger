using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services;

public class PedidoService : IPedidoService
{
    public void ValidarItensPedido(CriarPedidoRequest request, List<ItemCardapio> itensDoBanco)
    {
        //Validação de IDs duplicados
        var temDuplicados = request.ItensIds.Count != request.ItensIds.Distinct().Count();

        if (temDuplicados)
        {
            throw new ArgumentException("Itens duplicados não são permitidos no mesmo pedido.");
        }

        //Verifica se todos os itens enviados realmente existem no cardápio
        if (itensDoBanco.Count != request.ItensIds.Count)
        {
            throw new ArgumentException("Um ou mais itens informados não existem no cardápio.");
        }

        //Validação de quantidade máxima por categoria
        var totalLanches = itensDoBanco.Count(i => i.Categoria == ItemCategoria.Sanduiche);
        var totalAcompanhamentos = itensDoBanco.Count(i => i.Categoria == ItemCategoria.BatataFrita);
        var totalBebidas = itensDoBanco.Count(i => i.Categoria == ItemCategoria.Refrigerante);

        if (totalLanches > 1 || totalAcompanhamentos > 1 || totalBebidas > 1)
        {
            throw new ArgumentException("O pedido pode conter no máximo 1 sanduíche, 1 batata e 1 refrigerante.");
        }
    }

    public ResultadoCalculoPedido CalcularTotais(List<ItemCardapio> itens)
    {
        decimal subtotal = itens.Sum(i => i.Preco);

        //Identifica a composição do pedido para aplicar desconto
        bool temLanche = itens.Any(i => i.Categoria == ItemCategoria.Sanduiche);
        bool temAcompanhamento = itens.Any(i => i.Categoria == ItemCategoria.BatataFrita);
        bool temBebida = itens.Any(i => i.Categoria == ItemCategoria.Refrigerante);

        decimal percentualDesconto = 0m;

        // Combo Completo: Lanche + Batata + Bebida (20%)
        if (temLanche && temAcompanhamento && temBebida)
        {
            percentualDesconto = 0.20m;
        }
        // Combo Médio: Lanche + Bebida (15%)
        else if (temLanche && temBebida)
        {
            percentualDesconto = 0.15m;
        }
        // Combo Econômico: Lanche + Batata (10%)
        else if (temLanche && temAcompanhamento)
        {
            percentualDesconto = 0.10m;
        }

        decimal valorDesconto = subtotal * percentualDesconto;
        decimal totalFinal = subtotal - valorDesconto;

        return new ResultadoCalculoPedido
        {
            Subtotal = subtotal,
            ValorDesconto = valorDesconto,
            TotalFinal = totalFinal
        };
    }
}
