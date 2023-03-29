namespace WeCare.Application.ViewModels;

public class OpportunityForRegistrationViewModel
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime OpportunityDate { get; set; }
    public byte[] Photo { get; set; }
    public SecretativeAddressViewModel Address { get; set; }
    public InstitutionForRegistrationViewModel Institution { get; set; }
    
    public IEnumerable<string> Causes { get; set; } = new List<string>();
}