namespace WeCare.Domain;

public class Institution
{
    
    public int Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }
    public string Cnpj { get; set; }
    public string Telephone { get; set; }
    public string Address { get; set; }  
    public DateTime CreationDate { get; set; }
    public DateTime? LastUpdatedDate { get; set; }
    
    public int LineOfWorkId { get; set; }
    public InstitutionLineOfWork LineOfWork { get; set; }
    
}