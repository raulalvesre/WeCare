namespace WeCare.Domain.Models;

public class ConfirmationToken
{
    public long Id { get; set; }
    public string Token { get; set; }
    public DateTime CreationDate { get; set; }
    
    public long UserId { get; set; }
    public User User { get; set; }
    
}