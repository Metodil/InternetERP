namespace InternetERP.Web.Areas.Employee.Controllers.Manger
{
    using InternetERP.Services.Data.Contracts;
    using InternetERP.Web.ViewModels.Employee.Manager;
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
            var viewModel = new DashboardIndexViewModel { SettingsCount = this.settingsService.GetCount(), };
            return this.View(viewModel);
        }
    }
}
