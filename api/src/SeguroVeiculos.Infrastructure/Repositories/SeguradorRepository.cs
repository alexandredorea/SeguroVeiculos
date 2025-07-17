using Microsoft.EntityFrameworkCore;
using SeguroVeiculos.Domain.Entities;
using SeguroVeiculos.Domain.Interfaces;
using SeguroVeiculos.Infrastructure.Data;

namespace SeguroVeiculos.Infrastructure.Repositories;

public class SeguradorRepository : ISeguradorepository
{
    private readonly SeguroVeiculosDbContext _context;

    public SeguradorRepository(SeguroVeiculosDbContext context)
    {
        _context = context;
    }

    public async Task<Segurado?> GetByIdAsync(int id)
    {
        return await _context.Segurados.FindAsync(id);
    }

    public async Task<Segurado?> GetByCpfAsync(string cpf)
    {
        return await _context.Segurados
            .FirstOrDefaultAsync(s => s.CPF == cpf);
    }

    public async Task<Segurado> AddAsync(Segurado segurado)
    {
        _context.Segurados.Add(segurado);
        await _context.SaveChangesAsync();
        return segurado;
    }

    public async Task UpdateAsync(Segurado segurado)
    {
        _context.Segurados.Update(segurado);
        await _context.SaveChangesAsync();
    }
}

