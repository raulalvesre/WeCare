using FluentValidation;
using WeCare.Application.ViewModels;

namespace WeCare.Application.Validators;

public class AddressViewModelValidator : AbstractValidator<AddressViewModel>
{
    public AddressViewModelValidator()
    {
        RuleFor(x => x.Street)
            .NotEmpty()
            .WithMessage("O endereço da oportunidade não pode ser vazio")
            .MaximumLength(500)
            .WithMessage("O endereço deve ter no máximo 500 caracteres");
        
        RuleFor(x => x.Number)
            .NotEmpty()
            .WithMessage("O número do endereço não pode ser vazio")
            .MaximumLength(30)
            .WithMessage("O número do endereço deve ter no máximo 30 caracteres");
        
        RuleFor(x => x.Complement)
            .MaximumLength(100)
            .WithMessage("O complemento do endereço deve ter no máximo 100 caracteres");
        
        RuleFor(x => x.City)
            .NotEmpty()
            .WithMessage("A cidade não pode ser vazia")
            .MaximumLength(255)
            .WithMessage("A cidade deve ter no máximo 255 caracteres");

        RuleFor(x => x.Neighborhood)
            .MaximumLength(255)
            .WithMessage("O bairro deve ter no máximo 255 caracteres");

        RuleFor(x => x.State)
            .NotEmpty()
            .WithMessage("O estado do endereço não pode ser vazio");

        RuleFor(x => x.PostalCode)
            .NotEmpty()
            .WithMessage("O CEP não pode ser vazio")
            .Matches("[0-9]{5}-[0-9]{3}")
            .WithMessage("CEP inválido");
    }
}