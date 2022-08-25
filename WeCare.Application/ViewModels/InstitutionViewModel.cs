using WeCare.Domain;

namespace WeCare.Application.ViewModels;

public class InstitutionViewModel
{
    public long Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Telephone { get; set; } = string.Empty;
    public AddressViewModel Address { get; set; }
    public string Cnpj { get; set; } = string.Empty;

    public InstitutionViewModel(Institution institution)
    {
        var address = new AddressViewModel
        {
            Street = institution.Street,
            Number = institution.Number,
            Complement = institution.Complement,
            City = institution.City,
            Neighborhood = institution.Neighborhood,
            State = institution.State,
            PostalCode = institution.PostalCode
        };

        Id = institution.Id;
        Email = institution.Email;
        Password = institution.Password;
        Name = institution.Name;
        Telephone = institution.Telephone;
        Address = address;
        Cnpj = institution.Cnpj;
    }
}