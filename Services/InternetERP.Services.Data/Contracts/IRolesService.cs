namespace InternetERP.Services.Data.Contracts
{
    using System.Threading.Tasks;

    using InternetERP.Data.Models;

    public interface IRolesService
    {
        public Task<ApplicationUser> AddUserToRoleAsync(string userId, string roleName);

        public Task<ApplicationUser> RemoveUserToRoleAsync(string userId, string roleName);
    }
}
