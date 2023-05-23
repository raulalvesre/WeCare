namespace WeCare.Domain.Models;

public class ParticipationCertificate
{
    public long Id { get; set; }
    public string AuthenticityCode { get; set; }
    public DateTime CreationDate { get; set; }

    public long RegistrationId { get; set; }
    public OpportunityRegistration? Registration { get; set; }
    public IEnumerable<Qualification> DisplayedQualifications { get; set; }
}