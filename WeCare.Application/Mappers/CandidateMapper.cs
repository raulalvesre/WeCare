using WeCare.Application.ViewModels;
using WeCare.Domain;
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
            Address = new AddressViewModel(candidate),
            BirthDate = candidate.BirthDate
        };
    }

    public Candidate ToModel(CandidateForm form)
    {
        return new Candidate {
            Email = form.Email,
            Name = form.Name,
            Password = StringCipher.Encrypt(form.Password),
            Telephone = form.Telephone,
            Street = form.Address.Street,
            Number =  form.Address.Number,
            Complement =  form.Address.Complement,
            City =  form.Address.City,
            Neighborhood =  form.Address.Neighborhood,
            State =  form.Address.State,
            PostalCode =  form.Address.PostalCode,
            Cpf = form.Cpf,
            BirthDate = form.BirthDate
        };
    }

    public void Merge(Candidate candidate, CandidateForm form)
    {
        candidate.Email = form.Email;
        candidate.Password = StringCipher.Encrypt(form.Password);
        candidate.Name = form.Name;
        candidate.Telephone = form.Telephone;
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

    public Candidate ToModel(CandidateAdminForm form)
    {
        return new Candidate {
            Email = form.Email,
            Name = form.Name,
            Password = StringCipher.Encrypt(form.Password),
            Telephone = form.Telephone,
            Street = form.Address.Street,
            Number =  form.Address.Number,
            Complement =  form.Address.Complement,
            City =  form.Address.City,
            Neighborhood =  form.Address.Neighborhood,
            State =  form.Address.State,
            PostalCode =  form.Address.PostalCode,
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