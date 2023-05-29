using WeCare.Application.ViewModels;
using WeCare.Domain.Models;
using WeCare.Infrastructure;

namespace WeCare.Application.Mappers;

public class CandidateMapper
{
    public CandidateViewModel FromModel(Candidate candidate)
    {
        return new CandidateViewModel
        {
            Id = candidate.Id,
            Name = candidate.Name,
            Email = candidate.Email,
            Cpf = candidate.Cpf,
            Telephone = candidate.Telephone,
            Bio = candidate.Bio,
            Address = new AddressViewModel(candidate),
            BirthDate = candidate.BirthDate,
            Qualifications = candidate.Qualifications.Select(x => new QualificationViewModel(x)),
            CausesCandidateIsInterestedIn = candidate.CausesCandidateIsInterestedIn.Select(x => new OpportunityCauseViewModel(x))
        };
    }

    public CandidateForRegistrationViewModel FromModelToInstitutionRegistrationViewModel(Candidate candidate)
    {
        return new CandidateForRegistrationViewModel
        {
            Id = candidate.Id,
            Photo = candidate.Photo,
            Name = candidate.Name,
            Bio = candidate.Bio,
            Address = new SecretativeAddressViewModel(candidate),
            BirthDate = candidate.BirthDate
        };
    }

    public Candidate ToModel(CandidateCreateForm createForm,
        IEnumerable<Qualification> qualifications,
        IEnumerable<OpportunityCause> interestedInCauses)
    {
        return new Candidate
        {
            Email = createForm.Email,
            Name = createForm.Name,
            Password = StringCipher.Encrypt(createForm.Password),
            Telephone = createForm.Telephone,
            Bio = createForm.Bio,
            Street = createForm.Address.Street,
            Number = createForm.Address.Number,
            Complement = createForm.Address.Complement,
            City = createForm.Address.City,
            Neighborhood = createForm.Address.Neighborhood,
            State = createForm.Address.State,
            PostalCode = createForm.Address.PostalCode,
            Cpf = createForm.Cpf,
            BirthDate = createForm.BirthDate,
            Qualifications = qualifications,
            CausesCandidateIsInterestedIn = interestedInCauses
        };
    }

    public void Merge(Candidate candidate,
        CandidateCreateForm createForm,
        IEnumerable<Qualification> qualifications,
        IEnumerable<OpportunityCause> interestedInCauses)
    {
        candidate.Email = createForm.Email;
        candidate.Password = StringCipher.Encrypt(createForm.Password);
        candidate.Name = createForm.Name;
        candidate.Telephone = createForm.Telephone;
        candidate.Bio = createForm.Bio;
        candidate.Street = createForm.Address.Street;
        candidate.Number = createForm.Address.Number;
        candidate.Complement = createForm.Address.Complement;
        candidate.City = createForm.Address.City;
        candidate.Neighborhood = createForm.Address.Neighborhood;
        candidate.State = createForm.Address.State;
        candidate.PostalCode = createForm.Address.PostalCode;
        candidate.Cpf = createForm.Cpf;
        candidate.BirthDate = createForm.BirthDate;
        candidate.Qualifications = qualifications;
        candidate.CausesCandidateIsInterestedIn = interestedInCauses;
    }
    
    public void Merge(Candidate candidate,
        CandidateUpdateForm updateForm,
        IEnumerable<Qualification> qualifications,
        IEnumerable<OpportunityCause> interestedInCauses)
    {
        candidate.Email = updateForm.Email;
        candidate.Name = updateForm.Name;
        candidate.Telephone = updateForm.Telephone;
        candidate.Bio = updateForm.Bio;
        candidate.Street = updateForm.Address.Street;
        candidate.Number = updateForm.Address.Number;
        candidate.Complement = updateForm.Address.Complement;
        candidate.City = updateForm.Address.City;
        candidate.Neighborhood = updateForm.Address.Neighborhood;
        candidate.State = updateForm.Address.State;
        candidate.PostalCode = updateForm.Address.PostalCode;
        candidate.Cpf = updateForm.Cpf;
        candidate.BirthDate = updateForm.BirthDate;
        candidate.Qualifications = qualifications;
        candidate.CausesCandidateIsInterestedIn = interestedInCauses;
    }

    public Candidate ToModel(CandidateAdminForm form)
    {
        return new Candidate
        {
            Email = form.Email,
            Name = form.Name,
            Password = StringCipher.Encrypt(form.Password),
            Telephone = form.Telephone,
            Bio = form.Bio,
            Street = form.Address.Street,
            Number = form.Address.Number,
            Complement = form.Address.Complement,
            City = form.Address.City,
            Neighborhood = form.Address.Neighborhood,
            State = form.Address.State,
            PostalCode = form.Address.PostalCode,
            Cpf = form.Cpf,
            BirthDate = form.BirthDate
        };
    }

    public void Merge(Candidate candidate, CandidateAdminForm form)
    {
        candidate.Email = form.Email;
        candidate.Password = StringCipher.Encrypt(form.Password);
        candidate.Name = form.Name;
        candidate.Telephone = form.Telephone;
        candidate.Bio = form.Bio;
        candidate.Street = form.Address.Street;
        candidate.Number = form.Address.Number;
        candidate.Complement = form.Address.Complement;
        candidate.City = form.Address.City;
        candidate.Neighborhood = form.Address.Neighborhood;
        candidate.State = form.Address.State;
        candidate.PostalCode = form.Address.PostalCode;
        candidate.Cpf = form.Cpf;
        candidate.BirthDate = form.BirthDate;
    }
}