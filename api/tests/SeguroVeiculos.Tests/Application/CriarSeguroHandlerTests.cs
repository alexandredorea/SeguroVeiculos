using FluentAssertions;
using Moq;
using SeguroVeiculos.Application.Commands;
using SeguroVeiculos.Application.DTOs;
using SeguroVeiculos.Application.Handlers;
using SeguroVeiculos.Application.Services;
using SeguroVeiculos.Domain.Entities;
using SeguroVeiculos.Domain.Interfaces;

namespace SeguroVeiculos.Tests.Application;

public class CriarSeguroHandlerTests
{
    private readonly Mock<ISeguroRepository> _seguroRepositoryMock;
    private readonly Mock<ISeguradorepository> _seguradorRepositoryMock;
    private readonly Mock<ISeguradorService> _seguradorServiceMock;
    private readonly CriarSeguroHandler _handler;

    public CriarSeguroHandlerTests()
    {
        _seguroRepositoryMock = new Mock<ISeguroRepository>();
        _seguradorRepositoryMock = new Mock<ISeguradorepository>();
        _seguradorServiceMock = new Mock<ISeguradorService>();
        _handler = new CriarSeguroHandler(
            _seguroRepositoryMock.Object,
            _seguradorRepositoryMock.Object,
            _seguradorServiceMock.Object);
    }

    [Fact]
    public async Task Handle_ComSeguradorExistente_DeveCriarSeguroCorretamente()
    {
        // Arrange
        var command = new CriarSeguroCommand
        {
            CPF = "12345678901",
            VeiculoValor = 10000m,
            VeiculoMarcaModelo = "Honda Civic"
        };

        var seguradorExistente = new Segurado
        {
            Id = 1,
            Nome = "João Silva",
            CPF = "12345678901",
            Idade = 30
        };

        var seguroCriado = new Seguro(1, new SeguroVeiculos.Domain.ValueObjects.Veiculo(10000m, "Honda Civic"))
        {
            Id = 1
        };

        _seguradorRepositoryMock
            .Setup(x => x.GetByCpfAsync(command.CPF))
            .ReturnsAsync(seguradorExistente);

        _seguroRepositoryMock
            .Setup(x => x.AddAsync(It.IsAny<Seguro>()))
            .ReturnsAsync(seguroCriado);

        // Act
        var resultado = await _handler.Handle(command, CancellationToken.None);

        // Assert
        resultado.Should().NotBeNull();
        resultado.Id.Should().Be(1);
        resultado.SeguradorId.Should().Be(1);
        resultado.VeiculoValor.Should().Be(10000m);
        resultado.VeiculoMarcaModelo.Should().Be("Honda Civic");
        resultado.ValorFinal.Should().Be(1287.5m); // Valor calculado conforme fórmula
        resultado.Segurado.Should().NotBeNull();
        resultado.Segurado!.Nome.Should().Be("João Silva");

        _seguradorRepositoryMock.Verify(x => x.GetByCpfAsync(command.CPF), Times.Once);
        _seguroRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Seguro>()), Times.Once);
        _seguradorServiceMock.Verify(x => x.ObterSeguradorPorCpfAsync(It.IsAny<string>()), Times.Never);
    }

    [Fact]
    public async Task Handle_ComSeguradorNaoExistente_DeveBuscarNoServicoECriarSegurado()
    {
        // Arrange
        var command = new CriarSeguroCommand
        {
            CPF = "98765432109",
            VeiculoValor = 20000m,
            VeiculoMarcaModelo = "Toyota Corolla"
        };

        var dadosSeguradorServico = new SeguradorDto
        {
            Nome = "Maria Santos",
            CPF = "98765432109",
            Idade = 25
        };

        var seguradorCriado = new Segurado
        {
            Id = 2,
            Nome = "Maria Santos",
            CPF = "98765432109",
            Idade = 25
        };

        var seguroCriado = new Seguro(2, new SeguroVeiculos.Domain.ValueObjects.Veiculo(20000m, "Toyota Corolla"))
        {
            Id = 2
        };

        _seguradorRepositoryMock
            .Setup(x => x.GetByCpfAsync(command.CPF))
            .ReturnsAsync((Segurado?)null);

        _seguradorServiceMock
            .Setup(x => x.ObterSeguradorPorCpfAsync(command.CPF))
            .ReturnsAsync(dadosSeguradorServico);

        _seguradorRepositoryMock
            .Setup(x => x.AddAsync(It.IsAny<Segurado>()))
            .ReturnsAsync(seguradorCriado);

        _seguroRepositoryMock
            .Setup(x => x.AddAsync(It.IsAny<Seguro>()))
            .ReturnsAsync(seguroCriado);

        // Act
        var resultado = await _handler.Handle(command, CancellationToken.None);

        // Assert
        resultado.Should().NotBeNull();
        resultado.Id.Should().Be(2);
        resultado.SeguradorId.Should().Be(2);
        resultado.VeiculoValor.Should().Be(20000m);
        resultado.VeiculoMarcaModelo.Should().Be("Toyota Corolla");
        resultado.ValorFinal.Should().Be(2575m); // Valor calculado conforme fórmula
        resultado.Segurado.Should().NotBeNull();
        resultado.Segurado!.Nome.Should().Be("Maria Santos");

        _seguradorRepositoryMock.Verify(x => x.GetByCpfAsync(command.CPF), Times.Once);
        _seguradorServiceMock.Verify(x => x.ObterSeguradorPorCpfAsync(command.CPF), Times.Once);
        _seguradorRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Segurado>()), Times.Once);
        _seguroRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Seguro>()), Times.Once);
    }
}

