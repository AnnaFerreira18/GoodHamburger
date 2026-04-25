using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs;

public class CriarPedidoRequest
{
    public List<Guid> ItensIds { get; set; } = new();
}
