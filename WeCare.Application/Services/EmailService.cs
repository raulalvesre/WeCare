
using FluentEmail.Core;
using FluentEmail.Core.Models;
using WeCare.Application.ViewModels;

namespace WeCare.Application.Services;

public class EmailService
{

    private readonly EmailSettings _emailSettings;
    private readonly IFluentEmail _fluentEmail;

    public EmailService(EmailSettings emailSettings, IFluentEmail fluentEmail)
    {
        _emailSettings = emailSettings;
        _fluentEmail = fluentEmail;
    }
    
    public async Task SendEmailAsync(EmailRequest emailRequest)
    {
        await _fluentEmail
            .To(emailRequest.ToEmail)
            .Subject(emailRequest.Subject)
            .Body(emailRequest.Body)
            .SendAsync();
    }
    
    public async Task SendEmailAsync<T>(EmailRequest emailRequest, string templateName, T model)
    {
        string basePath = Directory.GetCurrentDirectory();
        string templatesPath = Path.Combine(Directory.GetParent(basePath).FullName, "WeCare.Application", "EmailTemplates");
        string templateFilePath = Path.Combine(templatesPath, $"{templateName}.cshtml");
        string template = File.ReadAllText(templateFilePath);


        await _fluentEmail
            .To(emailRequest.ToEmail)
            .Subject(emailRequest.Subject)
            .Body(emailRequest.Body)
            .UsingTemplate(template, model)
            .SendAsync();
    }
    
}