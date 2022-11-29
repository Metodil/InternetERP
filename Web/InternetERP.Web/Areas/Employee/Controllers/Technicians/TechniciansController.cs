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

        [HttpGet]
        public async Task<IActionResult> AllFailures(int page = 0, int id = 1, int selectedStatus = 0, string filterBy = null)
        {
            if (filterBy != null)
            {
                int.TryParse(filterBy, out selectedStatus);
            }

            if (page != 0)
            {
                id = page;
            }

            var model = new AllFailuresViewModel
            {
                Failures = await this.techniciansService.GetAllFailuresAsync<FailureListViewModel>(
                    id,
                    GlobalConstants.ItemsPerPageList,
                    selectedStatus),
                ItemsPerPage = GlobalConstants.ItemsPerPageList,
                PageNumber = id,
                AspAction = nameof(this.AllFailures),
                ItemsCount = await this.techniciansService.CountAsync(selectedStatus),
                FilterBy = selectedStatus.ToString(),
                SelectedStatus = selectedStatus,
            };

            return this.View(model);
        }
    }
}
