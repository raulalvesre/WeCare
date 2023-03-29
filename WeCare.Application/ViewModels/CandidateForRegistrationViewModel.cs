namespace WeCare.Application.ViewModels;

public class CandidateForRegistrationViewModel
{
    public long Id { get; set; }
    public byte[] Photo { get; set; }
    public string Name { get; set; }
    public string Bio { get; set; }
    public DateOnly BirthDate { get; set; }
    public SecretativeAddressViewModel Address { get; set; }
}