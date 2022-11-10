namespace InternetERP.Services.Data.Administration
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using InternetERP.Data.Common.Repositories;
    using InternetERP.Data.Models;
    using InternetERP.Services.Data.Contracts;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    public class RolesService : IRolesService
    {
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly ICustomUsersService usersService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IDeletableEntityRepository<ApplicationRole> rolesRepository;

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

        public async Task<IEnumerable<string>> GetAllRolesAsync()
        {
            return await this.roleManager
                    .Roles
                    .Select(r => r.Name)
                    .ToListAsync();
        }
    }
}
