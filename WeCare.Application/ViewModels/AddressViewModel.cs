using WeCare.Domain;

namespace WeCare.Application.ViewModels;

public class AddressViewModel
{
    public string Street { get; set; }
    public string Number { get; set; }
    public string? Complement { get; set; }
    public string City { get; set; }
    public string Neighborhood { get; set; }
    public State State { get; set; }
    public string PostalCode { get; set; }
}