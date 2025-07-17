using MediatR;
using SeguroVeiculos.Application.DTOs;

namespace SeguroVeiculos.Application.Commands;

public class CriarSeguroCommand : IRequest<SeguroDto>
{
    public string CPF { get; set; } = string.Empty;
    public decimal VeiculoValor { get; set; }
    public string VeiculoMarcaModelo { get; set; } = string.Empty;
}

