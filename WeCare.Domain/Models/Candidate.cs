namespace WeCare.Domain;

using System;

public class Candidate
{
    
    public int Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }
    public DateOnly BirthDate { get; set; } 
    public string Cpf { get; set; }
    public string Telephone { get; set; }
    public string Address { get; set; }  
    public DateTime CreationDate { get; set; }
    public DateTime? LastUpdatedDate { get; set; }
    
}