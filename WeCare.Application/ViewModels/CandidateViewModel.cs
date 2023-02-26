namespace WeCare.Application.ViewModels;

public class CandidateViewModel
{
    public long Id { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string Telephone { get; set; }
    public AddressViewModel Address { get; set; }
    public string Cpf { get; set; } = string.Empty;
    public DateOnly BirthDate { get; set; }
    
}