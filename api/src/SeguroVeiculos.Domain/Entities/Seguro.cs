using SeguroVeiculos.Domain.ValueObjects;

namespace SeguroVeiculos.Domain.Entities;

public class Seguro
{
    private const decimal MARGEM_SEGURANCA = 0.03m; // 3%
    private const decimal LUCRO = 0.05m; // 5%

    public int Id { get; set; }
    public int SeguradorId { get; set; }
    public Veiculo Veiculo { get; private set; }
    public decimal TaxaRisco { get; private set; }
    public decimal PremioRisco { get; private set; }
    public decimal PremioPuro { get; private set; }
    public decimal PremioComercial { get; private set; }
    public decimal ValorFinal { get; private set; }
    public DateTime DataCriacao { get; set; }

    // Navigation property
    public Segurado Segurado { get; set; } = null!;

    public Seguro(int seguradorId, Veiculo veiculo)
    {
        SeguradorId = seguradorId;
        Veiculo = veiculo ?? throw new ArgumentNullException(nameof(veiculo));
        DataCriacao = DateTime.UtcNow;
        
        CalcularSeguro();
    }

    // Construtor para EF Core
    private Seguro() { }

    private void CalcularSeguro()
    {
        // Taxa de Risco = (Valor do Veículo * 5) / (2 * Valor do Veículo)
        TaxaRisco = (Veiculo.Valor * 5) / (2 * Veiculo.Valor);
        
        // Prêmio de Risco = Taxa de Risco * Valor do Veículo
        PremioRisco = TaxaRisco * Veiculo.Valor;
        
        // Prêmio Puro = Prêmio de Risco * (1 + MARGEM_SEGURANÇA)
        PremioPuro = PremioRisco * (1 + MARGEM_SEGURANCA);
        
        // Prêmio Comercial = LUCRO * Prêmio Puro
        PremioComercial = LUCRO * PremioPuro;
        
        // Valor Final do Seguro
        ValorFinal = PremioComercial;
    }

    public void RecalcularSeguro()
    {
        CalcularSeguro();
    }
}

