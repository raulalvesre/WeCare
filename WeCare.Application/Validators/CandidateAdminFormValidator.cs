using FluentValidation;
using WeCare.Application.ViewModels;

namespace WeCare.Application.Validators;

public class CandidateAdminFormValidator : AbstractValidator<CandidateAdminForm>
{
    public CandidateAdminFormValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("É necessário um nome")
            .MinimumLength(3)
            .WithMessage("O nome precisa ter no minímo 3 caracteres")
            .MaximumLength(500)
            .WithMessage("O nome pode ter no máximo 500 caracteres");
        
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("É necessário um email")
            .EmailAddress()
            .WithMessage("O email deve ser válido");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("É necessário uma senha")
            .MinimumLength(6)
            .WithMessage("A senha precisa ter no minínmo 6 caracteres")
            .MaximumLength(500)
            .WithMessage("A senha precisa ter no máximo 500 caracteres");
        
        RuleFor(x => x.Telephone)
            .NotEmpty()
            .WithMessage("É necessário um telefone")
            .Must(ValidatorsUtils.IsValidTelephone)
            .WithMessage("Número de telefone inválido");

        RuleFor(x => x.Address)
            .SetValidator(new AddressViewModelValidator());

        RuleFor(x => x.Cpf)
            .NotEmpty()
            .WithMessage("É necessário um CPF")
            .Must(ValidatorsUtils.IsValidCpf)
            .WithMessage("CPF inválido");

        RuleFor(x => x.BirthDate)
            .NotEmpty()
            .WithMessage("É necessário uma data de nascimento")
            .LessThan(DateOnly.FromDateTime(DateTime.Now))
            .WithMessage("Data de nascimento inválida")
            .Must(ValidatorsUtils.IsAdult)
            .WithMessage("Menor de idade");
    }
}