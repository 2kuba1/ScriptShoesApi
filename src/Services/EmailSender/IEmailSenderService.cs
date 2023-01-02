namespace ScriptShoesAPI.Services.EmailSender;

public interface IEmailSenderService
{
    Task SendEmail(string userEmail, string body, string subject);
}