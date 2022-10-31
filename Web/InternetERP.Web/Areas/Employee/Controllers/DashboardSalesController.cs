namespace InternetERP.Web.Areas.Employee.Controllers
{
    using InternetERP.Services.Data;
    using InternetERP.Web.ViewModels.Employee.DashboardSales;
    using Microsoft.AspNetCore.Mvc;

    public class DashboardSalesController :EmployeeController
    {
        private readonly ISettingsService settingsService;

        public DashboardSalesController(ISettingsService settingsService)
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
