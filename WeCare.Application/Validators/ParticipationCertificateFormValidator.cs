using FluentValidation;
using WeCare.Infrastructure.Repositories;

namespace WeCare.Application.Validators;

public class ParticipationCertificateFormValidator : AbstractValidator<ParticipationCertificateForm>
{
    private readonly QualificationRepository _qualificationRepository;
    
    public ParticipationCertificateFormValidator(QualificationRepository qualificationRepository)
    {
        _qualificationRepository = qualificationRepository;
        RuleFor(x => x.RegistrationId)
            .NotNull()
            .WithMessage("É necessário o Id do registro");

        RuleFor(x => x.DisplayedQualifications)
            .CustomAsync(AllQualificationsExists);
    }

    private async Task AllQualificationsExists(IEnumerable<long> ids, ValidationContext<ParticipationCertificateForm> context, CancellationToken arg3)
    {
        var qualifications = await _qualificationRepository.FindByIdInAsync(ids);
        var dbQualificationsIds = qualifications.Select(x => x.Id);
        
        foreach (var id in ids)
        {
            if (!dbQualificationsIds.Contains(id))
            {
                context.AddFailure($"Qualificação com o ID {id} não existe");
            }
        }
    }
}