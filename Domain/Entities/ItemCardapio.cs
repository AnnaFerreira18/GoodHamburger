using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Domain.Enums;

namespace Domain.Entities;

public class ItemCardapio
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public decimal Preco { get; set; }
    public ItemCategoria Categoria { get; set; }
    [JsonIgnore]
    public List<Pedido> Pedidos { get; set; } = new();
}
