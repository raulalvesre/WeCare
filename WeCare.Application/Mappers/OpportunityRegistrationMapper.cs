using WeCare.Application.ViewModels;
using WeCare.Domain.Models;

namespace WeCare.Application.Mappers;

public class OpportunityRegistrationMapper
{
    private readonly CandidateMapper _candidateMapper;
    private readonly VolunteerOpportunityMapper _opportunityMapper;

    public OpportunityRegistrationMapper(CandidateMapper candidateMapper, VolunteerOpportunityMapper opportunityMapper)
    {
        _candidateMapper = candidateMapper;
        _opportunityMapper = opportunityMapper;
    }

    public OpportunityRegistrationWithCandidateViewModel FromModelWithCandidate(OpportunityRegistration model)
    {
        return new OpportunityRegistrationWithCandidateViewModel()
        {
            Id = model.Id,
            Status = model.Status,
            Candidate = _candidateMapper.FromModel(model.Candidate)
        };
    }
    
    public OpportunityRegistrationWithOpportunityViewModel FromModelWithOpportunity(OpportunityRegistration model)
    {
        return new OpportunityRegistrationWithOpportunityViewModel()
        {
            Id = model.Id,
            Status = model.Status, 
            Opportunity = _opportunityMapper.FromModel(model.Opportunity)
        };
    }
}