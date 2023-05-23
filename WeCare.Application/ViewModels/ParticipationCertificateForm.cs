using FluentValidation.Results;

namespace WeCare.Application.Validators;

public class ParticipationCertificateForm
{
    public long RegistrationId { get; set; }
    public IEnumerable<long> DisplayedQualifications { get; set; }
}