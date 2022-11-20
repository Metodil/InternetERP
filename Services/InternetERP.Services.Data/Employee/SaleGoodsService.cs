namespace InternetERP.Services.Data.Employee
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using InternetERP.Common;
    using InternetERP.Data.Common.Repositories;
    using InternetERP.Data.Models;
    using InternetERP.Services.Data.Contracts;
    using InternetERP.Services.Mapping;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    public class SaleGoodsService : ISaleGoodsService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        private readonly RoleManager<ApplicationRole> roleManaget;
        private readonly IHttpContextAccessor httpContextAccessor;

        public SaleGoodsService(
            UserManager<ApplicationUser> userManager,
            IDeletableEntityRepository<ApplicationUser> userRepository,
            RoleManager<ApplicationRole> roleManaget,
            IHttpContextAccessor httpContextAccessor)
        {
            this.userManager = userManager;
            this.userRepository = userRepository;
            this.roleManaget = roleManaget;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<T>> GetFilteredUsersPagingAsync<T>(
           int page,
           int itemsPerPage,
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
           string? filterBy = null,
           string? categoryFilter = null)
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        {
            var role = await this.roleManaget.FindByNameAsync(GlobalConstants.InternetAccountRoleName);
            if (filterBy == null)
            {
                return await this.userRepository
                        .AllAsNoTracking()
                        .Where(u => u.Roles.Any(r => r.RoleId == role.Id))
                        .OrderBy(x => x.FirstName)
                        .ThenBy(x => x.LastName)
                        .Skip((page - 1) * itemsPerPage)
                        .Take(itemsPerPage)
                        .To<T>()
                        .ToListAsync();
            }
            else
            {
                var filterList = filterBy
                    .Split(' ', System.StringSplitOptions.RemoveEmptyEntries)
                    .ToList();
                return await this.userRepository
                        .AllAsNoTracking()
                        .Where(u => u.Roles.Any(r => r.RoleId == role.Id))
                        .OrderBy(x => x.FirstName)
                        .ThenBy(x => x.LastName)
                        .Where(p => p.FirstName.Contains(filterBy) ||
                            p.LastName.Contains(filterBy))
                        .Skip((page - 1) * itemsPerPage)
                        .Take(itemsPerPage)
                        .To<T>()
                        .ToListAsync();
            }
        }

        public async Task<int> CountAsync(string? filterBy = null)
        {
            var role = await this.roleManaget.FindByNameAsync(GlobalConstants.InternetAccountRoleName);

            if (filterBy == null)
            {
                return await this.userRepository
                                .All()
                    .Where(u => u.Roles.Any(r => r.RoleId == role.Id))
                    .OrderBy(x => x.FirstName)
                    .ThenBy(x => x.LastName)
                                .CountAsync();
            }

            return await this.userRepository
                                .All()
                    .Where(u => u.Roles.Any(r => r.RoleId == role.Id))
                    .Where(p => p.FirstName.Contains(filterBy) ||
                        p.LastName.Contains(filterBy))
                    .OrderBy(x => x.FirstName)
                    .ThenBy(x => x.LastName)
                                .CountAsync();

        }

        public async Task<IEnumerable<T>> GetAllUsersAsync<T>()
        {
            var role = await this.roleManaget.FindByNameAsync(GlobalConstants.InternetAccountRoleName);

            return await this.userRepository
                    .AllAsNoTracking()
                    .Where(u => u.Roles.Any(r => r.RoleId == role.Id))
                    .OrderBy(x => x.FirstName)
                    .ThenBy(x => x.LastName)
                    .To<T>()
                    .ToListAsync();
        }

        public async Task<T> GetCurrentSaleId<T>(string id)
        {
            var tempT = await this.userRepository
                .AllAsNoTracking()
                .Where(u => u.Id == id)
                .To<T>()
                .FirstAsync();
            return tempT;
        }
    }
}
