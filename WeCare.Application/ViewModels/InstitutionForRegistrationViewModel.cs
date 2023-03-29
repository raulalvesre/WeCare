namespace WeCare.Application.ViewModels;

public class InstitutionForRegistrationViewModel
{
    public long Id { get; set; }
    public byte[] Photo { get; set; }
    public string Name { get; set; }
    public SecretativeAddressViewModel Address { get; set; }
    public string Cnpj { get; set; } = string.Empty;
}