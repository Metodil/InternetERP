namespace InternetERP.Services.Data.Administration
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using InternetERP.Data.Common.Repositories;
    using InternetERP.Data.Models;
    using InternetERP.Services.Data.Administration.Contracts;
    using InternetERP.Services.Mapping;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    public class RolesService : IRolesService
    {
        private readonly ICustomUsersService usersService;
        private readonly IDeletableEntityRepository<ApplicationRole> rolesRepository;
        private readonly UserManager<ApplicationUser> userManager;
        private RoleManager<ApplicationRole> roleManager;

        public RolesService(
            RoleManager<ApplicationRole> roleManager,
            ICustomUsersService usersService,
            UserManager<ApplicationUser> userManager,
            IDeletableEntityRepository<ApplicationRole> rolesRepository)
        {
            this.roleManager = roleManager;
            this.usersService = usersService;
            this.userManager = userManager;
            this.rolesRepository = rolesRepository;
        }

        public async Task<ApplicationUser> AddUserToRoleAsync(string userId, string roleName)
        {
            var currentUser = await this.usersService.GetUserByIdAsync(userId);
            await this.userManager.AddToRoleAsync(currentUser, roleName);

            return currentUser;
        }

        public async Task<ApplicationUser> RemoveUserToRoleAsync(string userId, string roleName)
        {
            var currentUser = await this.usersService.GetUserByIdAsync(userId);
            await this.userManager.RemoveFromRoleAsync(currentUser, roleName);

            return currentUser;
        }

        public async Task<string> GetRoleNameByIdAsync(string roleId)
        {
            var role = await this.roleManager
                    .Roles.FirstOrDefaultAsync(r => r.Id == roleId);
            var roleName = role.Name ?? string.Empty;
            return roleName;
        }

        public async Task<IEnumerable<string>> GetAllRolesNamesAsync()
        {
            return await this.roleManager
                    .Roles
                    .Select(r => r.Name)
                    .ToListAsync();
        }

        public async Task<IEnumerable<ApplicationRole>> GetAllRolesAsync()
        {
            return await this.roleManager
                    .Roles
                    .ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllRolesPagingAsync<T>(int page, int itemsPerPage)
        {
            return await this.roleManager
                .Roles
                .OrderBy(x => x.Name)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .To<T>()
                .ToListAsync();
        }

        public async Task<int> CountAsync()
        {
            return await this.roleManager
               .Roles
               .CountAsync();
        }

        public async Task<ApplicationRole> AddRole(ApplicationRole applicationRole)
        {
            var role = new ApplicationRole(applicationRole.Name);

            var result = await this.roleManager.RoleExistsAsync(applicationRole.Name);
            if (!result)
            {
                await this.roleManager.CreateAsync(role);
            }

            return role;
        }

        public async Task<ApplicationRole> GetRoleByIdAsync(string id)
        {
            var role = await this.roleManager.FindByIdAsync(id);

            return role;
        }

        public async Task<ApplicationRole> UpdateRoleByIdAsync(string id, string name)
        {
            var role = await this.roleManager.FindByIdAsync(id);
            if (role != null)
            {
                role.Name = name;
                var idResult = await this.roleManager.UpdateAsync(role);
                if (idResult.Succeeded)
                {
                    return role;
                }
            }

            return null;
        }

        public async Task<IdentityResult> DeleteAsync(string id)
        {
            var role = await this.roleManager.FindByIdAsync(id);
            if (role != null)
            {
                var idResult = await this.roleManager.DeleteAsync(role);
                if (idResult.Succeeded)
                {
                    return idResult;
                }
            }

            return null;
        }

        public string GetRolesStyles(string roleName)
        {
            var roleClass = string.Empty;
            switch (roleName)
            {
                case "Administrator":
                    roleClass = "badge bg-light-danger text-danger font-weight-medium";
                    break;
                case "Manager":
                    roleClass = "badge bg-light-info text-info font-weight-medium";
                    break;
                case "Sales":
                    roleClass = "badge bg-light-warning text-danger font-weight-medium";
                    break;
                case "Employee":
                    roleClass = "badge bg-light-success text-warning font-weight-medium";
                    break;
                case "InternetAccount":
                    roleClass = "badge bg-light-primary text-dark font-weight-medium";
                    break;
                default:
                    roleClass = "badge bg-light-info text-danger font-weight-medium";
                    break;
            }

            return roleClass;
        }
    }
}
