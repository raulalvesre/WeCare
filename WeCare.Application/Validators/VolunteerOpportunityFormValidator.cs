using FluentValidation;
using WeCare.Application.ViewModels;
using WeCare.Infrastructure.Repositories;

namespace WeCare.Application.Validators;

public class VolunteerOpportunityFormValidator : AbstractValidator<VolunteerOpportunityForm>
{

    private readonly OpportunityCauseRepository _opportunityCauseRepository;
    private readonly QualificationRepository _qualificationRepository;
    
    public VolunteerOpportunityFormValidator(OpportunityCauseRepository opportunityCauseRepository, 
        QualificationRepository qualificationRepository)
    {
        _opportunityCauseRepository = opportunityCauseRepository;
        _qualificationRepository = qualificationRepository;

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
            .GreaterThan(DateTime.Now.AddDays(1))
            .WithMessage("A oportunidade não pode acontecer em menos de 24 horas");

        RuleFor(x => x.Photo)
            .SetValidator(new ImageValidator());

        RuleFor(x => x.Address)
            .SetValidator(new AddressViewModelValidator());

        RuleFor(x => x.Causes)
            .Custom(AllCauseCodesExists);

        RuleFor(x => x.DesirableQualificationsIds)
            .CustomAsync(AllQualificationIdsExists);
    }

    private void AllCauseCodesExists(IEnumerable<string> formCausesCodes, ValidationContext<VolunteerOpportunityForm> context)
    {
        var modelCausesCodes = _opportunityCauseRepository.FindByCodeIn(formCausesCodes)
            .Select(x => x.Code);
        
        foreach (var causeCode in formCausesCodes)
        {
            if (!modelCausesCodes.Contains(causeCode))
            {
                context.AddFailure($"Causa com o código {causeCode} não existe");
            }
        }
    }
    
    private async Task AllQualificationIdsExists(IEnumerable<long> ids, ValidationContext<VolunteerOpportunityForm> context, CancellationToken arg3)
    {
        var qualifications = await _qualificationRepository.FindByIdInAsync(ids);
        var dbIds = qualifications.Select(x => x.Id);

        foreach (var id in ids)
        {
            if (!dbIds.Contains(id))
            {
                context.AddFailure($"Qualificação com o ID {id} não existe");
            }
        }
    }
}