using SeguroVeiculos.Application.DTOs;

namespace SeguroVeiculos.Application.Services;

public interface ISeguradorService
{
    Task<SeguradorDto> ObterSeguradorPorCpfAsync(string cpf);
}

