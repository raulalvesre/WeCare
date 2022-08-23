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
    public string Street { get; set; } = string.Empty;
    public string Number { get; set; } = string.Empty;
    public string Complement { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Neighborhood { get; set; } = string.Empty;
    public State State { get; set; }
    public string PostalCode { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }

    public Task<ValidationResult> ValidateAsync() {
        return new CandidateAdminFormValidator().ValidateAsync(this);
    }

    public Candidate toModel() {
        return new Candidate {
            Email = this.Email,
            Name = this.Name,
            Telephone = this.Telephone,
            Street = this.Street,
            Number = this.Number,
            Complement = this.Complement,
            City = this.City,
            Neighborhood = this.Neighborhood,
            State = this.State,
            PostalCode = this.PostalCode,
            Cpf = this.Cpf,
            BirthDate = this.BirthDate
        };
    }
}