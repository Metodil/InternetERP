namespace InternetERP.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using InternetERP.Services.Contracts;

    public class PaypalController : BaseController
    {
        private readonly IPaypalService paypalService;

        public PaypalController(
            IPaypalService paypalService)
        {
            this.paypalService = paypalService;
        }

        public async Task<IActionResult> CreatePayment(decimal amount, string description)
        {
            var result = await this.paypalService.CreatePayment(amount, description);

            foreach (var link in result.links)
            {
                if (link.rel.Equals("approval_url"))
                {
                    return this.Redirect(link.href);
                }
            }

            return this.NotFound();
        }

        public async Task<IActionResult> SuccessedPayment(string paymentId, string token, string payerId, [FromQuery] int visits)
        {
            var result = await this.paypalService.ExecutePayment(payerId, paymentId, token);

            // TODO Sale as payed

            return this.View(visits);
        }
    }
}
