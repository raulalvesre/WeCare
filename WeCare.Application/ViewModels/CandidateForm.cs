using FluentValidation.Results;
using WeCare.Application.Validators;
using WeCare.Domain;

namespace WeCare.Application.ViewModels;

public class CandidateForm
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Telephone { get; set; }
    public string Bio { get; set; }
    public AddressViewModel Address { get; set; }   
    public string Cpf { get; set; }
    public DateOnly BirthDate { get; set; }

    public Task<ValidationResult> ValidateAsync()
    {
        return new CandidateFormValidator().ValidateAsync(this);
    }
}