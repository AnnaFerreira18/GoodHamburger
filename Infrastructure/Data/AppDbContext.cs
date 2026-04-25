using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
namespace Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<ItemCardapio> ItensCardapio { get; set; }
    public DbSet<Pedido> Pedidos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Preenchendo banco com cardápio
        modelBuilder.Entity<ItemCardapio>().HasData(
            new ItemCardapio { Id = Guid.Parse("d28888e9-2ba9-473a-a40f-e38cb54f9b35"), Nome = "X Burger", Preco = 5.00m, Categoria = ItemCategoria.Sanduiche },
            new ItemCardapio { Id = Guid.Parse("da2fd609-6684-488b-830c-8f84830e0471"), Nome = "X Egg", Preco = 4.50m, Categoria = ItemCategoria.Sanduiche },
            new ItemCardapio { Id = Guid.Parse("1176047c-658b-49fc-9168-96144b202457"), Nome = "X Bacon", Preco = 7.00m, Categoria = ItemCategoria.Sanduiche },
            new ItemCardapio { Id = Guid.Parse("22f462a7-5827-4638-89c0-141a20689b94"), Nome = "Batata frita", Preco = 2.00m, Categoria = ItemCategoria.BatataFrita },
            new ItemCardapio { Id = Guid.Parse("79402543-5242-4f36-963d-4959146f338d"), Nome = "Refrigerante", Preco = 2.50m, Categoria = ItemCategoria.Refrigerante }
        );
    }
}
