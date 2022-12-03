namespace InternetERP.Services.Data.Employee
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using InternetERP.Data.Common.Repositories;
    using InternetERP.Data.Models;
    using InternetERP.Services.Data.Employee.Contracts;
    using InternetERP.Web.ViewModels.Employee.Invoices;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    public class InvoiceService : IInvoiceService
    {
        private readonly IDeletableEntityRepository<Bill> billsRepository;
        private readonly IDeletableEntityRepository<Customer> customersRepository;
        private readonly IDeletableEntityRepository<PaymentType> paymentTypesRepository;
        private readonly IDeletableEntityRepository<Invoice> invoiceRepository;
        private readonly IDeletableEntityRepository<Sale> salesRepository;

        public InvoiceService(
            IDeletableEntityRepository<Bill> billsRepository,
            IDeletableEntityRepository<Customer> customersRepository,
            IDeletableEntityRepository<PaymentType> paymentTypesRepository,
            IDeletableEntityRepository<Invoice> invoiceRepository,
            IDeletableEntityRepository<Sale> salesRepository)
        {
            this.billsRepository = billsRepository;
            this.customersRepository = customersRepository;
            this.paymentTypesRepository = paymentTypesRepository;
            this.invoiceRepository = invoiceRepository;
            this.salesRepository = salesRepository;
        }

        public async Task<List<SelectListItem>> GetCustomersForSelectAsync()
        {
            var selectedListItem = new SelectListItem
            {
                Value = string.Empty,
                Text = "Select an customer",
            };
            var selectedListItemsFromDb = await this.customersRepository
                .AllAsNoTracking()
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name + " BulstatN:" + c.BulstatNumber,
                })
                .ToListAsync();
            var selectedListItems = new List<SelectListItem>();
            selectedListItems.Add(selectedListItem);
            selectedListItems.AddRange(selectedListItemsFromDb);
            return selectedListItems;
        }

        public async Task<List<SelectListItem>> GetBillsForSelectAsync(string billId)
        {
            var selectedListItem = new SelectListItem
            {
                Value = string.Empty,
                Text = "Select an bill",
            };
            var selectedListItemsFromDb = new List<SelectListItem>();
            if (string.IsNullOrEmpty(billId))
            {
                selectedListItemsFromDb = await this.billsRepository
                    .AllAsNoTracking()
                    .Include(b => b.Sales)
                    .Select(b => new SelectListItem
                    {
                        Value = b.Id.ToString(),
                        Text = b.UserFullName + " Qty:" + b.Sales.Count(),
                    })
                    .ToListAsync();
            }
            else
            {
                selectedListItemsFromDb = await this.billsRepository
                    .AllAsNoTracking()
                    .Where(b => b.Id == billId)
                    .Include(b => b.Sales)
                    .Select(b => new SelectListItem
                    {
                        Value = b.Id.ToString(),
                        Text = b.UserFullName + " Qty:" + b.Sales.Count(),
                    })
                    .ToListAsync();
            }

            var selectedListItems = new List<SelectListItem>();
            selectedListItems.Add(selectedListItem);
            selectedListItems.AddRange(selectedListItemsFromDb);
            return selectedListItems;
        }

        public async Task<List<SelectListItem>> GetPaymentTypes()
        {
            var selectedListItem = new SelectListItem
            {
                Value = string.Empty,
                Text = "Select an payment type",
            };

            var selectedListItemsFromDb = await this.paymentTypesRepository
                .AllAsNoTracking()
                    .Select(pt => new SelectListItem
                    {
                        Value = pt.Id.ToString(),
                        Text = pt.Type,
                    })
                .ToListAsync();

            var selectedListItems = new List<SelectListItem>();
            selectedListItems.Add(selectedListItem);
            selectedListItems.AddRange(selectedListItemsFromDb);
            return selectedListItems;
        }

        public async Task<int> Create(CreateInvoiceInputModel input)
        {
            var billId = input.SelectedBillId;
            var newInvoce = new Invoice
            {
                BillId = billId,
                CustomerId = input.SelectedCustomerId,
                PaymentTypeId = input.SelectedPaymentTypeId,
            };

            await this.invoiceRepository.AddAsync(newInvoce);
            await this.invoiceRepository.SaveChangesAsync();
            var invoiceId = newInvoce.Id;
            var salesForUpdate = await this.salesRepository
                .All()
                .Where(s => s.BillId == billId)
                .ToListAsync();

            foreach (var sale in salesForUpdate)
            {
                sale.InvoiceId = invoiceId;
            }

            await this.salesRepository.SaveChangesAsync();
            return invoiceId;
        }

        public async Task<Invoice> GetInvoiceInfoAsync(int id)
        {
            return await this.invoiceRepository
                .AllAsNoTracking()
                .Include(i => i.Sales)
                .Include(i => i.PaymentType)
                .Where(i => i.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<Customer> GetCustomerAsync(int id)
        {
            return await this.customersRepository
                .AllAsNoTracking()
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();
        }
    }
}
