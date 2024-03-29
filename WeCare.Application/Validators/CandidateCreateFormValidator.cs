using FluentValidation;
using WeCare.Application.ViewModels;
using WeCare.Infrastructure.Repositories;

namespace WeCare.Application.Validators;

public class CandidateCreateFormValidator : AbstractValidator<CandidateCreateForm>
{
    private readonly CandidateRepository _candidateRepository;
    private readonly QualificationRepository _qualificationRepository;
    private readonly OpportunityCauseRepository _opportunityCauseRepository;

    public CandidateCreateFormValidator(CandidateRepository candidateRepository,
        QualificationRepository qualificationRepository, 
        OpportunityCauseRepository opportunityCauseRepository)
    {
        _candidateRepository = candidateRepository;
        _qualificationRepository = qualificationRepository;
        _opportunityCauseRepository = opportunityCauseRepository;

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
            .WithMessage("Telefone já cadastrado");

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
            .WithMessage("CPF já cadastrado");

        RuleFor(x => x.BirthDate)
            .NotEmpty()
            .WithMessage("É necessário uma data de nascimento")
            .LessThan(DateOnly.FromDateTime(DateTime.Now))
            .WithMessage("Data de nascimento inválida")
            .Must(ValidatorsUtils.IsAdult)
            .WithMessage("Menor de idade");

        RuleFor(x => x.QualificationsIds)
            .CustomAsync(AllQualificationsIdsExists);
        
        RuleFor(x => x.InterestedInCausesIds)
            .CustomAsync(AllCausesIdsExists);
    }

    private async Task<bool> UniqueEmail(CandidateCreateForm createForm, string email)
    {
        return !await _candidateRepository.ExistsByEmail(email);
    }

    private async Task<bool> UniqueTelephone(CandidateCreateForm createForm, string telephone)
    {
        return !await _candidateRepository.ExistsByTelephone(telephone);
    }

    private async Task<bool> UniqueCpf(CandidateCreateForm createForm, string cpf)
    {
        return !await _candidateRepository.ExistsByCpf(cpf);
    }

    private async Task AllQualificationsIdsExists(IEnumerable<long> qualificationIds,
        ValidationContext<CandidateCreateForm> context,
        CancellationToken cancellationToken)
    {
        var qualifications = await _qualificationRepository.FindByIdInAsync(qualificationIds);
        var dbQualificationsIds = qualifications.Select(x => x.Id).ToHashSet();
        
        foreach (var qualificationId in qualificationIds)
        {
            if (!dbQualificationsIds.Contains(qualificationId))
            {
                context.AddFailure($"Qualificação com o ID {qualificationId} não existe");
            }
        }
    }
    
    private async Task AllCausesIdsExists(IEnumerable<long> causesIds,
        ValidationContext<CandidateCreateForm> context,
        CancellationToken cancellationToken)
    {
        var causes = await _opportunityCauseRepository.FindByIdIn(causesIds);
        var dbCausesIds = causes.Select(x => x.Id).ToHashSet();

        foreach (var causeId in causesIds)
        {
            if (!dbCausesIds.Contains(causeId))
            {
                context.AddFailure($"Causa com o ID {causeId} não existe");
            }
        }
    }
}