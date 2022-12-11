namespace InternetERP.Services.Data.Tests
{
    using System;
    using System.Threading.Tasks;

    using InternetERP.Data;
    using InternetERP.Data.Models;
    using InternetERP.Data.Repositories;
    using InternetERP.Services.Data.Employee;
    using InternetERP.Web.ViewModels.Employee.Invoices;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;
    using Xunit;

    public class InvoiceServiceTests
    {
        [Fact]
        public async Task GetCustomersForSelectAsyncProperlyRetrunSelectListItems()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                      .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var db = new ApplicationDbContext(options);
            var billsRepository = new EfDeletableEntityRepository<Bill>(db);
            var customersRepository = new EfDeletableEntityRepository<Customer>(db);
            var paymentTypesRepository = new EfDeletableEntityRepository<PaymentType>(db);
            var invoiceRepository = new EfDeletableEntityRepository<Invoice>(db);
            var salesRepository = new EfDeletableEntityRepository<Sale>(db);

            var service = new InvoiceService(
                billsRepository,
                customersRepository,
                paymentTypesRepository,
                invoiceRepository,
                salesRepository);
            var customer = new Customer
            {
                Id = 1,
                Name = "Customer name",
                Address = "Address",
                BulstatNumber = "11111111",
                MOL = "MOL",
                ReceivedFrom = "ReceivedFrom",
                VATNumber = "9999999999",
            };
            await customersRepository.AddAsync(customer);
            await customersRepository.SaveChangesAsync();

            var result = await service.GetCustomersForSelectAsync();

            Assert.NotNull(result);
            var firstSelectListItem = new SelectListItem
            {
                Value = string.Empty,
                Text = "Select an customer",
            };
            var objExpect = JsonConvert.SerializeObject(firstSelectListItem);
            var objResult = JsonConvert.SerializeObject(result[0]);
            Assert.Equal(objExpect, objResult);

            var secondSelectListItem = new SelectListItem
            {
                Value = "1",
                Text = "Customer name BulstatN:11111111",
            };
            objExpect = JsonConvert.SerializeObject(secondSelectListItem);
            objResult = JsonConvert.SerializeObject(result[1]);
            Assert.Equal(objExpect, objResult);
        }

        [Fact]
        public async Task GetBillsForSelectAsyncProperlyRetrunSelectListItems()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                      .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var db = new ApplicationDbContext(options);
            var billsRepository = new EfDeletableEntityRepository<Bill>(db);
            var customersRepository = new EfDeletableEntityRepository<Customer>(db);
            var paymentTypesRepository = new EfDeletableEntityRepository<PaymentType>(db);
            var invoiceRepository = new EfDeletableEntityRepository<Invoice>(db);
            var salesRepository = new EfDeletableEntityRepository<Sale>(db);

            var service = new InvoiceService(
                billsRepository,
                customersRepository,
                paymentTypesRepository,
                invoiceRepository,
                salesRepository);
            var billId = Guid.NewGuid().ToString();
            var customer = new Bill
            {
                Id = billId,
                StatusId = 1,
            };
            await billsRepository.AddAsync(customer);
            await billsRepository.SaveChangesAsync();

            var result = await service.GetBillsForSelectAsync(billId);

            Assert.NotNull(result);
            var firstSelectListItem = new SelectListItem
            {
                Value = string.Empty,
                Text = "Select an bill",
            };
            var objExpect = JsonConvert.SerializeObject(firstSelectListItem);
            var objResult = JsonConvert.SerializeObject(result[0]);
            Assert.Equal(objExpect, objResult);

            var secondSelectListItem = new SelectListItem
            {
                Value = billId,
                Text = " Qty:0",
            };
            objExpect = JsonConvert.SerializeObject(secondSelectListItem);
            objResult = JsonConvert.SerializeObject(result[1]);
            Assert.Equal(objExpect, objResult);
        }

        [Fact]
        public async Task GetPaymentTypesAsyncProperlyRetrunSelectListItems()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                      .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var db = new ApplicationDbContext(options);
            var billsRepository = new EfDeletableEntityRepository<Bill>(db);
            var customersRepository = new EfDeletableEntityRepository<Customer>(db);
            var paymentTypesRepository = new EfDeletableEntityRepository<PaymentType>(db);
            var invoiceRepository = new EfDeletableEntityRepository<Invoice>(db);
            var salesRepository = new EfDeletableEntityRepository<Sale>(db);

            var service = new InvoiceService(
                billsRepository,
                customersRepository,
                paymentTypesRepository,
                invoiceRepository,
                salesRepository);
            var billId = Guid.NewGuid().ToString();
            var newPaymentType = new PaymentType
            {
                Id = 1,
                Type = "Bank tranfers",
            };
            await paymentTypesRepository.AddAsync(newPaymentType);
            await paymentTypesRepository.SaveChangesAsync();

            var result = await service.GetPaymentTypesAsync();

            Assert.NotNull(result);
            var firstSelectListItem = new SelectListItem
            {
                Value = string.Empty,
                Text = "Select an payment type",
            };
            var objExpect = JsonConvert.SerializeObject(firstSelectListItem);
            var objResult = JsonConvert.SerializeObject(result[0]);
            Assert.Equal(objExpect, objResult);

            var secondSelectListItem = new SelectListItem
            {
                Value = "1",
                Text = "Bank tranfers",
            };
            objExpect = JsonConvert.SerializeObject(secondSelectListItem);
            objResult = JsonConvert.SerializeObject(result[1]);
            Assert.Equal(objExpect, objResult);
        }

        [Fact]
        public async Task CreateProperlyCreateInvoice()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                      .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var db = new ApplicationDbContext(options);
            var billsRepository = new EfDeletableEntityRepository<Bill>(db);
            var customersRepository = new EfDeletableEntityRepository<Customer>(db);
            var paymentTypesRepository = new EfDeletableEntityRepository<PaymentType>(db);
            var invoiceRepository = new EfDeletableEntityRepository<Invoice>(db);
            var salesRepository = new EfDeletableEntityRepository<Sale>(db);

            var service = new InvoiceService(
                billsRepository,
                customersRepository,
                paymentTypesRepository,
                invoiceRepository,
                salesRepository);

            var billId = Guid.NewGuid().ToString();
            var newSale = new Sale
            {
                Id = 1,
                Name = "Sale name",
                BillId = billId,
            };
            await salesRepository.AddAsync(newSale);
            await salesRepository.SaveChangesAsync();

            var newInput = new CreateInvoiceInputModel
            {
                BillId = billId,
                SelectedBillId = billId,
                SelectedCustomerId = 1,
                SelectedPaymentTypeId = 1,
            };

            var result = await service.Create(newInput);

            Assert.Equal(1, result);

            var resultSale = await salesRepository
                .AllAsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == 1);
            Assert.NotNull(resultSale);
            Assert.Equal(1, resultSale.InvoiceId);
        }

        [Fact]
        public async Task GetInvoiceInfoAsyncProperlyReturnInvoices()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                      .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var db = new ApplicationDbContext(options);
            var billsRepository = new EfDeletableEntityRepository<Bill>(db);
            var customersRepository = new EfDeletableEntityRepository<Customer>(db);
            var paymentTypesRepository = new EfDeletableEntityRepository<PaymentType>(db);
            var invoiceRepository = new EfDeletableEntityRepository<Invoice>(db);
            var salesRepository = new EfDeletableEntityRepository<Sale>(db);

            var service = new InvoiceService(
                billsRepository,
                customersRepository,
                paymentTypesRepository,
                invoiceRepository,
                salesRepository);

            var billId = Guid.NewGuid().ToString();
            var newSale = new Sale
            {
                Id = 1,
                Name = "Sale name",
                BillId = billId,
            };
            await salesRepository.AddAsync(newSale);
            await salesRepository.SaveChangesAsync();

            var newPaymentType = new PaymentType
            {
                Id = 1,
                Type = "Bank tranfers",
            };
            await paymentTypesRepository.AddAsync(newPaymentType);
            await paymentTypesRepository.SaveChangesAsync();

            var newInvoice = new Invoice
            {
                Id = 1,
                BillId = billId,
                PaymentTypeId = 1,
            };
            await invoiceRepository.AddAsync(newInvoice);
            billId = Guid.NewGuid().ToString();
            var idInvoice = 2;
            newInvoice = new Invoice
            {
                Id = idInvoice,
                BillId = billId,
                PaymentTypeId = 1,
            };
            await invoiceRepository.AddAsync(newInvoice);
            await invoiceRepository.SaveChangesAsync();

            var result = await service.GetInvoiceInfoAsync(idInvoice);

            Assert.NotNull(result);
            Assert.Equal(billId, result.BillId);
        }

        [Fact]
        public async Task GetCustomerAsyncProperlyReturnCustomer()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                      .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var db = new ApplicationDbContext(options);
            var billsRepository = new EfDeletableEntityRepository<Bill>(db);
            var customersRepository = new EfDeletableEntityRepository<Customer>(db);
            var paymentTypesRepository = new EfDeletableEntityRepository<PaymentType>(db);
            var invoiceRepository = new EfDeletableEntityRepository<Invoice>(db);
            var salesRepository = new EfDeletableEntityRepository<Sale>(db);

            var service = new InvoiceService(
                billsRepository,
                customersRepository,
                paymentTypesRepository,
                invoiceRepository,
                salesRepository);

            var billId = Guid.NewGuid().ToString();
            var newSale = new Sale
            {
                Id = 1,
                Name = "Sale name",
                BillId = billId,
            };
            await salesRepository.AddAsync(newSale);
            await salesRepository.SaveChangesAsync();

            var newPaymentType = new PaymentType
            {
                Id = 1,
                Type = "Bank tranfers",
            };
            await paymentTypesRepository.AddAsync(newPaymentType);
            await paymentTypesRepository.SaveChangesAsync();

            var newInvoice = new Invoice
            {
                Id = 1,
                BillId = billId,
                PaymentTypeId = 1,
            };
            await invoiceRepository.AddAsync(newInvoice);
            billId = Guid.NewGuid().ToString();
            var idInvoice = 2;
            newInvoice = new Invoice
            {
                Id = idInvoice,
                BillId = billId,
                PaymentTypeId = 1,
            };
            await invoiceRepository.AddAsync(newInvoice);
            await invoiceRepository.SaveChangesAsync();

            var newCustomer = new Customer
            {
                Id = 1,
                Name = "Customer name",
                Address = "Address",
                BulstatNumber = "11111111",
                MOL = "MOL",
                ReceivedFrom = "ReceivedFrom",
                VATNumber = "9999999999",
            };
            await customersRepository.AddAsync(newCustomer);
            var customerSecondId = 2;
            var customerName = "Customer name second";
            newCustomer = new Customer
            {
                Id = customerSecondId,
                Name = customerName,
                Address = "Address",
                BulstatNumber = "11111111",
                MOL = "MOL",
                ReceivedFrom = "ReceivedFrom",
                VATNumber = "9999999999",
            };
            await customersRepository.AddAsync(newCustomer);
            await customersRepository.SaveChangesAsync();

            var result = await service.GetCustomerAsync(customerSecondId);

            Assert.NotNull(result);
            Assert.Equal(customerName, result.Name);
        }
    }
}
