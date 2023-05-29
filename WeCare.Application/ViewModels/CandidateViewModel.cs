using WeCare.Domain.Models;

namespace WeCare.Application.ViewModels;

public class CandidateViewModel
{
    public long Id { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string Telephone { get; set; }
    public string Bio { get; set; }
    public AddressViewModel Address { get; set; }
    public string Cpf { get; set; }
    public DateOnly BirthDate { get; set; }
    public IEnumerable<QualificationViewModel> Qualifications { get; set; }
    public IEnumerable<OpportunityCauseViewModel> CausesCandidateIsInterestedIn { get; set; }

}