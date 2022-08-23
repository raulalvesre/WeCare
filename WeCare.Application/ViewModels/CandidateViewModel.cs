using WeCare.Domain;

namespace WeCare.Application.ViewModels;

public class CandidateViewModel
{
    public long Id { get; set; }
    public string Email { get; set; }
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

    public CandidateViewModel(Candidate candidate) {
        this.Id = candidate.Id;
        this.Email = candidate.Email;
        this.Name = candidate.Name;
        this.Telephone = candidate.Telephone;
        this.Street = candidate.Street;
        this.Number = candidate.Number;
        this.Complement = candidate.Complement;
        this.City = candidate.City;
        this.Neighborhood = candidate.Neighborhood;
        this.State = candidate.State;
        this.PostalCode = candidate.PostalCode;
        this.Cpf = candidate.Cpf;
        this.BirthDate = candidate.BirthDate;
    }
}