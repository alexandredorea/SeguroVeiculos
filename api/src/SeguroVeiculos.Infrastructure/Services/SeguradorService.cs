using System.Text.Json;
using Microsoft.Extensions.Configuration;
using SeguroVeiculos.Application.DTOs;
using SeguroVeiculos.Application.Services;

namespace SeguroVeiculos.Infrastructure.Services;

public class SeguradorService : ISeguradorService
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;

    public SeguradorService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _baseUrl = configuration["SeguradorService:BaseUrl"] ?? "http://localhost:3001";
    }

    public async Task<SeguradorDto> ObterSeguradorPorCpfAsync(string cpf)
    {
        try
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/segurados?cpf={cpf}");
            
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var segurados = JsonSerializer.Deserialize<List<SeguradorDto>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                var segurado = segurados?.FirstOrDefault();
                if (segurado != null)
                {
                    return segurado;
                }
            }

            // Se não encontrar no serviço externo, retorna dados padrão
            return new SeguradorDto
            {
                Nome = "Segurado Padrão",
                CPF = cpf,
                Idade = 30
            };
        }
        catch
        {
            // Em caso de erro, retorna dados padrão
            return new SeguradorDto
            {
                Nome = "Segurado Padrão",
                CPF = cpf,
                Idade = 30
            };
        }
    }
}

