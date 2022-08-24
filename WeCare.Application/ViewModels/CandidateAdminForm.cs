using FluentValidation.Results;
using WeCare.Application.Validators;
using WeCare.Domain;

namespace WeCare.Application.ViewModels;

public class CandidateAdminForm
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }
    public string Telephone { get; set; }
    public AddressViewModel Address { get; set; }
    public string Cpf { get; set; }
    public DateTime BirthDate { get; set; }

    public Task<ValidationResult> ValidateAsync() {
        return new CandidateAdminFormValidator().ValidateAsync(this);
    }

    public Candidate toModel() {
        return new Candidate {
            Email = Email,
            Name = Name,
            Telephone = Telephone,
            Street = Address.Street,
            Number = Address.Number,
            Complement = Address.Complement,
            City = Address.City,
            Neighborhood = Address.Neighborhood,
            State = Address.State,
            PostalCode = Address.PostalCode,
            Cpf = Cpf,
            BirthDate = BirthDate
        };
    }
}