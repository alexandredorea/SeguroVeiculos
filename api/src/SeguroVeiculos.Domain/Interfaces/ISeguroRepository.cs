using SeguroVeiculos.Domain.Entities;

namespace SeguroVeiculos.Domain.Interfaces;

public interface ISeguroRepository
{
    Task<Seguro?> GetByIdAsync(int id);
    Task<IEnumerable<Seguro>> GetAllAsync();
    Task<Seguro> AddAsync(Seguro seguro);
    Task UpdateAsync(Seguro seguro);
    Task DeleteAsync(int id);
    Task<decimal> GetMediaValorFinalAsync();
    Task<decimal> GetMediaTaxaRiscoAsync();
    Task<decimal> GetMediaPremioRiscoAsync();
    Task<decimal> GetMediaPremioPuroAsync();
    Task<decimal> GetMediaPremioComercialAsync();
}

