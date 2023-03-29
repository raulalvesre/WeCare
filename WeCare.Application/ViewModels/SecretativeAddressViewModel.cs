using WeCare.Domain.Core;
using WeCare.Domain.Models;

namespace WeCare.Application.ViewModels;

public class SecretativeAddressViewModel
{
    public string City { get; set; }
    public string Neighborhood { get; set; }
    public State State { get; set; }
    
    
    public SecretativeAddressViewModel(User user)
    {
        City = user.City;
        Neighborhood = user.Neighborhood;
        State = user.State;
    }
    
    public SecretativeAddressViewModel(VolunteerOpportunity opportunity)
    {
        City = opportunity.City;
        Neighborhood = opportunity.Neighborhood;
        State = opportunity.State;
    }
}