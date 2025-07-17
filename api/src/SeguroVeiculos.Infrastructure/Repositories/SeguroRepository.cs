using Microsoft.EntityFrameworkCore;
using SeguroVeiculos.Domain.Entities;
using SeguroVeiculos.Domain.Interfaces;
using SeguroVeiculos.Infrastructure.Data;

namespace SeguroVeiculos.Infrastructure.Repositories;

public class SeguroRepository : ISeguroRepository
{
    private readonly SeguroVeiculosDbContext _context;

    public SeguroRepository(SeguroVeiculosDbContext context)
    {
        _context = context;
    }

    public async Task<Seguro?> GetByIdAsync(int id)
    {
        return await _context.Seguros
            .Include(s => s.Segurado)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<IEnumerable<Seguro>> GetAllAsync()
    {
        return await _context.Seguros
            .Include(s => s.Segurado)
            .ToListAsync();
    }

    public async Task<Seguro> AddAsync(Seguro seguro)
    {
        _context.Seguros.Add(seguro);
        await _context.SaveChangesAsync();
        return seguro;
    }

    public async Task UpdateAsync(Seguro seguro)
    {
        _context.Seguros.Update(seguro);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var seguro = await _context.Seguros.FindAsync(id);
        if (seguro != null)
        {
            _context.Seguros.Remove(seguro);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<decimal> GetMediaValorFinalAsync()
    {
        var seguros = await _context.Seguros.ToListAsync();
        return seguros.Any() ? seguros.Average(s => s.ValorFinal) : 0;
    }

    public async Task<decimal> GetMediaTaxaRiscoAsync()
    {
        var seguros = await _context.Seguros.ToListAsync();
        return seguros.Any() ? seguros.Average(s => s.TaxaRisco) : 0;
    }

    public async Task<decimal> GetMediaPremioRiscoAsync()
    {
        var seguros = await _context.Seguros.ToListAsync();
        return seguros.Any() ? seguros.Average(s => s.PremioRisco) : 0;
    }

    public async Task<decimal> GetMediaPremioPuroAsync()
    {
        var seguros = await _context.Seguros.ToListAsync();
        return seguros.Any() ? seguros.Average(s => s.PremioPuro) : 0;
    }

    public async Task<decimal> GetMediaPremioComercialAsync()
    {
        var seguros = await _context.Seguros.ToListAsync();
        return seguros.Any() ? seguros.Average(s => s.PremioComercial) : 0;
    }
}

