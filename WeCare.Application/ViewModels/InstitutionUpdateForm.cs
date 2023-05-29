using System.Text.Json.Serialization;
using FluentValidation.Results;
using WeCare.Application.Validators;
using WeCare.Domain;

namespace WeCare.Application.ViewModels;

public class InstitutionUpdateForm
{
    [JsonIgnore]
    public long Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Telephone { get; set; }
    public string Bio { get; set; }
    public AddressViewModel Address { get; set; }   
    public string Cnpj { get; set; }
}