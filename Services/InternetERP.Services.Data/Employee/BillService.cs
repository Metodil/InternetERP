namespace InternetERP.Services.Data.Employee
{
    using System.Linq;
    using System.Threading.Tasks;

    using InternetERP.Data.Common.Repositories;
    using InternetERP.Data.Models;
    using InternetERP.Services.Data.Employee.Contracts;
    using Microsoft.EntityFrameworkCore;

    public class BillService : IBillService
    {
        private readonly IDeletableEntityRepository<Bill> billRepository;
        private readonly IDeletableEntityRepository<Payment> paymentsRepository;
        private readonly IDeletableEntityRepository<Sale> salesRepository;

        public BillService(
            IDeletableEntityRepository<Bill> billRepository,
            IDeletableEntityRepository<Payment> paymentsRepository,
            IDeletableEntityRepository<Sale> salesRepository)
        {
            this.billRepository = billRepository;
            this.paymentsRepository = paymentsRepository;
            this.salesRepository = salesRepository;
        }

        public async Task ChangeStatus(string billId, int statusId)
        {
            var bill = await this.billRepository
                .All()
                .Where(b => b.Id == billId)
                .FirstAsync();
            bill.StatusId = statusId;
            await this.billRepository.SaveChangesAsync();
        }

        public async Task AddPaymentToBill(decimal amount, string billId, string provider)
        {
            var payment = new Payment
            {
                Amount = amount,
                BillId = billId,
                Provider = provider,
            };
            await this.paymentsRepository.AddAsync(payment);
            await this.paymentsRepository.SaveChangesAsync();
        }

        public async Task<decimal> GetBillAmount(string billId)
        {
            var amount = await this.salesRepository
                .AllAsNoTracking()
                .Where(s => s.BillId == billId)
                .SumAsync(s => s.SellPrice * s.StockQuantity);
            return amount;
        }
    }
}
