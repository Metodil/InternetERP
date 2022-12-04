namespace InternetERP.Services.Data.ContactUs
{
    using System.Threading.Tasks;

    using InternetERP.Services.Data.ContactUs.Contracts;
    using InternetERP.Services.Messaging;
    using InternetERP.Web.ViewModels.Contact;

    public class ContactUsService : IContactUsService
    {
        private readonly IEmailSender emailSender;
        private readonly IMailKitSender mailKitSender;

        public ContactUsService(
            IEmailSender emailSender,
            IMailKitSender mailKitSender)
        {
            this.emailSender = emailSender;
            this.mailKitSender = mailKitSender;
        }

        public async Task<bool> SendToUsAsync(ContactUsViewModel input)
        {
                //var contactFormEntry = new ContactFormEntry
                //{
                //    Name = input.Name,
                //    Email = input.Email,
                //    Subject = input.Subject ?? GlobalConstants.ConstSubject,
                //    Content = input.Content,
                //};

                //await this.emailSender.SendEmailAsync(
                //            "meto@elcomp68.com",
                //            input.Name,
                //            input.Email,
                //            input.Subject,
                //            input.Content);
            await this.mailKitSender.SendEmailAsync(
                        "interneterp.adm@gmail.com",
                        input.Name,
                        input.Email,
                        input.Subject,
                        input.Content);
            return true;
            // TODO return result from savechanges
            //  var result = await this.contactRepository.SaveChangesAsync();
            //    return result > 0;
        }
    }
}
