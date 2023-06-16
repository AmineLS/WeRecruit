namespace WeRecruit.Services;

public interface IMailService
{
    void SendConfirmation(string email);
}