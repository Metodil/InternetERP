namespace InternetERP.Web.Areas.Employee.Controllers.Sales
{
    using System.Threading.Tasks;
    using InternetERP.Data.Models;
    using InternetERP.Services.Data.Employee.Contracts;
    using InternetERP.Web.ViewModels.Employee.Invoices;
    using Microsoft.AspNetCore.Mvc;

    public class InvoicesController : EmployeeController
    {
        private readonly IInvoiceService invoiceService;

        public InvoicesController(
            IInvoiceService invoiceService)
        {
            this.invoiceService = invoiceService;
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
                PaymentTypes = await this.invoiceService.GetPaymentTypes(),
            };

            return this.View(newModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(CreateInvoiceInputModel input)
        {
            var billId = input.BillId == null ? string.Empty : input.BillId;
            if (!this.ModelState.IsValid)
            {
                input.Customers = await this.invoiceService.GetCustomersForSelectAsync();
                input.Bills = await this.invoiceService.GetBillsForSelectAsync(billId);
                input.PaymentTypes = await this.invoiceService.GetPaymentTypes();
                return this.View(input);
            }

            var invoiceId = await this.invoiceService.Create(input);
            var newInvoice = new CreateInvoiceInputModel
            {
                SuccessMsg = "Sucesfuly create Invoce.",
                InvoiceId = invoiceId,
            };

            return this.View(newInvoice);
        }

        [HttpGet]
        public async Task<IActionResult> Show(int invoiceId)
        {
            if (invoiceId == 0)
            {
                return this.RedirectToAction("All");
            }

            var invoiceInfo = await this.invoiceService.GetInvoiceInfoAsync(invoiceId);
            var newModel = new InvoiceViewModel
            {
                InvoiceInfo = invoiceInfo,
                Customer = await this.invoiceService.GetCustomerAsync(invoiceInfo.CustomerId),
            };

            return this.View(newModel);
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            return this.View();
        }
    }
}
