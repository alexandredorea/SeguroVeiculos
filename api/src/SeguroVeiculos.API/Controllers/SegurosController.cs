using MediatR;
using Microsoft.AspNetCore.Mvc;
using SeguroVeiculos.Application.Commands;
using SeguroVeiculos.Application.Queries;

namespace SeguroVeiculos.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SegurosController : ControllerBase
{
    private readonly IMediator _mediator;

    public SegurosController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CriarSeguro([FromBody] CriarSeguroCommand command)
    {
        try
        {
            var resultado = await _mediator.Send(command);
            return Ok(new
            {
                success = true,
                status = 200,
                message = "Seguro criado com sucesso",
                data = resultado,
                error = (object?)null
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new
            {
                success = false,
                status = 400,
                message = "Erro ao criar seguro",
                data = (object?)null,
                error = new[] { new { code = "VALIDATION_ERROR", message = ex.Message } }
            });
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ObterSeguro(int id)
    {
        try
        {
            var resultado = await _mediator.Send(new ObterSeguroQuery(id));
            
            if (resultado == null)
            {
                return NotFound(new
                {
                    success = false,
                    status = 404,
                    message = "Seguro não encontrado",
                    data = (object?)null,
                    error = new[] { new { code = "NOT_FOUND", message = "Seguro não encontrado" } }
                });
            }

            return Ok(new
            {
                success = true,
                status = 200,
                message = "Seguro encontrado",
                data = resultado,
                error = (object?)null
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new
            {
                success = false,
                status = 400,
                message = "Erro ao buscar seguro",
                data = (object?)null,
                error = new[] { new { code = "SEARCH_ERROR", message = ex.Message } }
            });
        }
    }

    [HttpGet("relatorio")]
    public async Task<IActionResult> ObterRelatorioMedias()
    {
        try
        {
            var resultado = await _mediator.Send(new ObterRelatorioMediasQuery());
            
            return Ok(new
            {
                success = true,
                status = 200,
                message = "Relatório gerado com sucesso",
                data = resultado,
                error = (object?)null
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new
            {
                success = false,
                status = 400,
                message = "Erro ao gerar relatório",
                data = (object?)null,
                error = new[] { new { code = "REPORT_ERROR", message = ex.Message } }
            });
        }
    }
}

