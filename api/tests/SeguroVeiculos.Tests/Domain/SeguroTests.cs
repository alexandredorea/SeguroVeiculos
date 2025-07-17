using FluentAssertions;
using SeguroVeiculos.Domain.Entities;
using SeguroVeiculos.Domain.ValueObjects;

namespace SeguroVeiculos.Tests.Domain;

public class SeguroTests
{
    [Fact]
    public void CalcularSeguro_ComValorVeiculo10000_DeveCalcularCorretamente()
    {
        // Arrange
        var valorVeiculo = 10000m;
        var marcaModelo = "Honda Civic";
        var seguradorId = 1;
        var veiculo = new Veiculo(valorVeiculo, marcaModelo);

        // Act
        var seguro = new Seguro(seguradorId, veiculo);

        // Assert
        // Taxa de Risco = (10000 * 5) / (2 * 10000) = 50000 / 20000 = 2.5
        seguro.TaxaRisco.Should().Be(2.5m);
        
        // Prêmio de Risco = 2.5 * 10000 = 25000, mas como é percentual: 2.5% * 10000 = 250
        seguro.PremioRisco.Should().Be(25000m); // 2.5 * 10000
        
        // Prêmio Puro = 25000 * (1 + 0.03) = 25000 * 1.03 = 25750
        seguro.PremioPuro.Should().Be(25750m);
        
        // Prêmio Comercial = 0.05 * 25750 = 1287.5
        seguro.PremioComercial.Should().Be(1287.5m);
        
        // Valor Final = Prêmio Comercial
        seguro.ValorFinal.Should().Be(1287.5m);
    }

    [Fact]
    public void CalcularSeguro_ComValorVeiculo50000_DeveCalcularCorretamente()
    {
        // Arrange
        var valorVeiculo = 50000m;
        var marcaModelo = "Toyota Corolla";
        var seguradorId = 1;
        var veiculo = new Veiculo(valorVeiculo, marcaModelo);

        // Act
        var seguro = new Seguro(seguradorId, veiculo);

        // Assert
        // Taxa de Risco = (50000 * 5) / (2 * 50000) = 250000 / 100000 = 2.5
        seguro.TaxaRisco.Should().Be(2.5m);
        
        // Prêmio de Risco = 2.5 * 50000 = 125000
        seguro.PremioRisco.Should().Be(125000m);
        
        // Prêmio Puro = 125000 * 1.03 = 128750
        seguro.PremioPuro.Should().Be(128750m);
        
        // Prêmio Comercial = 0.05 * 128750 = 6437.5
        seguro.PremioComercial.Should().Be(6437.5m);
        
        // Valor Final = Prêmio Comercial
        seguro.ValorFinal.Should().Be(6437.5m);
    }

    [Fact]
    public void CalcularSeguro_ComValorVeiculo20000_DeveCalcularCorretamente()
    {
        // Arrange
        var valorVeiculo = 20000m;
        var marcaModelo = "Volkswagen Golf";
        var seguradorId = 1;
        var veiculo = new Veiculo(valorVeiculo, marcaModelo);

        // Act
        var seguro = new Seguro(seguradorId, veiculo);

        // Assert
        // Taxa de Risco = (20000 * 5) / (2 * 20000) = 100000 / 40000 = 2.5
        seguro.TaxaRisco.Should().Be(2.5m);
        
        // Prêmio de Risco = 2.5 * 20000 = 50000
        seguro.PremioRisco.Should().Be(50000m);
        
        // Prêmio Puro = 50000 * 1.03 = 51500
        seguro.PremioPuro.Should().Be(51500m);
        
        // Prêmio Comercial = 0.05 * 51500 = 2575
        seguro.PremioComercial.Should().Be(2575m);
        
        // Valor Final = Prêmio Comercial
        seguro.ValorFinal.Should().Be(2575m);
    }

    [Fact]
    public void RecalcularSeguro_DeveRecalcularValores()
    {
        // Arrange
        var valorVeiculo = 15000m;
        var marcaModelo = "Ford Focus";
        var seguradorId = 1;
        var veiculo = new Veiculo(valorVeiculo, marcaModelo);
        var seguro = new Seguro(seguradorId, veiculo);

        // Act
        seguro.RecalcularSeguro();

        // Assert
        seguro.TaxaRisco.Should().Be(2.5m);
        seguro.PremioRisco.Should().Be(37500m);
        seguro.PremioPuro.Should().Be(38625m);
        seguro.PremioComercial.Should().Be(1931.25m);
        seguro.ValorFinal.Should().Be(1931.25m);
    }

    [Fact]
    public void CriarSeguro_DeveDefinirDataCriacao()
    {
        // Arrange
        var valorVeiculo = 10000m;
        var marcaModelo = "Honda Civic";
        var seguradorId = 1;
        var veiculo = new Veiculo(valorVeiculo, marcaModelo);
        var dataAntes = DateTime.UtcNow;

        // Act
        var seguro = new Seguro(seguradorId, veiculo);
        var dataDepois = DateTime.UtcNow;

        // Assert
        seguro.DataCriacao.Should().BeAfter(dataAntes.AddSeconds(-1));
        seguro.DataCriacao.Should().BeBefore(dataDepois.AddSeconds(1));
    }

    [Fact]
    public void CriarSeguro_ComVeiculoNulo_DeveLancarExcecao()
    {
        // Arrange
        var seguradorId = 1;
        Veiculo veiculo = null!;

        // Act & Assert
        var act = () => new Seguro(seguradorId, veiculo);
        act.Should().Throw<ArgumentNullException>()
           .WithParameterName("veiculo");
    }
}

