using FluentValidation;
using SeguroVeiculos.Application.Commands;

namespace SeguroVeiculos.Application.Validators;

public class CriarSeguroCommandValidator : AbstractValidator<CriarSeguroCommand>
{
    public CriarSeguroCommandValidator()
    {
        RuleFor(x => x.CPF)
            .NotEmpty()
            .WithMessage("CPF é obrigatório")
            .Length(11)
            .WithMessage("CPF deve ter 11 dígitos")
            .Matches(@"^\d{11}$")
            .WithMessage("CPF deve conter apenas números");

        RuleFor(x => x.VeiculoValor)
            .GreaterThan(0)
            .WithMessage("Valor do veículo deve ser maior que zero");

        RuleFor(x => x.VeiculoMarcaModelo)
            .NotEmpty()
            .WithMessage("Marca/Modelo do veículo é obrigatório")
            .MaximumLength(200)
            .WithMessage("Marca/Modelo deve ter no máximo 200 caracteres");
    }
}

