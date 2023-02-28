using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using WeCare.Application.Validators;

namespace WeCare.Application.ViewModels;

public class ImageUploadForm
{
    public IFormFile Photo { get; set; }

    public Task<ValidationResult> ValidateAsync()
    {
        return new ImageUploadFormValidator().ValidateAsync(this);
    }
}