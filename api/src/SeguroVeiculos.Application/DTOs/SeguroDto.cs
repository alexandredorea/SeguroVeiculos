namespace SeguroVeiculos.Application.DTOs;

public class SeguroDto
{
    public int Id { get; set; }
    public int SeguradorId { get; set; }
    public decimal VeiculoValor { get; set; }
    public string VeiculoMarcaModelo { get; set; } = string.Empty;
    public decimal TaxaRisco { get; set; }
    public decimal PremioRisco { get; set; }
    public decimal PremioPuro { get; set; }
    public decimal PremioComercial { get; set; }
    public decimal ValorFinal { get; set; }
    public DateTime DataCriacao { get; set; }
    public SeguradorDto? Segurado { get; set; }
}

public class SeguradorDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string CPF { get; set; } = string.Empty;
    public int Idade { get; set; }
}

public class RelatorioMediasDto
{
    public decimal MediaValorFinal { get; set; }
    public decimal MediaTaxaRisco { get; set; }
    public decimal MediaPremioRisco { get; set; }
    public decimal MediaPremioPuro { get; set; }
    public decimal MediaPremioComercial { get; set; }
}

