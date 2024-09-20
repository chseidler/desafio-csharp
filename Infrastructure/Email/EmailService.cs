using Application.Interfaces;

namespace Infrastructure.Email;

public class EmailService : IEmailService
{
    public Task SendEmailAsync(string to, string subject, string body)
    {
        Console.WriteLine($"Sended email to: {to}, subject: {subject} and body: {body}");
        return Task.CompletedTask;
    }
}
