
using FluentEmail.Core;
using FluentEmail.Core.Models;
using WeCare.Application.ViewModels;

namespace WeCare.Application.Services;

public class EmailService
{

    private readonly IFluentEmail _fluentEmail;

    public EmailService(IFluentEmail fluentEmail)
    {
        _fluentEmail = fluentEmail;
    }
    
    public async Task SendEmailAsync<T>(string toEmail, string subject, string templateName, T model)
    {
        string basePath = Directory.GetCurrentDirectory();
        string templatesPath = Path.Combine(Directory.GetParent(basePath).FullName, "WeCare.Application", "EmailTemplates");
        string templateFilePath = Path.Combine(templatesPath, $"{templateName}.cshtml");
        string template = File.ReadAllText(templateFilePath);
        
        await _fluentEmail
            .To(toEmail)
            .Subject(subject)
            .UsingTemplate(template, model)
            .SendAsync();
    }
    
}