namespace InternetERP.Services
{
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Threading.Tasks;

    using InternetERP.Data.Common.Repositories;
    using InternetERP.Services.Contracts;
    using InternetERP.Web.ViewModels.Employee.Sales;
    using Microsoft.Extensions.Configuration;
    using PayPal.Api;

    public class PaypalService : IPaypalService
    {
        private readonly IConfiguration configuration;
        private readonly IDeletableEntityRepository<InternetERP.Data.Models.Payment> paymentsRepository;

        public PaypalService(
            IConfiguration configuration,
            IDeletableEntityRepository<InternetERP.Data.Models.Payment> paymentsRepository)
        {
            this.configuration = configuration;
            this.paymentsRepository = paymentsRepository;
        }

        public async Task<Payment> CreatePayment(PayPalInputModel input)
        {
            var config = new Dictionary<string, string>();
            config.Add("mode", this.configuration["Logging:Paypal:Mode"]);
            config.Add("clientId", this.configuration["Logging:Paypal:ClientId"]);
            config.Add("clientSecret", this.configuration["Logging:Paypal:ClientSecret"]);

            var accessToken = new OAuthTokenCredential(config).GetAccessToken();

            var apiContext = new APIContext(accessToken)
            {
                Config = config,
            };

            try
            {
                Payment payment = new Payment
                {
                    intent = "sale",
                    payer = new Payer { payment_method = "paypal" },
                    transactions = new List<Transaction>
                    {
                        new Transaction
                        {
                            payee = new Payee
                            {
                                email = "sb-uiihk20806009@business.example.com",
                            },
                            amount = new Amount
                            {
                                currency = "USD",
                                total = input.Amount.ToString(),
                            },
                            description = "Products and Services from InternerERP. " + input.Description,
                        },
                    },
                    redirect_urls = new RedirectUrls
                    {
                        cancel_url = @"https://localhost:44319/Employee/Paypal/FailedPayment",
                        return_url = $@"https://localhost:44319/Employee/Paypal/SuccessedPayment?amount={input.Amount}&billId={input.BillId}",
                        // return_url = "nativexo://paypalpay",
                    },
                };
                var result = payment.Create(apiContext);

                var createdPayment = await Task.Run(() => payment.Create(apiContext));
                return createdPayment;
            }
            catch (WebException ex)
            {
                if (ex.Response is HttpWebResponse)
                {
                    HttpStatusCode statusCode = ((HttpWebResponse)ex.Response).StatusCode;
                    using (WebResponse wResponse = (HttpWebResponse)ex.Response)
                    {
                        using (Stream data = wResponse.GetResponseStream())
                        {
                            string text = new StreamReader(data).ReadToEnd();
                        }
                    }
                }

                return null;
            }
        }

        public async Task<Payment> ExecutePayment(string payerId, string paymentId, string token)
        {
            var config = new Dictionary<string, string>();
            config.Add("mode", this.configuration["Logging:Paypal:Mode"]);
            config.Add("clientId", this.configuration["Logging:Paypal:ClientId"]);
            config.Add("clientSecret", this.configuration["Logging:Paypal:ClientSecret"]);

            var accessToken = new OAuthTokenCredential(config).GetAccessToken();
            var apiContext = new APIContext(accessToken)
            {
                Config = config,
            };

            PaymentExecution paymentExecution = new PaymentExecution() { payer_id = payerId };
            Payment payment = new Payment() { id = paymentId };
            Payment executedPayment = await Task.Run(() => payment.Execute(apiContext, paymentExecution));

            return executedPayment;
        }
    }
}
