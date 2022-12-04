namespace InternetERP.Web.Controllers
{
    using System.Threading.Tasks;

    using InternetERP.Services.Data.ContactUs.Contracts;
    using InternetERP.Services.Messaging;
    using InternetERP.Web.ViewModels.Contact;
    using Microsoft.AspNetCore.Mvc;

    public class ContactUsController : BaseController
    {
        private readonly IContactUsService contactUsService;
        private readonly IEmailSender emailSender;

        public ContactUsController(
            IContactUsService contactUsService,
            IEmailSender emailSender)
        {
            this.contactUsService = contactUsService;
            this.emailSender = emailSender;
        }

        [HttpGet]
        public IActionResult ByMail()
        {
                return this.View();
        }

        [HttpPost]

        public async Task<IActionResult> ByMail(ContactUsViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var result = await this.contactUsService.SendToUsAsync(input);

            return this.Redirect("/Home/Index");
        }

        [HttpGet]
        public IActionResult ByPhoneAndAddress()
        {
            return this.View();
        }
    }
}
