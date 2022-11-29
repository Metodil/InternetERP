namespace InternetERP.Services.Data.Employee
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;

    using InternetERP.Common;
    using InternetERP.Data.Common.Repositories;
    using InternetERP.Data.Models;
    using InternetERP.Services.Data.Contracts;
    using InternetERP.Services.Mapping;
    using InternetERP.Web.ViewModels.Employee.Manager;
    using InternetERP.Web.ViewModels.Employee.Sales;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    public class SaleGoodsService : ISaleGoodsService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        private readonly IDeletableEntityRepository<Product> productsRepository;
        private readonly RoleManager<ApplicationRole> roleManaget;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IDeletableEntityRepository<Bill> billsRepository;
        private readonly IDeletableEntityRepository<Sale> saleRepository;
        private readonly IDeletableEntityRepository<InternetAccountType> internetAccountTypesRepository;
        private readonly IDeletableEntityRepository<InternetAccount> internetAccountRepository;

        public SaleGoodsService(
            UserManager<ApplicationUser> userManager,
            IDeletableEntityRepository<ApplicationUser> userRepository,
            IDeletableEntityRepository<Product> productsRepository,
            RoleManager<ApplicationRole> roleManaget,
            IHttpContextAccessor httpContextAccessor,
            IDeletableEntityRepository<Bill> billsRepository,
            IDeletableEntityRepository<Sale> saleRepository,
            IDeletableEntityRepository<InternetAccountType> internetAccountTypesRepository,
            IDeletableEntityRepository<InternetAccount> internetAccountRepository)
        {
            this.userManager = userManager;
            this.userRepository = userRepository;
            this.productsRepository = productsRepository;
            this.roleManaget = roleManaget;
            this.httpContextAccessor = httpContextAccessor;
            this.billsRepository = billsRepository;
            this.saleRepository = saleRepository;
            this.internetAccountTypesRepository = internetAccountTypesRepository;
            this.internetAccountRepository = internetAccountRepository;
        }

        public async Task<IEnumerable<T>> GetFilteredProductsPagingAsync<T>(
           int page,
           int itemsPerPage,
            #pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
           string? filterBy = null,
           string? categoryFilter = null)
            #pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        {
            if (filterBy == null)
            {
                return await this.productsRepository
                        .AllAsNoTracking()
                        .OrderBy(x => x.Name)
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
                return await this.productsRepository
                        .AllAsNoTracking()
                        .OrderBy(x => x.Name)
                        .Where(p => p.Name.Contains(filterBy))
                        .Skip((page - 1) * itemsPerPage)
                        .Take(itemsPerPage)
                        .To<T>()
                        .ToListAsync();
            }
        }

        #pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        public async Task<int> CountAsync(string? filterBy = null)
        #pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
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

        public async Task<bool> NewSaleId(string saleUserId, string selectedUserId)
        {
            var result = true;
            var selectedUser = await this.userRepository
                .All()
                .Where(u => u.Id == selectedUserId)
                .FirstAsync();

            // selectedUser.Town.Name != null ? selectedUser.Town.Name : "" + ", " +
            var userAddress =
                selectedUser.District != null ? selectedUser.District : string.Empty + ", " +
                selectedUser.Street != null ? selectedUser.Street : string.Empty;
            var newSaleId = new Bill
            {
                SelectedUserId = selectedUser.Id,
                UserFullName = selectedUser.FirstName + " " + selectedUser.LastName,
                UserAddress = userAddress,
                SaleUserId = saleUserId,
                StatusId = GlobalConstants.BillNewStatusId,
            };
            try
            {
                await this.billsRepository.AddAsync(newSaleId);
                await this.billsRepository.SaveChangesAsync();
            }
            catch (System.Exception)
            {
                // Log Error
                result = false;
            }

            return result;
        }

        public async Task<T> GetCurrentSaleId<T>(string saleUserId)
        {
            var tempT = await this.billsRepository
                .AllAsNoTracking()
                .Where(u => u.SaleUserId == saleUserId)
                .Where(u => u.Status.Name != GlobalConstants.BillFinishedStatus)
                .To<T>()
                .FirstAsync();
            return tempT;
        }

        public async Task<bool> SellProduct(AllProductsSalesViewModel input)
        {
            var result = true;
            var product = await this.productsRepository
                .AllAsNoTracking()
                .Where(p => p.Id == input.ProductId)
                .FirstAsync();
            var newSale = new Sale
            {
                BillId = input.BillId,
                ProductId = input.ProductId,
                StockQuantity = input.ProductQuantity,
                Name = product.Name,
                SellPrice = product.SellPrice,
                BayPrice = product.BayPrice,
            };
            try
            {
                await this.saleRepository
                    .AddAsync(newSale);
                await this.saleRepository.SaveChangesAsync();
            }
            catch (System.Exception)
            {
                // log error
                result = false;
            }

            return result;
        }

        public async Task<BillInfo> GetBillInfo(string billId)
        {
            return await this.billsRepository
                .AllAsNoTracking()
                .Where(b => b.Id == billId)
                .Select(b => new BillInfo
                {
                    Quantity = b.Sales.Sum(s => s.StockQuantity),
                    Totals = b.Sales.Sum(s => s.StockQuantity * s.SellPrice),
                })
                .FirstAsync();
        }

        public async Task<ICollection<InternetAccountType>> GetServices()
        {
            return await this.internetAccountTypesRepository
                .AllAsNoTracking()
                .ToListAsync();
        }

        public async Task<InternetAccount> GetInternetAccountInfo()
        {
            return await this.internetAccountRepository
                .AllAsNoTracking()
                .Include(a => a.AccountType)
                .FirstAsync();
        }

        public async Task<bool> SellService(SaleServicesViewModel input)
        {
            var result = true;
            var newSale = new Sale
            {
                BillId = input.BillId,
                InernetAccountId = input.SaleInternetAccountId,
                StockQuantity = 1,
                Name = "Montly Payment",
                SellPrice = input.MontlyPayment,
                BayPrice = 0,
            };
            try
            {
                await this.saleRepository
                    .AddAsync(newSale);
                await this.saleRepository.SaveChangesAsync();
                var internetAccount = await this.internetAccountRepository
                    .All()
                    .Where(ia => ia.InternetUserId == input.SaleInternetAccountId)
                    .FirstAsync();
                internetAccount.ExparedDate = input.ExparedDate;
                await this.internetAccountRepository.SaveChangesAsync();
            }
            catch (System.Exception)
            {
                // log error
                result = false;
            }

            return result;
        }
    }
}
