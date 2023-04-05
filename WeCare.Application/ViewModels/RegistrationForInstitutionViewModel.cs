using WeCare.Domain.Core;

namespace WeCare.Application.ViewModels;

public class RegistrationForInstitutionViewModel
{
    public long Id { get; set; }
    public OpportunityStatus Status { get; set; }
    public CandidateForRegistrationViewModel Candidate { get; set; }
}