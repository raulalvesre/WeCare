namespace WeCare.Application.ViewModels;

public class InstitutionViewModel
{
    public long Id { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string Telephone { get; set; }
    public string Bio { get; set; }
    public AddressViewModel Address { get; set; }
    public string Cnpj { get; set; } = string.Empty;
    
}