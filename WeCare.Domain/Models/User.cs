namespace WeCare.Domain;

public abstract class User
{
    public long Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }
    public string Telephone { get; set; }
    public string Address { get; set; }
    public string Number { get; set; }
    public string Complement { get; set; }
    public string City { get; set; }
    public string Neighborhood { get; set; }
    public State State { get; set; }
    public string Cep { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime? LastUpdateDate { get; set; }
}