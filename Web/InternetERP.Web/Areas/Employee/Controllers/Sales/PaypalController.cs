namespace InternetERP.Web.Areas.Employee.Controllers.Sales
{
    using System.Threading.Tasks;
    using InternetERP.Common;
    using InternetERP.Data.Common.Repositories;
    using InternetERP.Services.Contracts;
    using InternetERP.Services.Data.Employee.Contracts;
    using InternetERP.Web.Areas.Employee.Controllers;
    using InternetERP.Web.ViewModels.Employee.Sales;
    using Microsoft.AspNetCore.Mvc;
    using PayPal.Api;

    public class PaypalController : EmployeeController
    {
        private readonly IPaypalService paypalService;
        private readonly IBillService billService;

        public PaypalController(
            IPaypalService paypalService,
            IBillService billService)
        {
            this.paypalService = paypalService;
            this.billService = billService;
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

        public async Task<IActionResult> SuccessedPayment(string paymentId, string token, string payerId, decimal amount, string billId)
        {
            var result = await this.paypalService.ExecutePayment(payerId, paymentId, token);
            await this.billService.AddPaymentToBill(amount, billId);
            await this.billService.ChangeStatus(billId, GlobalConstants.BillFinishedId);

            return this.View(amount);
        }
    }
}
