namespace WeCare.Application.ViewModels;

public class ConfirmationTokenForm
{
    public string Token { get; set; }
    public long UserId { get; set; }

    public ConfirmationTokenForm(string token, long userId)
    {
        Token = token;
        UserId = userId;
    }
    
}