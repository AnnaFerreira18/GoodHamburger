using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces;

public interface IItemCardapioRepository
{
    Task<IEnumerable<ItemCardapio>> ListarTodosAsync();
    Task<List<ItemCardapio>> ObterItensPorIdsAsync(List<Guid> ids);
}
