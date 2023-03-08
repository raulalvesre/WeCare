using FluentValidation;
using Microsoft.AspNetCore.Http;
using WeCare.Application.ViewModels;

namespace WeCare.Application.Validators;

public class ImageUploadFormValidator : AbstractValidator<ImageUploadForm>
{
    public ImageUploadFormValidator()
    {
        RuleFor(x => x.Photo)
            .SetValidator(new ImageValidator());
    }
}