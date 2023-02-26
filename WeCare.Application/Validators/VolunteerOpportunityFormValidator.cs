// using FluentValidation;
// using WeCare.Application.ViewModels;
//
// namespace WeCare.Application.Validators;
//
// public class VolunteerOpportunityFormValidator : AbstractValidator<VolunteerOpportunityForm>
// {
//     public VolunteerOpportunityFormValidator()
//     {
//         RuleFor(x => x.Id)
//             .LessThan(1)
//             .WithMessage("Id inválido");
//
//         RuleFor(x => x.Name)
//             .NotEmpty()
//             .WithMessage("O nome da oportunidade não pode ser vazio")
//             .MaximumLength(255)
//             .WithMessage("O nome da oportunidade deve ter no máximo 255 caracteres");
//         
//         RuleFor(x => x.Description)
//             .NotEmpty()
//             .WithMessage("A descrição da oportunidade não pode ser vazio")
//             .MaximumLength(500)
//             .WithMessage("A descrição da oportunidade deve ter no máximo 500 caracteres");
//
//         RuleFor(x => x.OpportunityDate)
//             .NotNull()
//             .WithMessage("A data da oportunidade não pode nula")
//             .GreaterThan(DateTime.Now.AddDays(1))
//             .WithMessage("A oportunidade não pode acontecer em menos de 24 horas");
//
//         RuleFor(x => x.Address)
//             .SetValidator(new AddressViewModelValidator());
//     }
// }