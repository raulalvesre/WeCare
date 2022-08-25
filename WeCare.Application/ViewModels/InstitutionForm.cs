using FluentValidation.Results;
using WeCare.Application.Validators;

namespace WeCare.Application.ViewModels;

public class InstitutionForm
{
    public long Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Telephone { get; set; } = string.Empty;
    public AddressViewModel Address { get; set; }
    public string Cnpj { get; set; } = string.Empty;

    public Task<ValidationResult> ValidateAsync()
    {
        return new InstitutionFormValidator().ValidateAsync(this);
    }
}