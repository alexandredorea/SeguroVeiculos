using FluentAssertions;
using SeguroVeiculos.Application.Commands;
using SeguroVeiculos.Application.Validators;

namespace SeguroVeiculos.Tests.Application;

public class CriarSeguroCommandValidatorTests
{
    private readonly CriarSeguroCommandValidator _validator;

    public CriarSeguroCommandValidatorTests()
    {
        _validator = new CriarSeguroCommandValidator();
    }

    [Fact]
    public void Validate_ComDadosValidos_DevePassarNaValidacao()
    {
        // Arrange
        var command = new CriarSeguroCommand
        {
            CPF = "12345678901",
            VeiculoValor = 25000m,
            VeiculoMarcaModelo = "Honda Civic"
        };

        // Act
        var resultado = _validator.Validate(command);

        // Assert
        resultado.IsValid.Should().BeTrue();
        resultado.Errors.Should().BeEmpty();
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Validate_ComCPFVazio_DeveFalharNaValidacao(string cpfInvalido)
    {
        // Arrange
        var command = new CriarSeguroCommand
        {
            CPF = cpfInvalido,
            VeiculoValor = 25000m,
            VeiculoMarcaModelo = "Honda Civic"
        };

        // Act
        var resultado = _validator.Validate(command);

        // Assert
        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().Contain(e => e.PropertyName == "CPF" && e.ErrorMessage == "CPF é obrigatório");
    }

    [Theory]
    [InlineData("123456789")]
    [InlineData("123456789012")]
    [InlineData("1234567890a")]
    public void Validate_ComCPFInvalido_DeveFalharNaValidacao(string cpfInvalido)
    {
        // Arrange
        var command = new CriarSeguroCommand
        {
            CPF = cpfInvalido,
            VeiculoValor = 25000m,
            VeiculoMarcaModelo = "Honda Civic"
        };

        // Act
        var resultado = _validator.Validate(command);

        // Assert
        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().Contain(e => e.PropertyName == "CPF");
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1000)]
    [InlineData(-0.01)]
    public void Validate_ComValorVeiculoInvalido_DeveFalharNaValidacao(decimal valorInvalido)
    {
        // Arrange
        var command = new CriarSeguroCommand
        {
            CPF = "12345678901",
            VeiculoValor = valorInvalido,
            VeiculoMarcaModelo = "Honda Civic"
        };

        // Act
        var resultado = _validator.Validate(command);

        // Assert
        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().Contain(e => e.PropertyName == "VeiculoValor" && 
                                              e.ErrorMessage == "Valor do veículo deve ser maior que zero");
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Validate_ComMarcaModeloVazio_DeveFalharNaValidacao(string marcaModeloInvalido)
    {
        // Arrange
        var command = new CriarSeguroCommand
        {
            CPF = "12345678901",
            VeiculoValor = 25000m,
            VeiculoMarcaModelo = marcaModeloInvalido
        };

        // Act
        var resultado = _validator.Validate(command);

        // Assert
        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().Contain(e => e.PropertyName == "VeiculoMarcaModelo" && 
                                              e.ErrorMessage == "Marca/Modelo do veículo é obrigatório");
    }

    [Fact]
    public void Validate_ComMarcaModeloMuitoLonga_DeveFalharNaValidacao()
    {
        // Arrange
        var marcaModeloLonga = new string('A', 201); // 201 caracteres
        var command = new CriarSeguroCommand
        {
            CPF = "12345678901",
            VeiculoValor = 25000m,
            VeiculoMarcaModelo = marcaModeloLonga
        };

        // Act
        var resultado = _validator.Validate(command);

        // Assert
        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().Contain(e => e.PropertyName == "VeiculoMarcaModelo" && 
                                              e.ErrorMessage == "Marca/Modelo deve ter no máximo 200 caracteres");
    }
}

