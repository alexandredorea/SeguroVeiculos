using MediatR;
using SeguroVeiculos.Application.DTOs;
using SeguroVeiculos.Application.Queries;
using SeguroVeiculos.Domain.Interfaces;

namespace SeguroVeiculos.Application.Handlers;

public class ObterSeguroHandler : IRequestHandler<ObterSeguroQuery, SeguroDto?>
{
    private readonly ISeguroRepository _seguroRepository;

    public ObterSeguroHandler(ISeguroRepository seguroRepository)
    {
        _seguroRepository = seguroRepository;
    }

    public async Task<SeguroDto?> Handle(ObterSeguroQuery request, CancellationToken cancellationToken)
    {
        var seguro = await _seguroRepository.GetByIdAsync(request.Id);
        
        if (seguro == null)
            return null;

        return new SeguroDto
        {
            Id = seguro.Id,
            SeguradorId = seguro.SeguradorId,
            VeiculoValor = seguro.Veiculo.Valor,
            VeiculoMarcaModelo = seguro.Veiculo.MarcaModelo,
            TaxaRisco = seguro.TaxaRisco,
            PremioRisco = seguro.PremioRisco,
            PremioPuro = seguro.PremioPuro,
            PremioComercial = seguro.PremioComercial,
            ValorFinal = seguro.ValorFinal,
            DataCriacao = seguro.DataCriacao,
            Segurado = seguro.Segurado != null ? new SeguradorDto
            {
                Id = seguro.Segurado.Id,
                Nome = seguro.Segurado.Nome,
                CPF = seguro.Segurado.CPF,
                Idade = seguro.Segurado.Idade
            } : null
        };
    }
}

