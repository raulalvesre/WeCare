using Microsoft.AspNetCore.Http;

namespace WeCare.Application.ViewModels;

public class EmailRequest
{
    public string ToEmail { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
    public List<IFormFile> Attachments { get; set; } = new List<IFormFile>();
}