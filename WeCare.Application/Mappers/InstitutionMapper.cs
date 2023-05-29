using WeCare.Application.ViewModels;
using WeCare.Domain.Models;
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
            Bio = institution.Bio,
            Address = new AddressViewModel(institution),
        };
    }
     
     public InstitutionForRegistrationViewModel FromModelToCandidateRegistrationViewModel(Institution institution)
     {
         return new InstitutionForRegistrationViewModel
         {
             Id = institution.Id,
             Photo = institution.Photo,
             Name = institution.Name,
             Cnpj = institution.Cnpj,
             Address = new SecretativeAddressViewModel(institution),
         };
     }

    public Institution ToModel(InstitutionCreateForm createForm)
    {
        return new Institution {
            Email = createForm.Email,
            Name = createForm.Name,
            Password = StringCipher.Encrypt(createForm.Password),
            Telephone = createForm.Telephone,
            Bio = createForm.Bio,
            Street = createForm.Address.Street,
            Number =  createForm.Address.Number,
            Complement =  createForm.Address.Complement,
            City =  createForm.Address.City,
            Neighborhood =  createForm.Address.Neighborhood,
            State =  createForm.Address.State,
            PostalCode =  createForm.Address.PostalCode,
            Cnpj = createForm.Cnpj,
        };
    }

    public void Merge(Institution institution, InstitutionCreateForm createForm)
    {
        institution.Email = createForm.Email;
        institution.Password = StringCipher.Encrypt(createForm.Password);
        institution.Name = createForm.Name;
        institution.Telephone = createForm.Telephone;
        institution.Bio = createForm.Bio;
        institution.Street = createForm.Address.Street;
        institution.Number = createForm.Address.Number;
        institution.Complement = createForm.Address.Complement;
        institution.City = createForm.Address.City;
        institution.Neighborhood = createForm.Address.Neighborhood;
        institution.State = createForm.Address.State;
        institution.PostalCode = createForm.Address.PostalCode;
        institution.Cnpj = createForm.Cnpj;
    }
    
    public void Merge(Institution institution, InstitutionUpdateForm updateForm)
    {
        institution.Email = updateForm.Email;
        institution.Name = updateForm.Name;
        institution.Telephone = updateForm.Telephone;
        institution.Bio = updateForm.Bio;
        institution.Street = updateForm.Address.Street;
        institution.Number = updateForm.Address.Number;
        institution.Complement = updateForm.Address.Complement;
        institution.City = updateForm.Address.City;
        institution.Neighborhood = updateForm.Address.Neighborhood;
        institution.State = updateForm.Address.State;
        institution.PostalCode = updateForm.Address.PostalCode;
        institution.Cnpj = updateForm.Cnpj;
    }

    public Institution ToModel(InstitutionAdminForm form)
    {
        return new Institution {
            Email = form.Email,
            Name = form.Name,
            Password = StringCipher.Encrypt(form.Password),
            Telephone = form.Telephone,
            Bio = form.Bio,
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
        institution.Bio = form.Bio;
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