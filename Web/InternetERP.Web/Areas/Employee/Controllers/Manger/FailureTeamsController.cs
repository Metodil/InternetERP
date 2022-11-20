namespace InternetERP.Web.Areas.Employee.Controllers.Manger
{
    using System.Threading.Tasks;

    using InternetERP.Services.Data.Contracts;
    using InternetERP.Web.ViewModels.Employee.Manager;
    using Microsoft.AspNetCore.Mvc;

    public class FailureTeamsController : EmployeeController
    {
        private readonly IFailureTeamsService failureTeamsService;
        private readonly ICustomUsersService customUsersService;

        public FailureTeamsController(
            IFailureTeamsService failureTeamsService,
            ICustomUsersService customUsersService)
        {
            this.failureTeamsService = failureTeamsService;
            this.customUsersService = customUsersService;
        }

        [HttpGet]
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        public async Task<IActionResult> Index(string? managedTeam)
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        {
            if (
                await this.customUsersService.CountAsync() == 0 ||
                await this.failureTeamsService.CountAsync() == 0)
            {
                // TODO check not found in controlles
                return this.NotFound();
            }

            if (managedTeam == null)
            {
                managedTeam = string.Empty;
            }

            var model = new FailureTeamsViewModel
            {
                ManagedTeam = managedTeam,
                FailureTeams = await this.failureTeamsService.GetAllTeamsAsync(),
                FreeEmployees = await this.failureTeamsService.GetTechnicianEmployeesAsync(),
                EmployeesInTeam = await this.failureTeamsService.GetEmployeesInTeamAsync(managedTeam),
            };

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(FailureTeamsViewModel model)
        {
            if (model.ManagedTeam == null)
            {
                this.ModelState.AddModelError(string.Empty, "Choose a management team!");
                model.FailureTeams = await this.failureTeamsService.GetAllTeamsAsync();
                model.FreeEmployees = await this.failureTeamsService.GetTechnicianEmployeesAsync();
                return this.View(model);
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            if (await this.failureTeamsService.UpdateEmployeeToTeam(model.ManagedTeam, model.SelectedEmployee))
            {
                model.SuccessfullyMsg = $"Successfully updated {model.ManagedTeam} team!";
            }
            else
            {
                this.ModelState.AddModelError(string.Empty, $"Error on updated {model.ManagedTeam} team!");
            }

            var updatedModel = new FailureTeamsViewModel
            {
                ManagedTeam = model.ManagedTeam,
                FailureTeams = await this.failureTeamsService.GetAllTeamsAsync(),
                FreeEmployees = await this.failureTeamsService.GetTechnicianEmployeesAsync(),
                EmployeesInTeam = await this.failureTeamsService.GetEmployeesInTeamAsync(model.ManagedTeam),
            };

            return this.View(updatedModel);
        }
    }
}
