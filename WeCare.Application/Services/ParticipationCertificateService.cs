using Microsoft.EntityFrameworkCore;
using WeCare.Application.Exceptions;
using WeCare.Application.SearchParams;
using WeCare.Application.Validators;
using WeCare.Domain.Core;
using WeCare.Domain.Models;
using WeCare.Infrastructure;
using WeCare.Infrastructure.Repositories;

namespace WeCare.Application.Services;

public class ParticipationCertificateService
{
    private readonly ParticipationCertificateRepository _certificateRepository;
    private readonly OpportunityRegistrationRepository _registrationRepository;
    private readonly UnitOfWork _unitOfWork;
    private readonly ParticipationCertificateFormValidator _formValidator;
    private readonly QualificationRepository _qualificationRepository;

    public ParticipationCertificateService(ParticipationCertificateRepository certificateRepository,
        OpportunityRegistrationRepository registrationRepository,
        UnitOfWork unitOfWork,
        ParticipationCertificateFormValidator formValidator,
        QualificationRepository qualificationRepository)
    {
        _certificateRepository = certificateRepository;
        _registrationRepository = registrationRepository;
        _unitOfWork = unitOfWork;
        _formValidator = formValidator;
        _qualificationRepository = qualificationRepository;
    }

    public async Task<ParticipationCertificateViewModel> GetById(long id)
    {
        var certificate = await _certificateRepository.Query.Include(x => x.Registration)
            .Include(x => x.DisplayedQualifications)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (certificate is null)
            throw new NotFoundException("Certificado de participação não encontrado");

        return new ParticipationCertificateViewModel(certificate);
    }

    public async Task<Pagination<ParticipationCertificateViewModel>> GetPage(ParticipationCertificateSearchParams searchParams)
    {
        var certificates = await _certificateRepository.Paginate(searchParams);
        return new Pagination<ParticipationCertificateViewModel>(
            certificates.PageNumber, 
            certificates.PageSize,
            certificates.TotalCount,
            certificates.TotalPages,
            certificates.Data.Select(x => new ParticipationCertificateViewModel(x)));
    }

    public async Task<ParticipationCertificateViewModel> Save(ParticipationCertificateForm form)
    {
        var validationResult = await _formValidator.ValidateAsync(form);
        if (!validationResult.IsValid)
            throw new BadRequestException(validationResult.Errors);

        var registration = await _registrationRepository.GetByIdAsync(form.RegistrationId);
        if (registration is null)
            throw new NotFoundException("Inscrição não encontrada.");

        if (registration.IsNotAccepted())
            throw new UnprocessableEntityException("Inscrição nunca foi aceita, não é possível emitir certificado de participação");

        var alreadyExistingParticipationCertificate =
            _certificateRepository.Query.FirstOrDefault(x => x.RegistrationId == form.RegistrationId);
        if (alreadyExistingParticipationCertificate is not null)
            throw new ConflictException("Certificado ja foi emitido");
        
        var authenticityCode = Guid.NewGuid().ToString();
        var certificate = new ParticipationCertificate
        {
            Registration = registration,
            AuthenticityCode = authenticityCode,
            DisplayedQualifications = _qualificationRepository.FindByIdIn(form.DisplayedQualifications),
            CreationDate = DateTime.Now,
        };

        await _certificateRepository.Save(certificate);
        await _unitOfWork.SaveAsync();
        return new ParticipationCertificateViewModel(certificate);
    }

    public ParticipationCertificateViewModel GetByAuthenticityCode(Guid authenticityCode)
    {
        var certificate = _certificateRepository.Query.Include(x => x.Registration)
            .ThenInclude(x => x.Opportunity)
            .ThenInclude(x => x.Causes)
            .Include(x => x.Registration)
            .ThenInclude(x => x.Opportunity)
            .ThenInclude(x => x.Institution)
            .Include(x => x.Registration)
            .ThenInclude(x => x.Candidate)
            .ThenInclude(x => x.CausesCandidateIsInterestedIn)
            .Include(x => x.Registration)
            .ThenInclude(x => x.Candidate)
            .ThenInclude(x => x.Qualifications)
            .Include(x => x.DisplayedQualifications).FirstOrDefault(x => x.AuthenticityCode == authenticityCode.ToString());

        if (certificate is null)
            throw new NotFoundException("Certificado não encontrado");

        return new ParticipationCertificateViewModel(certificate);
    }
}