using WeCare.Domain;
using WeCare.Domain.Core;
using WeCare.Domain.Models;

namespace WeCare.Application.ViewModels;

public class AddressViewModel
{
    public string Street { get; set; }
    public string Number { get; set; }
    public string Complement { get; set; }= string.Empty;
    public string City { get; set; }
    public string Neighborhood { get; set; }
    public State State { get; set; }
    public string PostalCode { get; set; }
    
    public AddressViewModel() {}

    public AddressViewModel(User user)
    {
        Street = user.Street;
        Number = user.Number;
        Complement = user.Complement;
        City = user.City;
        Neighborhood = user.Neighborhood;
        State = user.State;
        PostalCode = user.PostalCode;
    }
    
}