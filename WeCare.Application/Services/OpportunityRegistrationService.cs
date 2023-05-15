using Microsoft.EntityFrameworkCore;
using WeCare.Application.Exceptions;
using WeCare.Application.Interfaces;
using WeCare.Application.Mappers;
using WeCare.Application.SearchParams;
using WeCare.Application.ViewModels;
using WeCare.Domain.Core;
using WeCare.Domain.Models;
using WeCare.Infrastructure;
using WeCare.Infrastructure.Repositories;
using static WeCare.Domain.Core.RegistrationStatus;

namespace WeCare.Application.Services;

public class OpportunityRegistrationService
{
    private readonly VolunteerOpportunityRepository _volunteerOpportunityRepository;
    private readonly OpportunityRegistrationRepository _registrationRepository;
    private readonly OpportunityRegistrationMapper _mapper;
    private readonly ICurrentUser _currentUser;
    private readonly UnitOfWork _unitOfWork;

    public OpportunityRegistrationService(VolunteerOpportunityRepository volunteerOpportunityRepository,
        OpportunityRegistrationRepository registrationRepository,
        OpportunityRegistrationMapper mapper,
        UnitOfWork unitOfWork, ICurrentUser currentUser)
    {
        _volunteerOpportunityRepository = volunteerOpportunityRepository;
        _registrationRepository = registrationRepository;
        _unitOfWork = unitOfWork;
        _currentUser = currentUser;
        _mapper = mapper;
    }
    
    public async Task<Pagination<AcceptedRegistrationForCandidateViewModel>> GetAcceptedRegistrationsPageForCandidate(OpportunityRegistrationSearchParams searchParams)
    {
        if (searchParams.CandidateId != _currentUser.GetUserId())
            throw new UnauthorizedException("Você não possui permissão");
        
        var page = await _registrationRepository.Paginate(searchParams);
        return new Pagination<AcceptedRegistrationForCandidateViewModel>(
            page.PageNumber, 
            page.PageSize,
            page.TotalCount,
            page.TotalPages,
            page.Data.Select(x => _mapper.FromModelToAcceptedRegistrationForCandidateViewModel(x)));
    }

    public async Task<Pagination<RegistrationForCandidateViewModel>> GetPageForCandidate(OpportunityRegistrationSearchParams searchParams)
    {
        if (searchParams.CandidateId != _currentUser.GetUserId())
            throw new UnauthorizedException("Você não possui permissão");
        
        var page = await _registrationRepository.Paginate(searchParams);
        return new Pagination<RegistrationForCandidateViewModel>(
            page.PageNumber, 
            page.PageSize,
            page.TotalCount,
            page.TotalPages,
            page.Data.Select(x => _mapper.FromModelToRegistrationForCandidateViewModel(x)));
    }
    
    public async Task<Pagination<RegistrationForInstitutionViewModel>> GetPageForInstitution(OpportunityRegistrationSearchParams searchParams)
    {
        var institutionDoestNotOwnOpportunity = !await _volunteerOpportunityRepository.Query
            .AnyAsync(x => x.Id == searchParams.OpportunityId && x.InstitutionId == _currentUser.GetUserId());

        if (institutionDoestNotOwnOpportunity)
            throw new UnauthorizedException("Você não é dona da oportunidade");
        
        var page = await _registrationRepository.Paginate(searchParams);
        return new Pagination<RegistrationForInstitutionViewModel>(
            page.PageNumber, 
            page.PageSize,
            page.TotalCount,
            page.TotalPages,
            page.Data.Select(x => _mapper.FromModelToRegistrationForInstutitionViewModel(x)));
    }
    
    public async Task RegisterCandidate(long opportunityId, long candidateId)
    {
        var opportunity = await _volunteerOpportunityRepository.Query
            .AsNoTracking()
            .Where(x => x.Id == opportunityId)
            .FirstOrDefaultAsync();
        
        if (opportunity is null)
            throw new NotFoundException($"Oportunidade com id={opportunityId} não encontrada");

        var candidateAlreadyRegistered = await _registrationRepository.Query
            .AnyAsync(x => x.OpportunityId == opportunityId && x.CandidateId == candidateId);

        if (candidateAlreadyRegistered)
            throw new ConflictException("Candidato já inscrito");

        if (opportunity.HasAlreadyHappened())
            throw new ConflictException("Oportunidade já aconteceu");
        
        var opportunityRegistration = new OpportunityRegistration
        {
            OpportunityId = opportunityId,
            CandidateId = candidateId
        };

        await _registrationRepository.Save(opportunityRegistration);
        await _unitOfWork.SaveAsync();
    }
    
    public async Task CancelRegistration(long opportunityId, long candidateId)
    {
        var opportunity = await _volunteerOpportunityRepository.Query
            .Where(x => x.Id == opportunityId)
            .FirstOrDefaultAsync();
        
        if (opportunity is null)
            throw new NotFoundException($"Oportunidade com id={opportunityId} não encontrada");

        var registration = await _registrationRepository.Query
            .FirstOrDefaultAsync(x => x.OpportunityId == opportunityId && x.CandidateId == candidateId);

        if (registration is null)
            throw new NotFoundException("Inscrição não encontrada");

        registration.Status = CANCELED;
        await _registrationRepository.Update(registration);
        await _unitOfWork.SaveAsync();
    }

    public async Task AcceptRegistration(long registrationId, string feedbackMessage)
    {
        var registration = await _registrationRepository.Query
            .FirstOrDefaultAsync(x => x.Id == registrationId);

        if (registration is null)
            throw new NotFoundException("Inscrição não encontrada");
        
        if (registration.IsCanceled())
            throw new BadRequestException("Inscrição cancelada");
        
        
        if (registration.AlreadyHasBeenDeniedOrAccepted())
            throw new BadRequestException("Inscrição já foi aceita/recusada");
        
        registration.Status = ACCEPTED;
        registration.FeedbackMessage = feedbackMessage;
        await _registrationRepository.Update(registration);
        await _unitOfWork.SaveAsync();
    }
    
    public async Task DenyRegistration(long registrationId, string feedbackMessage)
    {
        var registration = await _registrationRepository.Query
            .FirstOrDefaultAsync(x => x.Id == registrationId);

        if (registration is null)
            throw new NotFoundException("Inscrição não encontrada");

        if (registration.IsCanceled())
            throw new BadRequestException("Inscrição cancelada");
        
        if (registration.AlreadyHasBeenDeniedOrAccepted())
            throw new BadRequestException("Inscrição já foi aceita/recusada");

        registration.Status = DENIED;
        registration.FeedbackMessage = feedbackMessage;
        await _registrationRepository.Update(registration);
        await _unitOfWork.SaveAsync();
    }

    public async Task DenyAllPending(long opportunityId)
    {
        var opportunity = await _volunteerOpportunityRepository.Query
            .Where(x => x.Id == opportunityId)
            .FirstOrDefaultAsync();
        
        if (opportunity is null)
            throw new NotFoundException($"Oportunidade com id={opportunityId} não encontrada");
        
        var pendingRegistrations = _registrationRepository.Query
            .Where(x => x.OpportunityId == opportunityId && x.Status == PENDING)
            .ToHashSet();
        
        foreach (var registration in pendingRegistrations)
        {
            registration.Status = DENIED;
        }

        _registrationRepository.UpdateAll(pendingRegistrations);
        await _unitOfWork.SaveAsync();
    }

}