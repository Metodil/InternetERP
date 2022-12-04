namespace InternetERP.Services.Messaging
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using InternetERP.Common;
    using MailKit.Net.Smtp;
    using MimeKit;

    public class MailKitSender : IMailKitSender
    {
        public async Task SendEmailAsync(string from, string fromName, string to, string subject, string htmlContent, IEnumerable<EmailAttachment> attachments = null)
        {
            var email = new MimeMessage();

            //email.From.Add(new MailboxAddress("Sender Name", "sender@email.com"));
            //email.To.Add(new MailboxAddress("Receiver Name", "receiver@email.com"));
            email.From.Add(new MailboxAddress(fromName, from));
            email.To.Add(new MailboxAddress("Receiver Name", to));

            email.Subject = "InternetERP: "+subject;
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = htmlContent,
            };

            using (var smtp = new SmtpClient())
            {
                await smtp.ConnectAsync("smtp.gmail.com", 587, false);

                // Note: only needed if the SMTP server requires authentication
                await smtp.AuthenticateAsync(GlobalConstants.DataForSeeding.AdminEmail, "WebProject1");

                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);
            }
        }
    }
}
