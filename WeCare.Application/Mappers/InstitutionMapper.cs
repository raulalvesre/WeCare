using WeCare.Application.ViewModels;
using WeCare.Domain;
using WeCare.Infrastructure;

namespace WeCare.Application.Mappers;

public class InstitutionMapper
{
     public InstitutionViewModel FromModel(Institution Institution)
    {
        return new InstitutionViewModel
        {
            Id = Institution.Id,
            Name = Institution.Name,
            Email = Institution.Email,
            Cnpj = Institution.Cnpj,
            Telephone = Institution.Telephone,
            Address = new AddressViewModel(Institution),
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

    public void Merge(Institution Institution, InstitutionForm form)
    {
        Institution.Email = form.Email;
        Institution.Password = StringCipher.Encrypt(form.Password);
        Institution.Name = form.Name;
        Institution.Telephone = form.Telephone;
        Institution.Street = form.Address.Street;
        Institution.Number = form.Address.Number;
        Institution.Complement = form.Address.Complement;
        Institution.City = form.Address.City;
        Institution.Neighborhood = form.Address.Neighborhood;
        Institution.State = form.Address.State;
        Institution.PostalCode = form.Address.PostalCode;
        Institution.Cnpj = form.Cnpj;
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

    public void Merge(Institution Institution, InstitutionAdminForm form)
    {
        Institution.Email = form.Email;
        Institution.Password = StringCipher.Encrypt(form.Password);
        Institution.Name = form.Name;
        Institution.Telephone = form.Telephone;
        Institution.Street = form.Address.Street;
        Institution.Number = form.Address.Number;
        Institution.Complement = form.Address.Complement;
        Institution.City = form.Address.City;
        Institution.Neighborhood = form.Address.Neighborhood;
        Institution.State = form.Address.State;
        Institution.PostalCode = form.Address.PostalCode;
        Institution.Cnpj = form.Cnpj;
    }
}