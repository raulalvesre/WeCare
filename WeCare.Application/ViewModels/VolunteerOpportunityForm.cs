using FluentValidation.Results;
using WeCare.Application.Validators;
using WeCare.Domain;
using WeCare.Domain.Models;

namespace WeCare.Application.ViewModels;

public class VolunteerOpportunityForm
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime OpportunityDate { get; set; }
    public ImageUploadForm Photo { set; get; } 
    public AddressViewModel Address { get; set; }
    public IEnumerable<string> Causes { get; set; } = new List<string>();
    
    public Task<ValidationResult> ValidateAsync()
    {
        return new VolunteerOpportunityFormValidator().ValidateAsync(this);
    }
    
}