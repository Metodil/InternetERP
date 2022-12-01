namespace InternetERP.Services.Contracts
{
    using System.Threading.Tasks;

    using PayPal.Api;

    public interface IPaypalService
    {
        public Task<Payment> CreatePayment(decimal value, string description);

        public Task<Payment> ExecutePayment(string payerId, string paymentId, string token);
    }
}
