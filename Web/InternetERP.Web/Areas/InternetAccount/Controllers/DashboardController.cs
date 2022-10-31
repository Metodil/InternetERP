namespace InternetERP.Web.Areas.InternetAccount.Controllers
{
    using InternetERP.Services.Data;
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
            var viewModel = new IndexViewModel { SettingsCount = this.settingsService.GetCount(), };
            return this.View(viewModel);
        }
    }
}
