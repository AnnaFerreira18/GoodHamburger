using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs;
public class PedidoResponse
{
    public Guid Id { get; set; }
    public DateTime DataCriacao { get; set; }
    public decimal TotalFinal { get; set; }
    public bool Cancelado { get; set; }
    public List<ItemPedidoDTO> Itens { get; set; } = new();
}

public class ItemPedidoDTO
{
    public string Nome { get; set; } = string.Empty;
    public decimal Preco { get; set; }
}
