using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs;

public class ResultadoCalculoPedido
{
    public decimal Subtotal { get; set; }
    public decimal ValorDesconto { get; set; }
    public decimal TotalFinal { get; set; }
}
