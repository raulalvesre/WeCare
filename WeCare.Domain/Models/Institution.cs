namespace WeCare.Domain.Models;

public class Institution : User
{
    public string Cnpj { get; set; } = string.Empty;

}