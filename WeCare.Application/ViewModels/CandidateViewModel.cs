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
        Id = candidate.Id;
        Email = candidate.Email;
        Name = candidate.Name;
        Telephone = candidate.Telephone;
        Address = new AddressViewModel(candidate);
        Cpf = candidate.Cpf;
        BirthDate = candidate.BirthDate;
    }
}