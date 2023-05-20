using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using WeCare.Application.Validators;

namespace WeCare.Application.ViewModels;

public class VolunteerOpportunityForm
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime OpportunityDate { get; set; }
    public IFormFile Photo { set; get; }
    public AddressViewModel Address { get; set; }
    public IEnumerable<string> Causes { get; set; } = new List<string>();
    public IEnumerable<long> DesirableQualificationsIds { get; set; } = new List<long>();

}