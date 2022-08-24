using WeCare.Domain;

namespace WeCare.Application.ViewModels;

public class CandidateViewModel
{
    public long Id { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string Telephone { get; set; }
    public AddressViewModel Address { get; set; }
    public string Cpf { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }

    public CandidateViewModel(Candidate candidate)
    {
        var address = new AddressViewModel
        {
            Street = candidate.Street,
            Number = candidate.Number,
            Complement = candidate.Complement,
            City = candidate.City,
            Neighborhood = candidate.Neighborhood,
            State = candidate.State,
            PostalCode = candidate.PostalCode
        };
        
        Id = candidate.Id;
        Email = candidate.Email;
        Name = candidate.Name;
        Telephone = candidate.Telephone;
        Address = address;
        Cpf = candidate.Cpf;
        BirthDate = candidate.BirthDate;
    }
}