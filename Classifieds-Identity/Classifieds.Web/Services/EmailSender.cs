using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace Classifieds.Web.Services;

public class EmailSender : IEmailSender
{
    private readonly string _smtpServer;
    private readonly int _smtpPort;
    private readonly string _fromEmailAddress;
    private readonly string? _emailPassword;

    public EmailSender(string smtpServer, int smtpPort, string fromEmailAddress, string? emailPassword = null)
    {
        _smtpServer = smtpServer;
        _smtpPort = smtpPort;
        _fromEmailAddress = fromEmailAddress;
        _emailPassword = emailPassword;
    }

    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        MailMessage message =  new()
        {
            From = new MailAddress(_fromEmailAddress),
            Subject = subject,
            Body = htmlMessage,
            IsBodyHtml = true
        };

        message.To.Add(new MailAddress(email));

        using (var client = new SmtpClient(_smtpServer, _smtpPort))
        {
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Credentials = new NetworkCredential(_fromEmailAddress, _emailPassword);

            client.Send(message);
        }

        return Task.CompletedTask;
    }
}
