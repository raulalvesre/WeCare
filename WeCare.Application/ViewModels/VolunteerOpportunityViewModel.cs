using WeCare.Domain.Models;

namespace WeCare.Application.ViewModels;

public class VolunteerOpportunityViewModel
{
    public long Id { get; set; }
    public long InstitutionId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime OpportunityDate { get; set; }
    public byte[] Photo { get; set; }
    public AddressViewModel Address { get; set; }
    public DateTime CreationDate { get; set; }
    
    public IEnumerable<string> Causes { get; set; } = new List<string>();
    public IEnumerable<string> DesirableQualifications { get; set; } = new List<string>();

    public VolunteerOpportunityViewModel(VolunteerOpportunity opportunity)
    {
        Id = opportunity.Id;
        InstitutionId = opportunity.InstitutionId;
        Name = opportunity.Name;
        Description = opportunity.Description;
        OpportunityDate = opportunity.OpportunityDate;
        Photo = opportunity.Photo;
        Address = new AddressViewModel(opportunity);
        CreationDate = opportunity.CreationDate;
        Causes = opportunity.Causes.Select(x => x.Name);
        DesirableQualifications = opportunity.DesirableQualifications.Select(x => x.Name);
    }

    public VolunteerOpportunityViewModel()
    {
    }
}