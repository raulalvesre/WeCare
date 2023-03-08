using FluentValidation;
using WeCare.Application.ViewModels;
using WeCare.Infrastructure.Repositories;

namespace WeCare.Application.Validators;

public class InstitutionAdminFormValidator : AbstractValidator<InstitutionAdminForm>
{
    private readonly InstitutionRepository _institutionRepository;
    
    public InstitutionAdminFormValidator(InstitutionRepository institutionRepository)
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

    private async Task<bool> UniqueEmail(InstitutionAdminForm form, string email)
    {
        return !await _institutionRepository.ExistsByIdNotAndEmail(form.Id, email);
    }
    
    private async Task<bool> UniqueTelephone(InstitutionAdminForm form, string telephone)
    {
        return !await _institutionRepository.ExistsByIdNotAndTelephone(form.Id, telephone);
    }
    
    private async Task<bool> UniqueCnpj(InstitutionAdminForm form, string cnpj)
    {
        return !await _institutionRepository.ExistsByIdNotAndCnpj(form.Id, cnpj);
    }
}