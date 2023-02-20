using WeCare.Application.ViewModels;
using WeCare.Domain;
using WeCare.Infrastructure;

namespace WeCare.Application.Mappers;

public class InstitutionMapper
{
     public InstitutionViewModel FromModel(Institution institution)
    {
        return new InstitutionViewModel
        {
            Id = institution.Id,
            Name = institution.Name,
            Email = institution.Email,
            Cnpj = institution.Cnpj,
            Telephone = institution.Telephone,
            Address = new AddressViewModel(institution),
        };
    }

    public Institution ToModel(InstitutionForm form)
    {
        return new Institution {
            Email = form.Email,
            Name = form.Name,
            Password = StringCipher.Encrypt(form.Password),
            Telephone = form.Telephone,
            Street = form.Address.Street,
            Number =  form.Address.Number,
            Complement =  form.Address.Complement,
            City =  form.Address.City,
            Neighborhood =  form.Address.Neighborhood,
            State =  form.Address.State,
            PostalCode =  form.Address.PostalCode,
            Cnpj = form.Cnpj,
        };
    }

    public void Merge(Institution institution, InstitutionForm form)
    {
        institution.Email = form.Email;
        institution.Password = StringCipher.Encrypt(form.Password);
        institution.Name = form.Name;
        institution.Telephone = form.Telephone;
        institution.Street = form.Address.Street;
        institution.Number = form.Address.Number;
        institution.Complement = form.Address.Complement;
        institution.City = form.Address.City;
        institution.Neighborhood = form.Address.Neighborhood;
        institution.State = form.Address.State;
        institution.PostalCode = form.Address.PostalCode;
        institution.Cnpj = form.Cnpj;
    }

    public Institution ToModel(InstitutionAdminForm form)
    {
        return new Institution {
            Email = form.Email,
            Name = form.Name,
            Password = StringCipher.Encrypt(form.Password),
            Telephone = form.Telephone,
            Street = form.Address.Street,
            Number =  form.Address.Number,
            Complement =  form.Address.Complement,
            City =  form.Address.City,
            Neighborhood =  form.Address.Neighborhood,
            State =  form.Address.State,
            PostalCode =  form.Address.PostalCode,
            Cnpj = form.Cnpj,
        };
    }

    public void Merge(Institution institution, InstitutionAdminForm form)
    {
        institution.Email = form.Email;
        institution.Password = StringCipher.Encrypt(form.Password);
        institution.Name = form.Name;
        institution.Telephone = form.Telephone;
        institution.Street = form.Address.Street;
        institution.Number = form.Address.Number;
        institution.Complement = form.Address.Complement;
        institution.City = form.Address.City;
        institution.Neighborhood = form.Address.Neighborhood;
        institution.State = form.Address.State;
        institution.PostalCode = form.Address.PostalCode;
        institution.Cnpj = form.Cnpj;
    }
}