namespace InternetERP.Services.Data.Administration
{
    using System.Threading.Tasks;

    using InternetERP.Data.Common.Repositories;
    using InternetERP.Data.Models;
    using InternetERP.Services.Data.Contracts;
    using Microsoft.AspNetCore.Identity;

    public class RolesService : IRolesService
    {
        private readonly ICustomUsersService usersService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IDeletableEntityRepository<ApplicationRole> rolesRepository;

        public RolesService(
            ICustomUsersService usersService,
            UserManager<ApplicationUser> userManager,
            IDeletableEntityRepository<ApplicationRole> rolesRepository)
        {
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
    }
}
