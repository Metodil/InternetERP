namespace InternetERP.Services.Data.Tests
{
    using System;
    using System.Threading.Tasks;

    using InternetERP.Data;
    using InternetERP.Data.Models;
    using InternetERP.Data.Repositories;
    using InternetERP.Services.Data.Contracts;
    using InternetERP.Services.Data.Employee;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using Moq;
    using Xunit;

    public class BillServiceTests
    {
        [Fact]
        public async Task ChangeStatusProperlyUpdateSattus()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                      .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var db = new ApplicationDbContext(options);
            var billRepository = new EfDeletableEntityRepository<Bill>(db);
            var paymentsRepository = new EfDeletableEntityRepository<Payment>(db);
            new MapperInitializationProfile();

            var service = new BillService(
                billRepository,
                paymentsRepository);

            var billId = Guid.NewGuid().ToString();
            var bill = new Bill
            {
                Id = billId,
                StatusId = 1,
            };
            await billRepository.AddAsync(bill);
            await billRepository.SaveChangesAsync();
            var newStatus = 2;
            await service.ChangeStatus(billId, newStatus);

            var result = billRepository
                .AllAsNoTracking()
                .FirstOrDefaultAsync();

            Assert.NotNull(result);
            Assert.Equal(newStatus, result.Result.StatusId);
        }

        [Fact]
        public async Task AddPaymentToBillProperlyUpdateAmount()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                      .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var db = new ApplicationDbContext(options);
            var billRepository = new EfDeletableEntityRepository<Bill>(db);
            var paymentsRepository = new EfDeletableEntityRepository<Payment>(db);
            new MapperInitializationProfile();

            var service = new BillService(
                billRepository,
                paymentsRepository);

            var billId = Guid.NewGuid().ToString();
            var newAmount = 20m;
            await service.AddPaymentToBill(newAmount, billId);

            var result = paymentsRepository
                .AllAsNoTracking()
                .FirstOrDefaultAsync(p => p.BillId == billId);

            Assert.NotNull(result);
            Assert.Equal(newAmount, result.Result.Amount);
        }
    }
}
