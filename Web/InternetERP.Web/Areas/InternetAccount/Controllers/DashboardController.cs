namespace InternetERP.Web.Areas.InternetAccount.Controllers
{
    using System.Security.Claims;

    using InternetERP.Services.Data.Contracts;
    using InternetERP.Web.Areas.Employee.Controllers;
    using InternetERP.Web.ViewModels.InternetAccount.Dashboard;
    using Microsoft.AspNetCore.Mvc;

    public class DashboardController : InternetAccountController
    {
        private readonly ISettingsService settingsService;

        public DashboardController(ISettingsService settingsService)
        {
            this.settingsService = settingsService;
        }

        public IActionResult Index()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var viewModel = new IndexViewModel
            {
                InternetAccointId = userId,
            };

            return this.View(viewModel);
        }
    }
}
