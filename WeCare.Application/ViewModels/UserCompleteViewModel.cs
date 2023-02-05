using WeCare.Domain;

namespace WeCare.Application.ViewModels;

public class UserCompleteViewModel
{
    public long Id { get; set; }
    public string Type { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
    public string Telephone { get; set; }
    public AddressViewModel Address { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime? LastUpdateDate { get; set; }

    public UserCompleteViewModel(User user)
    {
        Id = user.Id;
        Type = user.UserType;
        Email = user.Email;
        Name = user.Name;
        Password = user.Password;
        Telephone = user.Telephone;
        Address = new AddressViewModel(user);
        CreationDate = user.CreationDate;
        LastUpdateDate = user.LastUpdateDate;
    }
    
}