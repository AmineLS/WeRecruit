using System.Net.Mail;
using WeRecruit.Model;

namespace WeRecruit.Services;

public class MailService : IMailService, IDisposable
{
    private readonly SmtpClient _smtpClient;
    private readonly MailTemplate _messageTemplate;

    public MailService(SmtpClient smtpClient, MailTemplate messageTemplate1)
    {
        _smtpClient = smtpClient;
        _messageTemplate = messageTemplate1;
    }

    public void Send(string email)
    {
        Task.Run(() =>
        {
            using var message = new MailMessage();
            message.From = new MailAddress(_messageTemplate.SenderEmail, _messageTemplate.SenderName);
            message.To.Add(email);
            message.Subject = _messageTemplate.Subject;
            message.Body = _messageTemplate.Body;

            _smtpClient.Send(message);
        });
    }

    public void Dispose()
    {
        _smtpClient.Dispose();
        GC.SuppressFinalize(this);
    }
}