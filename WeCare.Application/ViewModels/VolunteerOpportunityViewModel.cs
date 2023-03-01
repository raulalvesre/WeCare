using WeCare.Domain.Core;
using WeCare.Domain.Models;

namespace WeCare.Application.ViewModels;

public class VolunteerOpportunityViewModel
{
    public long Id { get; set; }
    public long InstitutionId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime OpportunityDate { get; set; }
    public string Photo { get; set; }
    public AddressViewModel Address { get; set; }
    public DateTime CreationDate { get; set; }
    
    public IEnumerable<string> Causes { get; set; } = new List<string>();
}