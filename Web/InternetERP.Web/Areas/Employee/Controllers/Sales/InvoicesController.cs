namespace InternetERP.Web.Areas.Employee.Controllers.Sales
{
    using System.Threading.Tasks;
    using InternetERP.Common;
    using InternetERP.Data.Models;
    using InternetERP.Services.Data.Employee.Contracts;
    using InternetERP.Web.ViewModels.Employee.Invoices;
    using Microsoft.AspNetCore.Mvc;
    using PayPal.Api;

    public class InvoicesController : EmployeeController
    {
        private readonly IInvoiceService invoiceService;
        private readonly IBillService billService;

        public InvoicesController(
            IInvoiceService invoiceService,
            IBillService billService)
        {
            this.invoiceService = invoiceService;
            this.billService = billService;
        }

        [HttpGet]
        public async Task<IActionResult> Create(string billId)
        {
            if (billId == null)
            {
                billId = string.Empty;
            }

            var newModel = new CreateInvoiceInputModel
            {
                Customers = await this.invoiceService.GetCustomersForSelectAsync(),
                Bills = await this.invoiceService.GetBillsForSelectAsync(billId),
                PaymentTypes = await this.invoiceService.GetPaymentTypesAsync(),
            };

            return this.View(newModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(CreateInvoiceInputModel input)
        {
            var billId = input.BillId == null ? input.SelectedBillId : input.BillId;
            if (!this.ModelState.IsValid)
            {
                input.Customers = await this.invoiceService.GetCustomersForSelectAsync();
                input.Bills = await this.invoiceService.GetBillsForSelectAsync(billId);
                input.PaymentTypes = await this.invoiceService.GetPaymentTypesAsync();
                return this.View(input);
            }

            var amount = await this.billService.GetBillAmount(billId);
            await this.billService.AddPaymentToBill(amount, billId, "Bank Tranfer");
            await this.billService.ChangeStatus(billId, GlobalConstants.BillFinishedId);

            var invoiceId = await this.invoiceService.Create(input);

            return this.RedirectToAction("Show", new { id = invoiceId });
        }

        [HttpGet]
        public async Task<IActionResult> Show(int id)
        {
            if (id == 0)
            {
                return this.RedirectToAction("All");
            }

            var invoiceInfo = await this.invoiceService.GetInvoiceInfoAsync(id);
            var newModel = new InvoiceViewModel
            {
                InvoiceInfo = invoiceInfo,
                Customer = await this.invoiceService.GetCustomerAsync(invoiceInfo.CustomerId),
            };

            return this.View(newModel);
        }

        [HttpGet]
        public IActionResult All()
        {
            return this.View();
        }
    }
}
