namespace WeCare.Domain.Models;

public abstract class User
{
    public long Id { get; set; }
    public string Type { get; set; } //DO NOT SET A DEFAULT VALUE!!! IT WILL BREAK INHERITANCE
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Telephone { get; set; } = string.Empty;
    public string Bio { get; set; } = string.Empty;
    public byte[] Photo { get; set; }
    public string Street { get; set; } = string.Empty;
    public string Number { get; set; } = string.Empty;
    public string Complement { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Neighborhood { get; set; } = string.Empty;
    public State State { get; set; }
    public string PostalCode { get; set; } = string.Empty;
    public bool Enabled { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime? LastUpdateDate { get; set; }
    
}