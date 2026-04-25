using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces;

public interface IPedidoRepository
{
    Task<IEnumerable<Pedido>> ListarAtivosAsync();
    Task<Pedido?> ObterPorIdAsync(Guid id);

    Task AdicionarAsync(Pedido pedido);
    Task AtualizarAsync(Pedido pedido);
}
