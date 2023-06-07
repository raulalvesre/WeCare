using WeCare.Domain.Models;

namespace WeCare.Application.ViewModels;

public class CandidateForRegistrationViewModel
{
    public long Id { get; set; }
    public byte[] Photo { get; set; }
    public string Name { get; set; }
    public string Bio { get; set; }
    public DateOnly BirthDate { get; set; }
    public SecretativeAddressViewModel Address { get; set; }
    public IEnumerable<OpportunityCauseViewModel> CausesInterestedIn { get; set; }
    public IEnumerable<QualificationViewModel> Qualifications { get; set; }
}