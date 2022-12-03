using System.Threading.Tasks;

namespace InternetERP.Services.Data.Employee.Contracts
{
    public interface IBillService
    {
        Task AddPaymentToBill(decimal amount, string billId);

        Task ChangeStatus(string billId, int status);
    }
}
