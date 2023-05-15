using Microsoft.EntityFrameworkCore;
using WeCare.Application.Exceptions;
using WeCare.Application.Interfaces;
using WeCare.Application.Mappers;
using WeCare.Application.SearchParams;
using WeCare.Application.ViewModels;
using WeCare.Domain.Core;
using WeCare.Infrastructure;
using WeCare.Infrastructure.Repositories;
using static WeCare.Domain.Core.IssueStatus;

namespace WeCare.Application.Services;

public class IssueService
{
    private readonly IssueRepository _issueRepository;
    private readonly UserRepository _userRepository;
    private readonly IssueMessageRepository _issueMessageRepository;
    private readonly OpportunityRegistrationRepository _opportunityRegistrationRepository;
    private readonly VolunteerOpportunityRepository _volunteerOpportunityRepository;
    private readonly UnitOfWork _unitOfWork;
    private readonly IssueMapper _mapper;
    private readonly ICurrentUser _currentUser;
    private readonly IssueMessageMapper _issueMessageMapper;

    public IssueService(IssueRepository issueRepository,
        IssueMapper mapper,
        ICurrentUser currentUser,
        UserRepository userRepository,
        UnitOfWork unitOfWork,
        IssueMessageMapper issueMessageMapper,
        IssueMessageRepository issueMessageRepository,
        OpportunityRegistrationRepository opportunityRegistrationRepository,
        VolunteerOpportunityRepository volunteerOpportunityRepository)
    {
        _issueRepository = issueRepository;
        _mapper = mapper;
        _currentUser = currentUser;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _issueMessageMapper = issueMessageMapper;
        _issueMessageRepository = issueMessageRepository;
        _opportunityRegistrationRepository = opportunityRegistrationRepository;
        _volunteerOpportunityRepository = volunteerOpportunityRepository;
    }

    public async Task<IssueReportViewModel> GetById(long id)
    {
        var issueReport = await _issueRepository.GetById(id);
        if (issueReport is null)
            throw new BadRequestException("Relato de problema não encontrado");

        return _mapper.FromModel(issueReport);
    }

    public async Task<Pagination<IssueReportViewModel>> GetPage(IssueSearchParams searchParams)
    {
        var reports = await _issueRepository.Paginate(searchParams);
        return new Pagination<IssueReportViewModel>(
            reports.PageNumber,
            reports.PageSize,
            reports.TotalCount,
            reports.TotalPages,
            reports.Data.Select(x => _mapper.FromModel(x)));
    }

    public async Task<IssueReportViewModel> Save(IssueReportForm form)
    {
        var validationResult = await form.ValidateAsync();
        if (!validationResult.IsValid)
            throw new BadRequestException(validationResult.Errors);

        if (form.ReportedUserId == _currentUser.GetUserId())
            throw new BadRequestException("Você não pode se reportar");

        var opportunity = await _volunteerOpportunityRepository.GetByIdAsync(form.OpportunityId);
        if (opportunity is null)
            throw new NotFoundException("Oportunidade não encontrada");

        if (_currentUser.IsInRole("INSTITUTION"))
        {
            var candidateIsRegisteredInOpportunity = await _opportunityRegistrationRepository.Query
                .AnyAsync(x => x.OpportunityId == form.OpportunityId && x.CandidateId == form.ReportedUserId);

            if (!candidateIsRegisteredInOpportunity)
            {
                throw new UnprocessableEntityException("O candidato reportado não está cadastrado na oportunidade");
            }
        }

        if (_currentUser.IsInRole("CANDIDATE"))
        {
            var reportedInstitutionOwnsOpportunity = await _volunteerOpportunityRepository.Query
                .AnyAsync(x => x.InstitutionId == form.ReportedUserId);

            if (!reportedInstitutionOwnsOpportunity)
            {
                throw new UnprocessableEntityException("Instituição reportada não é dono da oportunidade");
            }
        }

        var reportedUser = await _userRepository.GetByIdAsync(form.ReportedUserId);
        if (reportedUser is null)
            throw new NotFoundException("Usuário reportado não encontrado");

        var userAlreadyReportedUserInOpportunity = await _issueRepository.Query
            .AnyAsync(x => x.OpportunityId == form.OpportunityId && x.ReportedUserId == form.ReportedUserId);
        if (userAlreadyReportedUserInOpportunity)
            throw new ConflictException("Você ja denunciou esse usuário nessa mesma oportunidade");

        var reporter = await _userRepository.GetByIdAsync(_currentUser.GetUserId());
        var issueReport = _mapper.ToModel(form, reportedUser, reporter, opportunity);

        await _issueRepository.Save(issueReport);
        await _unitOfWork.SaveAsync();

        return _mapper.FromModel(issueReport);
    }

    // public async Task Update(long id, IssueReportForm form)
    // {
    //     var validationResult = await form.ValidateAsync();
    //     if (!validationResult.IsValid)
    //         throw new BadRequestException(validationResult.Errors);
    //
    //     if (form.ReportedUserId == _currentUser.GetUserId())
    //         throw new BadRequestException("Você não pode se reportar");
    //
    //     var issueReport = await _issueRepository.GetById(id);
    //     if (issueReport is null)
    //         throw new NotFoundException("Relato de problema não encontrado");
    //     
    //     if (_currentUser.IsInRole("ADMIN") && issueReport.ReporterId != _currentUser.GetUserId())
    //         throw new UnauthorizedException("Você não tem permissão para editar esse relato de problema");
    //     
    //     if (_currentUser.IsInRole("INSTITUTION"))
    //     {
    //         var candidateIsNotRegisteredInOpportunity = await _opportunityRegistrationRepository.Query
    //             .AnyAsync(x => x.CandidateId == form.ReportedUserId);
    //             
    //         if (candidateIsNotRegisteredInOpportunity)
    //         {
    //             throw new UnprocessableEntityException("O usuário reportado não está cadastrado na oportunidade");
    //         }
    //     }
    //     
    //     if (_currentUser.IsInRole("CANDIDATE"))
    //     {
    //         var reportedInstituionOwnsOpportunity = await _volunteerOpportunityRepository.Query
    //             .AnyAsync(x => x.InstitutionId == form.ReportedUserId);
    //
    //         if (reportedInstituionOwnsOpportunity)
    //         {
    //             throw new UnprocessableEntityException("Usuário reportado não é dono da oportunidade");
    //
    //         }
    //     }
    //
    //     var reportedUser = await _userRepository.GetByIdAsync(form.ReportedUserId);
    //     if (reportedUser is null)
    //         throw new NotFoundException("Usuário reportado não encontrado");
    //
    //     var reporter = await _userRepository.GetByIdAsync(_currentUser.GetUserId());
    //
    //     _mapper.Merge(issueReport, form, reportedUser, reporter);
    //
    //     await _issueRepository.Update(issueReport);
    //     await _unitOfWork.SaveAsync();
    // }

    public async Task Delete(long id)
    {
        var issueReport = await _issueRepository.GetById(id);
        if (issueReport is null)
            throw new NotFoundException("Relato de problema não encontrado");

        if (!_currentUser.IsInRole("ADMIN") && issueReport.ReporterId != _currentUser.GetUserId())
            throw new UnauthorizedException("Você não tem permissão para deletar esse relato de problema");

        await _issueRepository.Remove(issueReport);
        await _unitOfWork.SaveAsync();
    }

    public async Task ResolveIssue(long id, IssueResolutionForm form)
    {
        if (!_currentUser.IsInRole("ADMIN"))
            throw new UnauthorizedException("Você não tem permissão para resolver esse relato de problema");

        var validationResult = await form.ValidateAsync();
        if (!validationResult.IsValid)
            throw new BadRequestException(validationResult.Errors);

        var issueReport = await _issueRepository.GetById(id);
        if (issueReport is null)
            throw new NotFoundException("Relato de problema não encontrado");

        if (issueReport.IsClosed())
        {
            throw new ConflictException("Relato de problema já resolvido");
        }

        issueReport.Resolve(form.ResolutionNotes, _currentUser.GetUserId());

        await _issueRepository.Update(issueReport);
        await _unitOfWork.SaveAsync();
    }

    public async Task CreateMessage(long issueId, IssueMessageForm form)
    {
        var validationResult = await form.ValidateAsync();
        if (!validationResult.IsValid)
            throw new BadRequestException(validationResult.Errors);

        var issueReport = await _issueRepository.GetById(issueId);
        if (issueReport is null)
            throw new NotFoundException("Relato de problema não encontrado");

        if (issueReport.IsClosed())
            throw new UnprocessableEntityException("Relato de problema já foi resolvido");

        if (!_currentUser.IsInRole("ADMIN") && _currentUser.GetUserId() != issueReport.ReporterId)
            throw new UnauthorizedException("Você não tem permissão para enviar mensagens nesse relato de problema");

        var issueMessage = _issueMessageMapper.ToModel(issueId, form, _currentUser.GetUserId());
        await _issueMessageRepository.Save(issueMessage);
        await _unitOfWork.SaveAsync();
    }

    public async Task<Pagination<IssueMessageViewModel>> GetMessagesPage(IssueMessageSearchParams searchParams)
    {
        var modelPage = await _issueMessageRepository.Paginate(searchParams);
        return new Pagination<IssueMessageViewModel>
        (
            modelPage.PageNumber, 
            modelPage.PageSize,
            modelPage.TotalCount,
            modelPage.TotalPages,
            modelPage.Data.Select(x => new IssueMessageViewModel(x)));
    }
}