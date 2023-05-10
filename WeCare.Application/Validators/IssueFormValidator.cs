using FluentValidation;
using WeCare.Application.ViewModels;

namespace WeCare.Application.Validators;

public class IssueFormValidator : AbstractValidator<IssueReportForm>
{
    
    public IssueFormValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("É necessário um titulo")
            .MaximumLength(100)
            .WithMessage("O titulo deve ter no máximo 100 caracteres");
        
        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("É necessário uma descrição")
            .MaximumLength(1024)
            .WithMessage("A descrição deve ter no máximo 1024 caracteres");

        RuleFor(x => x.ReportedUserId)
            .NotNull()
            .WithMessage("É necessário o Id de quem está sendo reportado");
        
    }
}