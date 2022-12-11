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

        public BillService(
            IDeletableEntityRepository<Bill> billRepository,
            IDeletableEntityRepository<Payment> paymentsRepository)
        {
            this.billRepository = billRepository;
            this.paymentsRepository = paymentsRepository;
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

        public async Task AddPaymentToBill(decimal amount, string billId)
        {
            var payment = new Payment
            {
                Amount = amount,
                BillId = billId,
                Provider = "PayPal",
            };
            await this.paymentsRepository.AddAsync(payment);
            await this.paymentsRepository.SaveChangesAsync();
        }
    }
}
