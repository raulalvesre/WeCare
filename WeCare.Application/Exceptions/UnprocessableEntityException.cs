namespace WeCare.Application.Exceptions;

public class UnprocessableEntityException: Exception
{
    public List<string> Errors { get; set; } = new List<string>();

    public UnprocessableEntityException(List<string> errors) : this()
    {
        Errors = errors;
    }
    
    public UnprocessableEntityException(string error) : base(error)
    {
    }

    public UnprocessableEntityException() : base("Requisição inválida por favor verifique os dados e tente novamente")
    {
    }
    
}