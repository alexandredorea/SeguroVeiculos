using SeguroVeiculos.Domain.Entities;

namespace SeguroVeiculos.Domain.Interfaces;

public interface ISeguradorepository
{
    Task<Segurado?> GetByIdAsync(int id);
    Task<Segurado?> GetByCpfAsync(string cpf);
    Task<Segurado> AddAsync(Segurado segurado);
    Task UpdateAsync(Segurado segurado);
}

