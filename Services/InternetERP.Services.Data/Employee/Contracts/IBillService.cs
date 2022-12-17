using System.Threading.Tasks;

namespace InternetERP.Services.Data.Employee.Contracts
{
    public interface IBillService
    {
        Task AddPaymentToBill(decimal amount, string billId, string provider);

        Task ChangeStatus(string billId, int status);

        Task<decimal> GetBillAmount(string billId);
    }
}
