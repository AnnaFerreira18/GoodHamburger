using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IPedidoService
    {
        ResultadoCalculoPedido CalcularTotais(List<ItemCardapio> itens);
        void ValidarItensPedido(CriarPedidoRequest request, List<ItemCardapio> itensNoBanco);
    }
}
