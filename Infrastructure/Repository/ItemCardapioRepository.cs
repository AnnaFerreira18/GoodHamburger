using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repository;

public class ItemCardapioRepository : IItemCardapioRepository
{
    private readonly AppDbContext _context;
    public ItemCardapioRepository(AppDbContext context) => _context = context;

    public async Task<IEnumerable<ItemCardapio>> ListarTodosAsync()
    {
        return await _context.ItensCardapio.ToListAsync();
    }

    public async Task<List<ItemCardapio>> ObterItensPorIdsAsync(List<Guid> ids)
    {
        return await _context.ItensCardapio
            .Where(i => ids.Contains(i.Id))
            .ToListAsync();
    }
}
