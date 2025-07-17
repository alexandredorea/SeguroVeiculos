using MediatR;
using SeguroVeiculos.Application.Commands;
using SeguroVeiculos.Application.DTOs;
using SeguroVeiculos.Application.Services;
using SeguroVeiculos.Domain.Entities;
using SeguroVeiculos.Domain.Interfaces;
using SeguroVeiculos.Domain.ValueObjects;

namespace SeguroVeiculos.Application.Handlers;

public class CriarSeguroHandler : IRequestHandler<CriarSeguroCommand, SeguroDto>
{
    private readonly ISeguroRepository _seguroRepository;
    private readonly ISeguradorepository _seguradorepository;
    private readonly ISeguradorService _seguradorService;

    public CriarSeguroHandler(
        ISeguroRepository seguroRepository,
        ISeguradorepository seguradorepository,
        ISeguradorService seguradorService)
    {
        _seguroRepository = seguroRepository;
        _seguradorepository = seguradorepository;
        _seguradorService = seguradorService;
    }

    public async Task<SeguroDto> Handle(CriarSeguroCommand request, CancellationToken cancellationToken)
    {
        // Buscar ou criar segurado
        var segurado = await _seguradorepository.GetByCpfAsync(request.CPF);
        
        if (segurado == null)
        {
            // Buscar dados do segurado no serviço externo
            var dadosSegurado = await _seguradorService.ObterSeguradorPorCpfAsync(request.CPF);
            
            segurado = new Segurado
            {
                Nome = dadosSegurado.Nome,
                CPF = dadosSegurado.CPF,
                Idade = dadosSegurado.Idade
            };
            
            segurado = await _seguradorepository.AddAsync(segurado);
        }

        // Criar veículo
        var veiculo = new Veiculo(request.VeiculoValor, request.VeiculoMarcaModelo);

        // Criar seguro
        var seguro = new Seguro(segurado.Id, veiculo);
        seguro = await _seguroRepository.AddAsync(seguro);

        // Retornar DTO
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
            Segurado = new SeguradorDto
            {
                Id = segurado.Id,
                Nome = segurado.Nome,
                CPF = segurado.CPF,
                Idade = segurado.Idade
            }
        };
    }
}

