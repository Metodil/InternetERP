namespace InternetERP.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using InternetERP.Common;
    using InternetERP.Data.Models;
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

        [HttpGet]
        public async Task<IActionResult> Index(int id = 1)
        {
            var model = new AllRolesViewModel
            {
                Roles = await this.rolesService.GetAllRolesPagingAsync<RoleListViewModel>(
                    id, GlobalConstants.ItemsPerPageList),
                ItemsPerPage = GlobalConstants.ItemsPerPageList,
                PageNumber = id,
                AspAction = nameof(this.Index),
                ItemsCount = await this.rolesService.CountAsync(),
            };

            return this.View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")] ApplicationRole applicationRole)
        {
            if (this.ModelState.IsValid)
            {
                applicationRole = await this.rolesService.AddRole(applicationRole);
                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(applicationRole);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id) || await this.rolesService.GetAllRolesAsync() == null)
            {
                return this.NotFound();
            }

            var role = await this.rolesService.GetRoleByIdAsync(id);
            if (role == null)
            {
                return this.NotFound();
            }

            return this.View(role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Name,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] ApplicationRole applicationRole)
        {
            if (id != applicationRole.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                await this.rolesService.UpdateRoleByIdAsync(applicationRole.Id, applicationRole.Name);

                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(applicationRole);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id) || await this.rolesService.GetAllRolesNamesAsync() == null)
            {
                return this.NotFound();
            }

            var role = await this.rolesService.GetRoleByIdAsync(id);
            if (role == null)
            {
                return this.NotFound();
            }

            return this.View(role);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            // TODO Do not real delete role, only mark
            if (await this.rolesService.GetAllRolesNamesAsync() == null)
            {
                return this.Problem("There is not 'Application Roles'.");
            }

            var role = await this.rolesService.GetRoleByIdAsync(id);
            if (role != null)
            {
                var result = await this.rolesService.DeleteAsync(role.Id);
            }

            return this.RedirectToAction(nameof(this.Index));
        }

        public async Task<IActionResult> AddRoleToUser(ChangeRoleInputModel inputModel)
        {
            await this.rolesService.AddUserToRoleAsync(inputModel.UserId, inputModel.RoleName);

            return this.Redirect($"/Administration/Users/Profile?userId={inputModel.UserId}");
        }

        public async Task<IActionResult> RemoveRoleToUser(ChangeRoleInputModel inputModel)
        {
            await this.rolesService.RemoveUserToRoleAsync(inputModel.UserId, inputModel.RoleName);
            return this.Redirect($"/Administration/Users/Profile?userId={inputModel.UserId}");
        }
    }
}
