namespace WeRecruit.Model;

public class MailTemplate
{
    public MailTemplate(string senderEmail, string senderName, string subject, string body)
    {
        SenderEmail = senderEmail;
        SenderName = senderName;
        Subject = subject;
        Body = body;
    }

    public string SenderEmail { get; }
    public string SenderName { get; }
    public string Subject { get; }
    public string Body { get; }
}