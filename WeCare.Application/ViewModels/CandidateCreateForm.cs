using System.Text.Json.Serialization;

namespace WeCare.Application.ViewModels;

public class CandidateCreateForm
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Telephone { get; set; }
    public string Bio { get; set; }
    public AddressViewModel Address { get; set; }   
    public string Cpf { get; set; }
    public DateOnly BirthDate { get; set; }

    public IEnumerable<long> QualificationsIds { get; set; }
    public IEnumerable<long> InterestedInCausesIds { get; set; }

}