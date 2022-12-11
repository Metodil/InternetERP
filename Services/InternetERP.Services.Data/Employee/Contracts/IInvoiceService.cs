namespace InternetERP.Services.Data.Employee.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using InternetERP.Data.Models;
    using InternetERP.Web.ViewModels.Employee.Invoices;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public interface IInvoiceService
    {
        Task<int> Create(CreateInvoiceInputModel input);

        Task<List<SelectListItem>> GetBillsForSelectAsync(string billId);

        Task<Customer> GetCustomerAsync(int id);

        Task<List<SelectListItem>> GetCustomersForSelectAsync();

        Task<Invoice> GetInvoiceInfoAsync(int id);

        Task<List<SelectListItem>> GetPaymentTypesAsync();
    }
}
