using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities;

public class Pedido
{
    public Guid Id { get; set; }
    public DateTime DataCriacao { get; set; } = DateTime.Now;
    public List<ItemCardapio> Itens { get; set; } = new();

    public decimal Subtotal { get; set; }
    public decimal ValorDesconto { get; set; }
    public decimal TotalFinal { get; set; }

    public bool Cancelado { get; set; } = false;
}
