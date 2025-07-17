namespace SeguroVeiculos.Domain.ValueObjects;

public class Veiculo
{
    public decimal Valor { get; private set; }
    public string MarcaModelo { get; private set; }

    public Veiculo(decimal valor, string marcaModelo)
    {
        if (valor <= 0)
            throw new ArgumentException("Valor do veículo deve ser maior que zero", nameof(valor));
        
        if (string.IsNullOrWhiteSpace(marcaModelo))
            throw new ArgumentException("Marca/Modelo do veículo é obrigatório", nameof(marcaModelo));

        Valor = valor;
        MarcaModelo = marcaModelo;
    }

    // Construtor para EF Core
    private Veiculo() { }

    public override bool Equals(object? obj)
    {
        if (obj is not Veiculo other) return false;
        return Valor == other.Valor && MarcaModelo == other.MarcaModelo;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Valor, MarcaModelo);
    }
}

