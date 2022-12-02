namespace InternetERP.Services.Contracts
{
    using System.Threading.Tasks;

    using InternetERP.Web.ViewModels.Employee.Sales;
    using PayPal.Api;

    public interface IPaypalService
    {
        public Task<Payment> CreatePayment(PayPalInputModel inpu);

        public Task<Payment> ExecutePayment(string payerId, string paymentId, string token);
    }
}
