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

    public RegistrationForInstitutionViewModel FromModelToRegistrationForInstutitionViewModel(OpportunityRegistration model)
    {
        return new RegistrationForInstitutionViewModel()
        {
            Id = model.Id,
            Status = model.Status,
            Candidate = _candidateMapper.FromModelToInstitutionRegistrationViewModel(model.Candidate),
            Opportunity = new VolunteerOpportunityViewModel(model.Opportunity)
        };
    }
    
    public RegistrationForCandidateViewModel FromModelToRegistrationForCandidateViewModel(OpportunityRegistration model)
    {
        return new RegistrationForCandidateViewModel()
        {
            Id = model.Id,
            Status = model.Status, 
            Opportunity = _opportunityMapper.FromModelToCandidateRegistrationViewModel(model.Opportunity)
        };
    }
    
    public AcceptedRegistrationForCandidateViewModel FromModelToAcceptedRegistrationForCandidateViewModel(OpportunityRegistration model)
    {
        return new AcceptedRegistrationForCandidateViewModel()
        {
            Id = model.Id,
            Status = model.Status, 
            Opportunity = _opportunityMapper.FromModelToOpportunityForAcceptedRegistrationViewModel(model.Opportunity)
        };
    }
}