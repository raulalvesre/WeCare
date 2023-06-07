using WeCare.Domain.Core;

namespace WeCare.Application.ViewModels;

public class RegistrationForInstitutionViewModel
{
    public long Id { get; set; }
    public RegistrationStatus Status { get; set; }
    public CandidateForRegistrationViewModel Candidate { get; set; }
    public VolunteerOpportunityViewModel Opportunity { get; set; }
}