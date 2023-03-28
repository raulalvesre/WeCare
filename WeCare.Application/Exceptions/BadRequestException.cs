using FluentValidation.Results;

namespace WeCare.Application.Exceptions;

public class BadRequestException : Exception
{
    public List<ValidationFailure> Errors { get; set; } = new();

    public BadRequestException(List<ValidationFailure> errors) : this()
    {
        Errors = errors;
    }

    public BadRequestException(string message) : base(message) {}

    public BadRequestException() : base("Requisição inválida por favor verifique os dados e tente novamente")
    {
    }
    
}