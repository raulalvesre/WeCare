using WeCare.Domain.Models;

namespace WeCare.Application.ViewModels;

public class CandidateViewModel
{
    public long Id { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string Telephone { get; set; }
    public string Bio { get; set; }
    public byte[] Photo { get; set; }
    public string LinkedIn { get; set; }
    public AddressViewModel Address { get; set; }
    public string Cpf { get; set; }
    public DateOnly BirthDate { get; set; }
    public IEnumerable<QualificationViewModel> Qualifications { get; set; }
    public IEnumerable<OpportunityCauseViewModel> CausesCandidateIsInterestedIn { get; set; }

    public CandidateViewModel(Candidate candidate)
    {
        Id = candidate.Id;
        Email = candidate.Email;
        Name = candidate.Name;
        Telephone = candidate.Telephone;
        Bio = candidate.Bio;
        Photo = candidate.Photo;
        LinkedIn = candidate.LinkedIn;
        Address = new AddressViewModel(candidate);
        Cpf = candidate.Cpf;
        BirthDate = candidate.BirthDate;
        Qualifications = candidate.Qualifications.Select(x => new QualificationViewModel(x));
        CausesCandidateIsInterestedIn =  candidate.CausesCandidateIsInterestedIn.Select(x => new OpportunityCauseViewModel(x));;
    }

    public CandidateViewModel()
    {
    }
}