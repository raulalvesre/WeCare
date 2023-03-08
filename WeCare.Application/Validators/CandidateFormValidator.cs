using FluentValidation;
using WeCare.Application.ViewModels;
using WeCare.Infrastructure.Repositories;

namespace WeCare.Application.Validators;

public class CandidateFormValidator : AbstractValidator<CandidateForm>
{
    private readonly CandidateRepository _candidateRepository;
    
    public CandidateFormValidator(CandidateRepository candidateRepository)
    {
        _candidateRepository = candidateRepository;
        
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
            .WithMessage("Email já cadastrado");

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
            .WithMessage("Telefone já cadastrado");;

        RuleFor(x => x.Bio)
            .MaximumLength(1024)
            .WithMessage("Uma bio deve ter no máximo 1024 caracteres");

        RuleFor(x => x.Address)
            .SetValidator(new AddressViewModelValidator());

        RuleFor(x => x.Cpf)
            .NotEmpty()
            .WithMessage("É necessário um CPF")
            .Must(ValidatorsUtils.IsValidCpf)
            .WithMessage("CPF inválido")
            .MustAsync((x, cpf, cT) => UniqueCpf(x, cpf))
            .WithMessage("CPF já cadastrado");;

        RuleFor(x => x.BirthDate)
            .NotEmpty()
            .WithMessage("É necessário uma data de nascimento")
            .LessThan(DateOnly.FromDateTime(DateTime.Now))
            .WithMessage("Data de nascimento inválida")
            .Must(ValidatorsUtils.IsAdult)
            .WithMessage("Menor de idade");
    }
    
    private async Task<bool> UniqueEmail(CandidateForm form, string email)
    {
        return !await _candidateRepository.ExistsByIdNotAndEmail(form.Id, email);
    }
    
    private async Task<bool> UniqueTelephone(CandidateForm form, string telephone)
    {
        return !await _candidateRepository.ExistsByIdNotAndTelephone(form.Id, telephone);
    }
    
    private async Task<bool> UniqueCpf(CandidateForm form, string cpf)
    {
        return !await _candidateRepository.ExistsByIdNotAndCpf(form.Id, cpf);
    }

}