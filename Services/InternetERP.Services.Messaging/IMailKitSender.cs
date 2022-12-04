using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternetERP.Services.Messaging
{
    public interface IMailKitSender
    {
        Task SendEmailAsync(string from, string fromName, string to, string subject, string htmlContent, IEnumerable<EmailAttachment> attachments = null);
    }
}
