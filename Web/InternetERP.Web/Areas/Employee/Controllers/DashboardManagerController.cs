namespace InternetERP.Web.Areas.Employee.Controllers
{
    using InternetERP.Services.Data;
    using InternetERP.Web.ViewModels.Employee.DashboardManager;
    using Microsoft.AspNetCore.Mvc;

    public class DashboardManagerController : EmployeeController
    {
        private readonly ISettingsService settingsService;

        public DashboardManagerController(ISettingsService settingsService)
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
