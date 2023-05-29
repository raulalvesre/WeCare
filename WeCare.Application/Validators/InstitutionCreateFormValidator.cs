using FluentValidation;
using WeCare.Application.ViewModels;
using WeCare.Infrastructure.Repositories;

namespace WeCare.Application.Validators;

public class InstitutionCreateFormValidator : AbstractValidator<InstitutionCreateForm>
{
    private readonly InstitutionRepository _institutionRepository;
    
    public InstitutionCreateFormValidator(InstitutionRepository institutionRepository)
    {
        _institutionRepository = institutionRepository;
        
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
            .WithMessage("O email deve ser válido")
            .MustAsync((x, email, cT) => UniqueEmail(x, email))
            .WithMessage("Telefone já cadastrado");

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
            .WithMessage("Número de telefone inválido")
            .MustAsync((x, telephone, cT) => UniqueTelephone(x, telephone))
            .WithMessage("Telefone já cadastrado");
        
        RuleFor(x => x.Bio)
            .MaximumLength(1024)
            .WithMessage("Uma bio deve ter no máximo 1024 caracteres");

        RuleFor(x => x.Address)
            .SetValidator(new AddressViewModelValidator());

        RuleFor(x => x.Cnpj)
            .NotEmpty()
            .WithMessage("É necessário um CNPJ")
            .Must(ValidatorsUtils.IsValidCnpj)
            .WithMessage("CNPJ inválido")
            .MustAsync((x, cnpj, cT) => UniqueCnpj(x, cnpj))
            .WithMessage("CNPJ já cadastrado");
    }

    private async Task<bool> UniqueEmail(InstitutionCreateForm createForm, string email)
    {
        return !await _institutionRepository.ExistsByEmail(email);
    }
    
    private async Task<bool> UniqueTelephone(InstitutionCreateForm createForm, string telephone)
    {
        return !await _institutionRepository.ExistsByTelephone(telephone);
    }
    
    private async Task<bool> UniqueCnpj(InstitutionCreateForm createForm, string cnpj)
    {
        return !await _institutionRepository.ExistsByCnpj(cnpj);
    }
}