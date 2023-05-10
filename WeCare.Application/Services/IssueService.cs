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
    private readonly UnitOfWork _unitOfWork;
    private readonly IssueMapper _mapper;
    private readonly ICurrentUser _currentUser;

    public IssueService(IssueRepository issueRepository,
        IssueMapper mapper,
        ICurrentUser currentUser, 
        UserRepository userRepository, 
        UnitOfWork unitOfWork)
    {
        _issueRepository = issueRepository;
        _mapper = mapper;
        _currentUser = currentUser;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
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

        var reportedUser = await _userRepository.GetById(form.ReportedUserId);
        if (reportedUser is null)
            throw new NotFoundException("Usuário reportado não encontrado");

        var reporter = await _userRepository.GetById(_currentUser.GetUserId());
        var issueReport = _mapper.ToModel(form, reportedUser, reporter);

        await _issueRepository.Save(issueReport);
        await _unitOfWork.SaveAsync();

        return _mapper.FromModel(issueReport);
    }

    public async Task Update(long id, IssueReportForm form)
    {
        var validationResult = await form.ValidateAsync();
        if (!validationResult.IsValid)
            throw new BadRequestException(validationResult.Errors);
        
        if (form.ReportedUserId == _currentUser.GetUserId())
            throw new BadRequestException("Você não pode se reportar");

        var issueReport = await _issueRepository.GetById(id);
        if (issueReport is null)
            throw new NotFoundException("Relato de problema não encontrado");

        if (_currentUser.IsInRole("ADMIN") && issueReport.ReporterId != _currentUser.GetUserId())
            throw new UnauthorizedException("Você não tem permissão para editar esse relato de problema");
        
        var reportedUser = await _userRepository.GetById(form.ReportedUserId);
        if (reportedUser is null)
            throw new NotFoundException("Usuário reportado não encontrado");

        var reporter = await _userRepository.GetById(_currentUser.GetUserId());
        
        _mapper.Merge(issueReport, form, reportedUser, reporter);
        
        await _issueRepository.Update(issueReport);
        await _unitOfWork.SaveAsync();
    }

    public async Task Delete(long id)
    {
        var issueReport = await _issueRepository.GetById(id);
        if (issueReport is null)
            throw new NotFoundException("Relato de problema não encontrado");
        
        if (!_currentUser.IsInRole("ADMIN") && issueReport.ReporterId != _currentUser.GetUserId())
            throw new UnauthorizedException("Você não tem permissão para editar esse relato de problema");

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

        issueReport.ResolutionNotes = form.ResolutionNotes;
        issueReport.Status = CLOSED;
    }
}