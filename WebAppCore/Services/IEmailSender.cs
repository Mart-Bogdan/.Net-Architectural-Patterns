using System.Threading.Tasks;

namespace WebAppCore.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}