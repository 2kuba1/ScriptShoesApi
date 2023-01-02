using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace ScriptShoesAPI.Services.EmailSender;

public class EmailSenderService : IEmailSenderService
{
    private readonly IConfiguration _configuration;

    public EmailSenderService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendEmail(string userEmail, string body, string subject)
    {
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(_configuration.GetValue<string>("Email:EmailAddress")));
        email.To.Add(MailboxAddress.Parse(userEmail));
        email.Subject = subject;
        email.Body = new TextPart(TextFormat.Plain) { Text = body };

        using var smtp = new SmtpClient();
        await smtp.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
        await smtp.AuthenticateAsync(_configuration.GetValue<string>("Email:EmailAddress"), _configuration.GetValue<string>("Email:EmailApplicationPassword"));
        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);
    }
}