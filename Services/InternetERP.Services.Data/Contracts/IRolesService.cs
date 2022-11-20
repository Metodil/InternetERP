namespace InternetERP.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using InternetERP.Data.Models;
    using Microsoft.AspNetCore.Identity;

    public interface IRolesService
    {
        public Task<ApplicationUser> AddUserToRoleAsync(string userId, string roleName);

        public Task<ApplicationUser> RemoveUserToRoleAsync(string userId, string roleName);

        public Task<string> GetRoleNameByIdAsync(string roleId);

        public Task<IEnumerable<string>> GetAllRolesNamesAsync();

        public Task<IEnumerable<ApplicationRole>> GetAllRolesAsync();

        public Task<IEnumerable<T>> GetAllRolesPagingAsync<T>(int page, int itemsPerPage);

        public Task<ApplicationRole> AddRole(ApplicationRole applicationRole);

        public Task<ApplicationRole> GetRoleByIdAsync(string id);

        public Task<ApplicationRole> UpdateRoleByIdAsync(string id, string name);

        public string GetRolesStyles(string roleName);

        public Task<IdentityResult> DeleteAsync(string id);

        public Task<int> CountAsync();
    }
}
