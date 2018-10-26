using System;
using System.Threading.Tasks;

namespace WebAppCore.Services
{
    // This class is used by the application to send email for account confirmation and password reset.
    // For more details see https://go.microsoft.com/fwlink/?LinkID=532713
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            Console.WriteLine($"Sending email To: {email} Subject: {subject}\n{message}");
            return Task.CompletedTask;
        }
    }
}