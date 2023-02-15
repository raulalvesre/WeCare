using System.Reflection.Metadata;
using WeCare.Domain;

namespace WeCare.Application.ViewModels;

public class ConfirmationTokenViewModel
{
    public long Id { get; set; }
    public string Token { get; set; }
    public long UserId { get; set; }
    public DateTime CreationDate { get; set; }

    public ConfirmationTokenViewModel(ConfirmationToken confirmationToken)
    {
        Id = confirmationToken.Id;
        Token = confirmationToken.Token;
        UserId = confirmationToken.UserId;
        CreationDate = confirmationToken.CreationDate;
    }
    
}