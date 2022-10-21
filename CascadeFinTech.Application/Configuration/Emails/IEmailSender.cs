using System.Threading.Tasks;

namespace CascadeFinTech.Application.Configuration.Emails
{
    public interface IEmailSender
    {
        Task SendEmailAsync(EmailMessage message);
    }
}