namespace WeCare.Application.Exceptions;

public class RequestEntityTooLargeException: Exception
{
    public RequestEntityTooLargeException(string message) : base(message)
    {
    }
}