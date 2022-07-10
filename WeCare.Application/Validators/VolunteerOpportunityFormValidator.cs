using FluentValidation;
using WeCare.Application.ViewModels;

namespace WeCare.Application.Validators;

public class VolunteerOpportunityFormValidator : AbstractValidator<VolunteerOpportunityForm>
{
    public VolunteerOpportunityFormValidator()
    {
        RuleFor(x => x.Id)
            .LessThan(1)
            .WithMessage("Id inválido");

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("O nome da oportunidade não pode ser vazio")
            .MaximumLength(255)
            .WithMessage("O nome da oportunidade deve ter no máximo 255 caracteres");
        
        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("A descrição da oportunidade não pode ser vazio")
            .MaximumLength(500)
            .WithMessage("A descrição da oportunidade deve ter no máximo 500 caracteres");

        RuleFor(x => x.OpportunityDate)
            .NotNull()
            .WithMessage("A data da oportunidade não pode nula")
            .LessThanOrEqualTo(DateTime.Now.AddDays(1))
            .WithMessage("A oportunidade não pode acontecer em menos de 24 horas");
        
        RuleFor(x => x.AddressStreet)
            .NotEmpty()
            .WithMessage("O endereço da oportunidade não pode ser vazio")
            .MaximumLength(500)
            .WithMessage("O endereço da oportunidade deve ter no máximo 500 caracteres");
        
        RuleFor(x => x.AddressNumber)
            .NotEmpty()
            .WithMessage("O número do endereço da oportunidade não pode ser vazio")
            .MaximumLength(30)
            .WithMessage("O número do endereço da oportunidade deve ter no máximo 30 caracteres");
        
        RuleFor(x => x.AddressComplement)
            .NotEmpty()
            .WithMessage("O complemento do endereço da oportunidade não pode ser vazio")
            .MaximumLength(100)
            .WithMessage("O complemento do endereço da oportunidade deve ter no máximo 100 caracteres");
        
        RuleFor(x => x.AddressCity)
            .NotEmpty()
            .WithMessage("A cidade da oportunidade não pode ser vazia")
            .MaximumLength(255)
            .WithMessage("A cidade da oportunidade deve ter no máximo 255 caracteres");

        RuleFor(x => x.AddressNeighborhood)
            .MaximumLength(255)
            .WithMessage("O bairro da oportunidade deve ter no máximo 255 caracteres");

        RuleFor(x => x.AddressState)
            .NotEmpty()
            .WithMessage("O estado do endereço da oportunidade não pode ser vazio");

        RuleFor(x => x.AddressPostalCode)
            .NotEmpty()
            .WithMessage("O CEP da oportunidade não pode ser vazio")
            .Matches("[0-9]{5}-[0-9]{3}")
            .WithMessage("CEP inválido");
    }
}