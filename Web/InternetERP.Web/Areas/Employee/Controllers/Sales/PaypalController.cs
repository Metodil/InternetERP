namespace InternetERP.Web.Areas.Employee.Controllers.Sales
{
    using System.Threading.Tasks;

    using InternetERP.Services.Contracts;
    using InternetERP.Web.Areas.Employee.Controllers;
    using InternetERP.Web.ViewModels.Employee.Sales;
    using Microsoft.AspNetCore.Mvc;

    public class PaypalController : EmployeeController
    {
        private readonly IPaypalService paypalService;

        public PaypalController(
            IPaypalService paypalService)
        {
            this.paypalService = paypalService;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePayment(decimal amount, string billId, string description)
        {
            var input = new PayPalInputModel
            {
                Amount = amount,
                BillId = billId,
                Description = description,
            };
            var result = await this.paypalService.CreatePayment(input);

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
