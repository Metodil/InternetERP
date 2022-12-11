namespace InternetERP.Services.Data.ContactUs
{
    using System.Threading.Tasks;

    using InternetERP.Common;
    using InternetERP.Services.Data.ContactUs.Contracts;
    using InternetERP.Services.Messaging;
    using InternetERP.Web.ViewModels.Contact;

    public class ContactUsService : IContactUsService
    {
        private readonly IEmailSender emailSender;

        public ContactUsService(
            IEmailSender emailSender)
        {
            this.emailSender = emailSender;
        }

        public async Task<bool> SendToUsAsync(ContactUsViewModel input)
        {
            var resutl = true;
            try
            {
                await this.emailSender.SendEmailAsync(
                            "meto@elcomp68.com",
                            input.Name,
                            input.Email,
                            input.Subject ?? GlobalConstants.ConstMailSubject,
                            input.Content);
            }
            catch (System.Exception)
            {
                // todo Loging error
                resutl = false;
            }

            return resutl;
        }
    }
}
