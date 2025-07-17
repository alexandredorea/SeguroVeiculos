namespace SeguroVeiculos.Domain.Entities;

public class Segurado
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string CPF { get; set; } = string.Empty;
    public int Idade { get; set; }
    
    // Navigation property
    public ICollection<Seguro> Seguros { get; set; } = new List<Seguro>();
}

