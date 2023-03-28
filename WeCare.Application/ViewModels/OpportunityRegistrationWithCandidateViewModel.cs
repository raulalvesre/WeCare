using WeCare.Domain.Core;

namespace WeCare.Application.ViewModels;

public class OpportunityRegistrationWithCandidateViewModel
{
    public long Id { get; set; }
    public OpportunityStatus Status { get; set; }
    public CandidateViewModel Candidate { get; set; }
}