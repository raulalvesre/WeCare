using WeCare.Application.ViewModels;
using WeCare.Domain.Models;

namespace WeCare.Application.Validators;

public class ParticipationCertificateViewModel
{
    public long Id { get; set; }
    public RegistrationViewModel Registration { get; set; }
    public string AuthenticityCode { get; set; }
    public IEnumerable<string> DisplayedQualifications { get; set; }
    public DateTime CreationDate { get; set; }
    
    public ParticipationCertificateViewModel(ParticipationCertificate certificate)
    {
        Id = certificate.Id;
        Registration = new RegistrationViewModel(certificate.Registration);
        DisplayedQualifications = certificate.DisplayedQualifications.Select(x => x.Name);
        AuthenticityCode = certificate.AuthenticityCode;
        CreationDate = certificate.CreationDate;
    }
}