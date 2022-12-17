namespace InternetERP.Web.ViewComponents
{
    using System.Threading.Tasks;
    using InternetERP.Data.Models;
    using InternetERP.Services.Data.Contracts;
    using InternetERP.Web.ViewModels.InternetAccount.Dashboard;
    using Microsoft.AspNetCore.Mvc;

    [ViewComponent(Name = "LatestPaymentsForInternetAccount")]
    public class LatestPaymentsForInternetAccountViewComponent : ViewComponent
    {
        private readonly ISaleGoodsService salesService;

        public LatestPaymentsForInternetAccountViewComponent(
            ISaleGoodsService salesService)
        {
            this.salesService = salesService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string internetAccountId)
        {
            var viewModel = new LatestsPaymentsForInternetAccountsViewModel()
            {
                LatestPaymentsForInternetAccounts = await this.salesService.GetLatestPaymentsForInternetAccountAsync(internetAccountId),
            };

            return this.View(viewModel);
        }
    }
}
