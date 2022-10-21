using System.Threading.Tasks;
using CascadeFinTech.Application.Configuration.Emails;

namespace CascadeFinTech.Infrastructure.Emails
{
    public class EmailSender : IEmailSender
    {
        public async Task SendEmailAsync(EmailMessage message)
        {
            // Integration with email service.

            return;
        }
    }
}