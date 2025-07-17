using FluentAssertions;
using SeguroVeiculos.Domain.ValueObjects;

namespace SeguroVeiculos.Tests.Domain;

public class VeiculoTests
{
    [Fact]
    public void CriarVeiculo_ComDadosValidos_DeveCriarCorretamente()
    {
        // Arrange
        var valor = 25000m;
        var marcaModelo = "Honda Civic";

        // Act
        var veiculo = new Veiculo(valor, marcaModelo);

        // Assert
        veiculo.Valor.Should().Be(valor);
        veiculo.MarcaModelo.Should().Be(marcaModelo);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1000)]
    [InlineData(-0.01)]
    public void CriarVeiculo_ComValorInvalido_DeveLancarExcecao(decimal valorInvalido)
    {
        // Arrange
        var marcaModelo = "Honda Civic";

        // Act & Assert
        var act = () => new Veiculo(valorInvalido, marcaModelo);
        act.Should().Throw<ArgumentException>()
           .WithMessage("Valor do veículo deve ser maior que zero*")
           .WithParameterName("valor");
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void CriarVeiculo_ComMarcaModeloInvalido_DeveLancarExcecao(string marcaModeloInvalido)
    {
        // Arrange
        var valor = 25000m;

        // Act & Assert
        var act = () => new Veiculo(valor, marcaModeloInvalido);
        act.Should().Throw<ArgumentException>()
           .WithMessage("Marca/Modelo do veículo é obrigatório*")
           .WithParameterName("marcaModelo");
    }

    [Fact]
    public void Equals_ComVeiculosIguais_DeveRetornarTrue()
    {
        // Arrange
        var veiculo1 = new Veiculo(25000m, "Honda Civic");
        var veiculo2 = new Veiculo(25000m, "Honda Civic");

        // Act & Assert
        veiculo1.Equals(veiculo2).Should().BeTrue();
        (veiculo1 == veiculo2).Should().BeFalse(); // Não implementamos operator ==
    }

    [Fact]
    public void Equals_ComVeiculosDiferentes_DeveRetornarFalse()
    {
        // Arrange
        var veiculo1 = new Veiculo(25000m, "Honda Civic");
        var veiculo2 = new Veiculo(30000m, "Toyota Corolla");

        // Act & Assert
        veiculo1.Equals(veiculo2).Should().BeFalse();
    }

    [Fact]
    public void GetHashCode_ComVeiculosIguais_DeveRetornarHashCodeIgual()
    {
        // Arrange
        var veiculo1 = new Veiculo(25000m, "Honda Civic");
        var veiculo2 = new Veiculo(25000m, "Honda Civic");

        // Act & Assert
        veiculo1.GetHashCode().Should().Be(veiculo2.GetHashCode());
    }

    [Fact]
    public void GetHashCode_ComVeiculosDiferentes_DeveRetornarHashCodeDiferente()
    {
        // Arrange
        var veiculo1 = new Veiculo(25000m, "Honda Civic");
        var veiculo2 = new Veiculo(30000m, "Toyota Corolla");

        // Act & Assert
        veiculo1.GetHashCode().Should().NotBe(veiculo2.GetHashCode());
    }
}

