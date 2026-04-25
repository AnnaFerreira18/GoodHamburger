using Application.DTOs;
using Application.Services;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PedidosController : ControllerBase
{
    private readonly PedidoService _pedidoService;
    private readonly AppDbContext _context;

    public PedidosController(AppDbContext context, PedidoService pedidoService)
    {
        _context = context;
        _pedidoService = pedidoService;
    }

    // GET: api/pedidos/cardapio
    [HttpGet("cardapio")]
    public async Task<ActionResult<IEnumerable<ItemCardapio>>> GetCardapio()
    {
        return await _context.ItensCardapio.ToListAsync();
    }

    // GET: api/pedidos
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Pedido>>> ListarTodos()
    {
        return await _context.Pedidos
            .Include(p => p.Itens)
            .OrderByDescending(p => p.DataCriacao)
            .ToListAsync();
    }

    // GET: api/pedidos/{id}
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Pedido>> ObterPedidoPorId(Guid id)
    {
        var pedido = await _context.Pedidos
            .Include(p => p.Itens)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (pedido == null)
        {
            return NotFound(new { mensagem = "Pedido não encontrado." });
        }

        return Ok(pedido);
    }

    // POST: api/pedidos/
    [HttpPost]
    public async Task<IActionResult> CriarPedido([FromBody] CriarPedidoRequest request)
    {
        try
        {
            var itensNoBanco = await _context.ItensCardapio
                .Where(i => request.ItensIds.Contains(i.Id))
                .ToListAsync();

            _pedidoService.ValidarItensPedido(request, itensNoBanco);
            var calculo = _pedidoService.CalcularTotais(itensNoBanco);

            var novoPedido = new Pedido
            {
                Id = Guid.NewGuid(),
                Itens = itensNoBanco,
                Subtotal = calculo.Subtotal,
                ValorDesconto = calculo.ValorDesconto,
                TotalFinal = calculo.TotalFinal
            };

            _context.Pedidos.Add(novoPedido);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(ObterPedidoPorId), new { id = novoPedido.Id }, novoPedido);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { erro = ex.Message });
        }
    }
}