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
        private readonly IDeletableEntityRepository<Bill> billsRepository;
        private readonly IDeletableEntityRepository<Sale> salesRepository;
        private readonly IDeletableEntityRepository<InternetAccountType> internetAccountTypesRepository;
        private readonly IDeletableEntityRepository<InternetAccount> internetAccountRepository;
        private readonly IDeletableEntityRepository<Failure> failuresRepository;

        public SaleGoodsService(
            UserManager<ApplicationUser> userManager,
            IDeletableEntityRepository<ApplicationUser> userRepository,
            IDeletableEntityRepository<Product> productsRepository,
            RoleManager<ApplicationRole> roleManaget,
            IDeletableEntityRepository<Bill> billsRepository,
            IDeletableEntityRepository<Sale> salesRepository,
            IDeletableEntityRepository<InternetAccountType> internetAccountTypesRepository,
            IDeletableEntityRepository<InternetAccount> internetAccountRepository,
            IDeletableEntityRepository<Failure> failuresRepository)
        {
            this.userManager = userManager;
            this.userRepository = userRepository;
            this.productsRepository = productsRepository;
            this.roleManaget = roleManaget;
            this.billsRepository = billsRepository;
            this.salesRepository = salesRepository;
            this.internetAccountTypesRepository = internetAccountTypesRepository;
            this.internetAccountRepository = internetAccountRepository;
            this.failuresRepository = failuresRepository;
        }

        public async Task<ICollection<T>> GetFilteredProductsPagingAsync<T>(
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
                    .AllAsNoTracking()
                    .Where(u => u.Roles.Any(r => r.RoleId == role.Id))
                    .OrderBy(x => x.FirstName)
                    .ThenBy(x => x.LastName)
                                .CountAsync();
            }

            return await this.userRepository
                    .AllAsNoTracking()
                    .Where(u => u.Roles.Any(r => r.RoleId == role.Id))
                    .Where(p => p.FirstName.Contains(filterBy) ||
                        p.LastName.Contains(filterBy))
                    .OrderBy(x => x.FirstName)
                    .ThenBy(x => x.LastName)
                                .CountAsync();
        }

        public async Task<ICollection<T>> GetAllUsersAsync<T>()
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
                StockQuantity = input.StockQuantity,
                Name = product.Name,
                SellPrice = product.SellPrice,
                BayPrice = product.BayPrice,
            };
            try
            {
                await this.salesRepository
                    .AddAsync(newSale);
                await this.salesRepository.SaveChangesAsync();
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
                await this.salesRepository
                    .AddAsync(newSale);
                await this.salesRepository.SaveChangesAsync();
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

        public async Task<ICollection<Failure>> GetFailures(int internetAccountUserId)
        {
            return await this.failuresRepository
                .AllAsNoTracking()
                .Include(f => f.StatusFailure)
                .Include(f => f.FailurePhases)
                .ThenInclude(fp => fp.FailureTeam)
                .Include(f => f.FailurePhases)
                .ThenInclude(fp => fp.User)
                .Include(f => f.FailurePhases)
                .ThenInclude(fp => fp.StatusFailure)
                .OrderBy(f => f.CreatedOn)
                .Where(f => f.AccountId == internetAccountUserId)
                .Where(f => !f.IsPaid && f.Price > 0)
                .ToListAsync();
        }

        public async Task<bool> SaleFailureAmount(PayFailureViewModel input)
        {
            var result = true;
            var newSale = new Sale
            {
                BillId = input.BillId,
                InernetAccountId = input.SaleInternetAccountId,
                StockQuantity = 1,
                Name = "Failure Payments for: " + input.FailureIdsForUpdate,
                SellPrice = input.FailurePayment,
                BayPrice = 0,
            };
            try
            {
                await this.salesRepository
                    .AddAsync(newSale);
                await this.salesRepository.SaveChangesAsync();
                List<string> failureIdsForUpdate = input.FailureIdsForUpdate
                    .Split(',', System.StringSplitOptions.RemoveEmptyEntries)
                    .ToList();

                var failureForUpdate = await this.failuresRepository
                    .All()
                    .Where(f => failureIdsForUpdate.Contains(f.Id.ToString()))
                    .ToListAsync();
                foreach (var failure in failureForUpdate)
                {
                    failure.IsPaid = true;
                }

                await this.failuresRepository.SaveChangesAsync();
            }
            catch (System.Exception)
            {
                // log error
                result = false;
            }

            return result;
        }

        public async Task<ICollection<Sale>> GetSales(string billId)
        {
            return await this.salesRepository
                .AllAsNoTracking()
                .OrderBy(s => s.CreatedOn)
                .Where(s => s.BillId == billId)
                .ToListAsync();
        }

        public async Task UpdateCheckout(CheckoutViewModel input)
        {
            var index = 0;
            foreach (var stockId in input.StockIds)
            {
                var stock = await this.salesRepository
                    .All()
                    .Where(s => s.Id == stockId)
                    .FirstAsync();
                var newQuantity = input.Quantities[index];
                if (newQuantity == 0)
                {
                    this.salesRepository.Delete(stock);
                }
                else
                {
                    stock.StockQuantity = input.Quantities[index];
                }

                index++;
            }

            await this.salesRepository.SaveChangesAsync();
        }

        public async Task<Failure> GetFailureById(int id)
        {
            return await this.failuresRepository
                .AllAsNoTracking()
                .Include(f => f.StatusFailure)
                .Include(f => f.FailurePhases)
                .ThenInclude(fp => fp.FailureTeam)
                .Include(f => f.FailurePhases)
                .ThenInclude(fp => fp.User)
                .Include(f => f.FailurePhases)
                .ThenInclude(fp => fp.StatusFailure)
                .FirstOrDefaultAsync(f => f.Id == id);
        }
    }
}
