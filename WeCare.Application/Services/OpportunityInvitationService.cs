using Microsoft.EntityFrameworkCore;
using WeCare.Application.Exceptions;
using WeCare.Application.Interfaces;
using WeCare.Application.Mappers;
using WeCare.Application.Validators;
using WeCare.Application.ViewModels;
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

    public OpportunityInvitationService(OpportunityInvitationRepository invitationRepository,
        ICurrentUser currentUser,
        OpportunityInvitationMapper invitationMapper,
        OpportunityInvitationFormValidator opportunityInvitationFormValidator, 
        UnitOfWork unitOfWork,
        EmailService emailService, 
        CandidateRepository candidateRepository, 
        InstitutionRepository institutionRepository,
        VolunteerOpportunityRepository opportunityRepository)
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

        var candidateIsAlreadyInvited = await _invitationRepository.Query
            .AnyAsync(x => x.CandidateId == candidate.Id && x.OpportunityId == opportunity.Id);

        if (candidateIsAlreadyInvited)
            throw new ConflictException("Candidato já foi registrado");
        
        var validationResult = await _opportunityInvitationFormValidator.ValidateAsync(form);
        if (!validationResult.IsValid)
            throw new BadRequestException(validationResult.Errors);

        var invitation = _invitationMapper.ToModel(form);
        
        await _invitationRepository.Save(invitation);
        await _emailService.SendEmailAsync(CreateInvitationEmail(candidate, opportunity));

        await _unitOfWork.SaveAsync();

        return _invitationMapper.FromModel(invitation);
    }

    private EmailRequest CreateInvitationEmail(Candidate candidate, VolunteerOpportunity opportunity)
    {
        return new EmailRequest
        {
            ToEmail = candidate.Email,
            Subject = $"WeCare - A empresa {opportunity.Institution.Name} te convidou para uma oportunidade",
            Body = $"Parabéns! Você foi convidade para a seguinte oportunidade: link/opportunity/{opportunity.Id} <botao act> <botao negar>"
        };
    }
    
}