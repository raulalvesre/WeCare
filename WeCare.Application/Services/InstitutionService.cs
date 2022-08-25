using WeCare.Application.Exceptions;
using WeCare.Application.ViewModels;
using WeCare.Domain;
using WeCare.Infrastructure.Repositories;

namespace WeCare.Application.Services;

public class InstitutionService
{
    private readonly InstitutionRepository _institutionRepository;

    public InstitutionService(InstitutionRepository institutionRepository)
    {
        _institutionRepository = institutionRepository;
    }

    public async Task<InstitutionViewModel> GetById(long id)
    {
        var institution = await _institutionRepository.GetById(id);

        if (institution is null)
        {
            throw new NotFoundException("Instituição não encontrada");
        }

        return new InstitutionViewModel(institution);
    }

    public async Task<InstitutionViewModel> Save(InstitutionForm form)
    {
        var validationResult = await form.ValidateAsync();
        if (!validationResult.IsValid)
            throw new BadRequestException(validationResult.Errors);

        var institution = new Institution
        {
            Email = form.Email,
            Password = form.Password,
            Name = form.Name,
            Telephone = form.Telephone,
            Street = form.Address.Street,
            Number = form.Address.Number,
            Complement = form.Address.Complement,
            City = form.Address.City,
            Neighborhood = form.Address.Neighborhood,
            State = form.Address.State,
            PostalCode = form.Address.PostalCode,
            Cnpj = form.Cnpj,
        };

        await _institutionRepository.Save(institution);

        return new InstitutionViewModel(institution);
    }
}