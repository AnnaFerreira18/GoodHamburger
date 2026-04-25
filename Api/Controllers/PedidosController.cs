using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PedidosController : ControllerBase
{
    private readonly IPedidoService _pedidoService;
    private readonly IPedidoRepository _pedidoRepo;
    private readonly IItemCardapioRepository _itemRepo;

    public PedidosController(IPedidoRepository pedidoRepo,IItemCardapioRepository itemRepo, IPedidoService pedidoService)
    {
        _pedidoRepo = pedidoRepo;
        _itemRepo = itemRepo;
        _pedidoService = pedidoService;
    }

    // GET: api/pedidos/cardapio
    [HttpGet("cardapio")]
    public async Task<ActionResult<IEnumerable<ItemCardapio>>> GetCardapio()
    {
        var cardapio = await _itemRepo.ListarTodosAsync();
        return Ok(cardapio);
    }

    // GET: api/pedidos
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PedidoResponse>>> ListarTodos()
    {
        var pedidos = await _pedidoRepo.ListarAtivosAsync();

        var response = pedidos.Select(p => new PedidoResponse
        {
            Id = p.Id,
            DataCriacao = p.DataCriacao,
            TotalFinal = p.TotalFinal,
            Cancelado = p.Cancelado,
            Itens = p.Itens.Select(i => new ItemPedidoDTO
            {
                Nome = i.Nome,
                Preco = i.Preco
            }).ToList()
        });

        return Ok(response);
    }

    // GET: api/pedidos/{id}
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Pedido>> ObterPedidoPorId(Guid id)
    {
        var pedido = await _pedidoRepo.ObterPorIdAsync(id);

        if (pedido == null)
        {
            return NotFound(new { mensagem = "Pedido não encontrado." });
        }

        return Ok(pedido);
    }

    // POST: api/pedidos
    [HttpPost]
    public async Task<IActionResult> CriarPedido([FromBody] CriarPedidoRequest request)
    {
        try
        {
            var itensNoBanco = await _itemRepo.ObterItensPorIdsAsync(request.ItensIds);

            _pedidoService.ValidarItensPedido(request, itensNoBanco);
            var calculo = _pedidoService.CalcularTotais(itensNoBanco);

            var novoPedido = new Pedido
            {
                Id = Guid.NewGuid(),
                DataCriacao = DateTime.Now,
                Itens = itensNoBanco,
                Subtotal = calculo.Subtotal,
                ValorDesconto = calculo.ValorDesconto,
                TotalFinal = calculo.TotalFinal,
                Cancelado = false
            };

            await _pedidoRepo.AdicionarAsync(novoPedido);

            var response = new PedidoResponse
            {
                Id = novoPedido.Id,
                DataCriacao = novoPedido.DataCriacao,
                TotalFinal = novoPedido.TotalFinal,
                Cancelado = novoPedido.Cancelado,
                Itens = novoPedido.Itens.Select(i => new ItemPedidoDTO
                {
                    Nome = i.Nome,
                    Preco = i.Preco
                }).ToList()
            };

            return CreatedAtAction(nameof(ObterPedidoPorId), new { id = response.Id }, response);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { erro = ex.Message });
        }
    }

    // PUT: api/pedidos/{id}
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> AtualizarPedido(Guid id, [FromBody] CriarPedidoRequest request)
    {
        var pedidoExistente = await _pedidoRepo.ObterPorIdAsync(id);

        if (pedidoExistente == null)
        {
            return NotFound(new { mensagem = "Pedido não encontrado para atualização." });
        }

        var novosItens = await _itemRepo.ObterItensPorIdsAsync(request.ItensIds);

        try
        {
            _pedidoService.ValidarItensPedido(request, novosItens);
            var calculo = _pedidoService.CalcularTotais(novosItens);

            pedidoExistente.Itens = novosItens;
            pedidoExistente.Subtotal = calculo.Subtotal;
            pedidoExistente.ValorDesconto = calculo.ValorDesconto;
            pedidoExistente.TotalFinal = calculo.TotalFinal;

            await _pedidoRepo.AtualizarAsync(pedidoExistente);

            return Ok(pedidoExistente);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { erro = ex.Message });
        }
    }

    // DELETE: api/pedidos/{id}
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> CancelarPedido(Guid id)
    {
        var pedido = await _pedidoRepo.ObterPorIdAsync(id);

        if (pedido == null)
            return NotFound(new { mensagem = "Pedido não encontrado." });

        pedido.Cancelado = true;

        await _pedidoRepo.AtualizarAsync(pedido);

        return Ok(new { mensagem = "Pedido cancelado com sucesso!" });
    }
}