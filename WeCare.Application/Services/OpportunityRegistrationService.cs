using Microsoft.EntityFrameworkCore;
using WeCare.Application.Exceptions;
using WeCare.Application.Interfaces;
using WeCare.Domain.Models;
using WeCare.Infrastructure;
using WeCare.Infrastructure.Repositories;

namespace WeCare.Application.Services;

public class OpportunityRegistrationService
{
    private readonly VolunteerOpportunityRepository _volunteerOpportunityRepository;
    private readonly OpportunityRegistrationRepository _registrationRepository;
    private readonly UnitOfWork _unitOfWork;

    public OpportunityRegistrationService(VolunteerOpportunityRepository volunteerOpportunityRepository,
        OpportunityRegistrationRepository registrationRepository,
        UnitOfWork unitOfWork)
    {
        _volunteerOpportunityRepository = volunteerOpportunityRepository;
        _registrationRepository = registrationRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Pagination<
    
    public async Task RegisterCandidate(long opportunityId, long candidateId)
    {
        var opportunity = await _volunteerOpportunityRepository.Query
            .Where(x => x.Id == opportunityId)
            .FirstOrDefaultAsync();
        
        if (opportunity is null)
            throw new NotFoundException($"Oportunidade com id={opportunityId} não encontrada");

        var candidateAlreadyRegistered = await _registrationRepository.Query
            .AnyAsync(x => x.OpportunityId == opportunityId && x.CandidateId == candidateId);

        if (candidateAlreadyRegistered)
            throw new ConflictException("Candidato já registrado");
        
        var opportunityRegistration = new OpportunityRegistration
        {
            OpportunityId = opportunityId,
            CandidateId = candidateId
        };

        await _registrationRepository.Add(opportunityRegistration);
        await _unitOfWork.SaveAsync();
    }
}