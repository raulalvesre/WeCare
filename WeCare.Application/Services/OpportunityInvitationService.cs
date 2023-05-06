using FluentEmail.Core;
using FluentEmail.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using WeCare.Application.Exceptions;
using WeCare.Application.Interfaces;
using WeCare.Application.Mappers;
using WeCare.Application.SearchParams;
using WeCare.Application.Validators;
using WeCare.Application.ViewModels;
using WeCare.Domain.Core;
using WeCare.Domain.Models;
using WeCare.Infrastructure;
using WeCare.Infrastructure.Repositories;

namespace WeCare.Application.Services;

public class OpportunityInvitationService
{
    private readonly OpportunityInvitationRepository _invitationRepository;
    private readonly CandidateRepository _candidateRepository;
    private readonly VolunteerOpportunityRepository _opportunityRepository;
    private readonly InstitutionRepository _institutionRepository;
    private readonly OpportunityInvitationMapper _invitationMapper;
    private readonly OpportunityInvitationFormValidator _opportunityInvitationFormValidator;
    private readonly UnitOfWork _unitOfWork;
    private readonly EmailService _emailService;
    private readonly ICurrentUser _currentUser;
    private readonly IFluentEmail _fluentEmail;
    private readonly ITemplateRenderer _templateRenderer;
    private readonly OpportunityRegistrationRepository _registrationRepository;

    public OpportunityInvitationService(OpportunityInvitationRepository invitationRepository,
        ICurrentUser currentUser,
        OpportunityInvitationMapper invitationMapper,
        OpportunityInvitationFormValidator opportunityInvitationFormValidator, 
        UnitOfWork unitOfWork,
        EmailService emailService, 
        CandidateRepository candidateRepository, 
        InstitutionRepository institutionRepository,
        VolunteerOpportunityRepository opportunityRepository,
        IFluentEmail fluentEmail,
        ITemplateRenderer templateRenderer,
        OpportunityRegistrationRepository registrationRepository)
    {
        _invitationRepository = invitationRepository;
        _currentUser = currentUser;
        _invitationMapper = invitationMapper;
        _opportunityInvitationFormValidator = opportunityInvitationFormValidator;
        _unitOfWork = unitOfWork;
        _emailService = emailService;
        _candidateRepository = candidateRepository;
        _institutionRepository = institutionRepository;
        _opportunityRepository = opportunityRepository;
        _fluentEmail = fluentEmail;
        _templateRenderer = templateRenderer;
        _registrationRepository = registrationRepository;
    }

    public async Task<OpportunityInvitationViewModel> GetById(long id)
    {
        var invitation = await _invitationRepository.Query
            .FirstOrDefaultAsync(x => x.Id == id);

        if (invitation is null)
            throw new NotFoundException("Convite não encontrado");

        return _invitationMapper.FromModel(invitation);
    }
    
    public async Task<Pagination<OpportunityInvitationViewModel>> GetPage(OpportunityInvitationSearchParams searchParams)
    {
        var invitationPage = await _invitationRepository.Paginate(searchParams);
        return new Pagination<OpportunityInvitationViewModel>(
            invitationPage.PageNumber, 
            invitationPage.PageSize,
            invitationPage.TotalCount,
            invitationPage.TotalPages,
            invitationPage.Data.Select(x => _invitationMapper.FromModel(x)));
    }

    public async Task<OpportunityInvitationViewModel> Save(OpportunityInvitationForm form)
    {
        var candidate = await _candidateRepository.GetById(form.CandidateId);
        if (candidate is null)
            throw new NotFoundException("Candidate não encontrado");

        var opportunity = await _opportunityRepository.Query
            .Include(x => x.Institution)
            .FirstOrDefaultAsync(x => x.Id == form.OpportunityId);
        
        if (opportunity is null)
            throw new NotFoundException("Oportunidade não encontrada");

        if (opportunity.InstitutionId != _currentUser.GetUserId())
            throw new UnauthorizedException("Você não tem permissão para convidar candidatos para essa oportunidade");

        var candidateIsAlreadyInvited = await _invitationRepository.Query
            .AnyAsync(x => x.CandidateId == candidate.Id && x.OpportunityId == opportunity.Id);

        if (candidateIsAlreadyInvited)
            throw new ConflictException("Candidato já foi convidado");
        
        var validationResult = await _opportunityInvitationFormValidator.ValidateAsync(form);
        if (!validationResult.IsValid)
            throw new BadRequestException(validationResult.Errors);

        var invitation = _invitationMapper.ToModel(form);

        var alreadyCreatedRegistration = await _registrationRepository.Query
            .FirstOrDefaultAsync(x => x.CandidateId == form.CandidateId && x.OpportunityId == form.OpportunityId);

        if (alreadyCreatedRegistration is not null)
        {
            invitation.Status = InvitationStatus.ACCEPTED;
            alreadyCreatedRegistration.Status = RegistrationStatus.ACCEPTED;
            await _registrationRepository.Update(alreadyCreatedRegistration);
        }
        
        await _invitationRepository.Save(invitation);
        await SendInvitationEmail(candidate, opportunity, invitation.InvitationMessage);

        await _unitOfWork.SaveAsync();

        return _invitationMapper.FromModel(invitation);
    }

    private async Task SendInvitationEmail(Candidate candidate, VolunteerOpportunity opportunity, string invitationMessage)
    {
        var invitationEmailViewModel = new OpportunityInvitationEmailViewModel
        {
            CandidateName = candidate.Name,
            OpportunityId = opportunity.Id,
            OpportunityName = opportunity.Name,
            InvitationMessage = invitationMessage
        };

        var subject = $"WeCare - A empresa {opportunity.Institution.Name} te convidou para uma oportunidade";
        await _emailService.SendEmailAsync(candidate.Email, subject, nameof(OpportunityInvitation), invitationEmailViewModel);
    }

    public async Task Cancel(long invitationId)
    {
        var invitation = await _invitationRepository.Query
            .Include(x => x.Opportunity)
            .FirstOrDefaultAsync(x => x.Id == invitationId);
        
        if (invitation is null)
            throw new NotFoundException("Convite não encontrado");

        if (invitation.Opportunity.InstitutionId != _currentUser.GetUserId())
            throw new UnauthorizedException("Você não tem permissão para cancelar esse convite");
        
        if (invitation.HasBeenCanceled())
            throw  new ConflictException("Convite já cancelado");

        invitation.Status = InvitationStatus.CANCELED;
        await _invitationRepository.Update(invitation);
        
        var registration = await _registrationRepository.Query
            .FirstOrDefaultAsync(x => x.CandidateId == invitation.CandidateId && x.OpportunityId == invitation.OpportunityId);

        if (registration is not null)
        {
            registration.Status = RegistrationStatus.CANCELED;
        }

        await _unitOfWork.SaveAsync();
    }

    public async Task Accept(long invitationId, long candidateId, OpportunityInvitationResponseForm form)
    {
        var invitation = await _invitationRepository.Query
            .Include(x => x.Candidate)
            .FirstOrDefaultAsync(x => x.Id == invitationId);
        
        if (invitation is null)
            throw new NotFoundException("Convite não encontrado");
        
        if (invitation.CandidateId != candidateId)
            throw new UnauthorizedException("Você não tem permissão para aceitar esse convite");
        
        if (!invitation.IsPending())
            throw new ConflictException("Convite não está mais pendente");
        
        var validationResult = await form.ValidateAsync();
        if (!validationResult.IsValid)
            throw new BadRequestException(validationResult.Errors);

        invitation.Status = InvitationStatus.ACCEPTED;
        invitation.ResponseMessage = form.ResponseMessage;

        var registration = new OpportunityRegistration
        {
            Status = RegistrationStatus.ACCEPTED,
            OpportunityId = invitation.OpportunityId,
            CandidateId = candidateId
        };

        await _invitationRepository.Update(invitation);
        await _registrationRepository.Save(registration);
        await _unitOfWork.SaveAsync();
    }

    public async Task Deny(long invitationId, long candidateId, OpportunityInvitationResponseForm form)
    {
        var invitation = await _invitationRepository.Query
            .Include(x => x.Candidate)
            .FirstOrDefaultAsync(x => x.Id == invitationId);
        
        if (invitation is null)
            throw new NotFoundException("Convite não encontrado");
        
        if (invitation.CandidateId != candidateId)
            throw new UnauthorizedException("Você não tem permissão para aceitar esse convite");
        
        if (!invitation.IsPending())
            throw new ConflictException("Convite não está mais pendente");
        
        var validationResult = await form.ValidateAsync();
        if (!validationResult.IsValid)
            throw new BadRequestException(validationResult.Errors);

        invitation.Status = InvitationStatus.DENIED;
        invitation.ResponseMessage = form.ResponseMessage;

        await _invitationRepository.Update(invitation);
        await _unitOfWork.SaveAsync();
    }
    
}