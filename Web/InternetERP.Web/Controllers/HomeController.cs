namespace InternetERP.Web.Controllers
{
    using System.Diagnostics;
    using System.Threading.Tasks;

    using InternetERP.Services.Data.Home.Contracts;
    using InternetERP.Web.ViewModels;
    using InternetERP.Web.ViewModels.Home;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly IHomeService homeService;

        public HomeController(
            IHomeService homeService)
        {
            this.homeService = homeService;
        }

        public async Task<IActionResult> Index()
        {
            var model = new HomeViewModel
            {
                PromoProducts = await this.homeService.GetPromoProducts(),
            };

            return this.View(model);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
