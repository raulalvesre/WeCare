using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using WeCare.Application.ViewModels;

namespace WeCare.Application.Services;

public class EmailService
{

    private readonly EmailSettings _emailSettings;

    public EmailService(EmailSettings emailSettings)
    {
        _emailSettings = emailSettings;
    }
    
    public async Task SendEmailAsync(EmailRequest emailRequest)
    {
        var email = new MimeMessage();
        email.Sender = MailboxAddress.Parse(_emailSettings.Mail);
        email.To.Add(MailboxAddress.Parse(emailRequest.ToEmail));
        email.Subject = emailRequest.Subject;
        var builder = new BodyBuilder();
        if (!emailRequest.Attachments.Any())
        {
            byte[] fileBytes;
            foreach (var file in emailRequest.Attachments)
            {
                if (file.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        file.CopyTo(ms);
                        fileBytes = ms.ToArray();
                    }
                    builder.Attachments.Add(file.Name, fileBytes, new ContentType(file.ContentType, ""));
                }
            }
        }
        
        builder.HtmlBody = emailRequest.Body;
        email.Body = builder.ToMessageBody();
        using var smtp = new SmtpClient();
        smtp.Connect(_emailSettings.Host, _emailSettings.Port, SecureSocketOptions.StartTls);
        smtp.Authenticate(_emailSettings.Mail, _emailSettings.Password);
        smtp.Timeout = 200000;
        await smtp.SendAsync(email);
        smtp.Disconnect(true);
    }
    
}