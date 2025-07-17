using MediatR;
using SeguroVeiculos.Application.DTOs;
using SeguroVeiculos.Application.Queries;
using SeguroVeiculos.Domain.Interfaces;

namespace SeguroVeiculos.Application.Handlers;

public class ObterRelatorioMediasHandler : IRequestHandler<ObterRelatorioMediasQuery, RelatorioMediasDto>
{
    private readonly ISeguroRepository _seguroRepository;

    public ObterRelatorioMediasHandler(ISeguroRepository seguroRepository)
    {
        _seguroRepository = seguroRepository;
    }

    public async Task<RelatorioMediasDto> Handle(ObterRelatorioMediasQuery request, CancellationToken cancellationToken)
    {
        var mediaValorFinal = await _seguroRepository.GetMediaValorFinalAsync();
        var mediaTaxaRisco = await _seguroRepository.GetMediaTaxaRiscoAsync();
        var mediaPremioRisco = await _seguroRepository.GetMediaPremioRiscoAsync();
        var mediaPremioPuro = await _seguroRepository.GetMediaPremioPuroAsync();
        var mediaPremioComercial = await _seguroRepository.GetMediaPremioComercialAsync();

        return new RelatorioMediasDto
        {
            MediaValorFinal = mediaValorFinal,
            MediaTaxaRisco = mediaTaxaRisco,
            MediaPremioRisco = mediaPremioRisco,
            MediaPremioPuro = mediaPremioPuro,
            MediaPremioComercial = mediaPremioComercial
        };
    }
}

