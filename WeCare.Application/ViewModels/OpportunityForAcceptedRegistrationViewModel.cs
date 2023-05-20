namespace WeCare.Application.ViewModels;

public class OpportunityForAcceptedRegistrationViewModel
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime OpportunityDate { get; set; }
    public byte[] Photo { get; set; }
    public AddressViewModel Address { get; set; }
    public InstitutionForRegistrationViewModel Institution { get; set; }
    
    public IEnumerable<string> Causes { get; set; } = new List<string>();
    public IEnumerable<string> DesirableQualifications { get; set; } = new List<string>();

}