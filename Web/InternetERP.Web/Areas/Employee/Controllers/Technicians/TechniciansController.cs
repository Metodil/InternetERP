namespace InternetERP.Web.Areas.Employee.Controllers.Technician
{
    using System.Threading.Tasks;

    using InternetERP.Common;
    using InternetERP.Services.Data.Contracts;
    using InternetERP.Web.ViewModels.Employee.Manager;
    using InternetERP.Web.ViewModels.Employee.Technicians;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.TechnicianRoleName)]
    public class TechniciansController : EmployeeController
    {
        private readonly ITechniciansService techniciansService;

        public TechniciansController(
            ITechniciansService techniciansService)
        {
            this.techniciansService = techniciansService;
        }

        public async Task<IActionResult> AllFailures(
            int id = 1,
            string filterByStatus = null)
        {
            var model = new AllFailuresViewModel
            {
                Failures = await this.techniciansService.GetAllFailuresAsync<FailureListViewModel>(
                    id,
                    GlobalConstants.ItemsPerPageList,
                    filterByStatus),
                ItemsPerPage = GlobalConstants.ItemsPerPageList,
                PageNumber = id,
                AspAction = nameof(this.AllFailures),
                ItemsCount = await this.techniciansService.CountAsync(filterByStatus),
                FilterBy = filterByStatus,
            };

            return this.View(model);
        }
    }
}
