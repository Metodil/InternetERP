namespace InternetERP.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using InternetERP.Services.Data.Contracts;
    using InternetERP.Web.ViewModels.Administration.Roles;
    using Microsoft.AspNetCore.Mvc;

    public class RolesController : AdministrationController
    {
        private readonly IRolesService rolesService;

        public RolesController(IRolesService rolesService)
        {
            this.rolesService = rolesService;
        }

        public async Task<IActionResult> Add(ChangeRoleInputModel inputModel)
        {
            await this.rolesService.AddUserToRoleAsync(inputModel.UserId, inputModel.RoleName);
            return this.Redirect("/Administration/Users/All");
        }

        public async Task<IActionResult> Remove(ChangeRoleInputModel inputModel)
        {
            await this.rolesService.RemoveUserToRoleAsync(inputModel.UserId, inputModel.RoleName);
            return this.Redirect("/Administration/Users/All");
        }
    }
}
