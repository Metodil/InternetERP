namespace InternetERP.Services.Data.Tests
{
    using System;
    using System.Threading.Tasks;

    using InternetERP.Data;
    using InternetERP.Data.Models;
    using InternetERP.Data.Repositories;
    using InternetERP.Services.Data.Employee;
    using InternetERP.Services.Mapping;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using Moq;
    using Xunit;

    public class SaleGoodsServiceTests
    {
        [Fact]
        public async Task GetFilteredProductsPagingAsynProperlyRetrunProductByPage()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                      .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var db = new ApplicationDbContext(options);
            var userManager = new Mock<UserManager<ApplicationUser>>(
                new Mock<IUserStore<ApplicationUser>>().Object, null, null, null, null, null, null, null, null)
                .Object;
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(db);
            var productRepository = new EfDeletableEntityRepository<Product>(db);
            var roleManager = this.GetRoleManagerMock<ApplicationRole>();
            var billsRepository = new EfDeletableEntityRepository<Bill>(db);
            var salesRepository = new EfDeletableEntityRepository<Sale>(db);
            var internetAccountTypesRepository = new EfDeletableEntityRepository<InternetAccountType>(db);
            var internetAccountRepository = new EfDeletableEntityRepository<InternetAccount>(db);
            var failuresRepository = new EfDeletableEntityRepository<Failure>(db);
            new MapperInitializationProfile();

            var service = new SaleGoodsService(
                userManager,
                userRepository,
                productRepository,
                roleManager.Object,
                billsRepository,
                salesRepository,
                internetAccountTypesRepository,
                internetAccountRepository,
                failuresRepository);
            var newProduct = new Product
            {
                Id = 1,
                Name = "Name of Product",
                SellPrice = 20m,
                BayPrice = 15m,
                StockQuantity = 1,
                Description = "Description",
            };
            await productRepository.AddAsync(newProduct);
            await productRepository.SaveChangesAsync();

            var page = 1;
            var itemsPerPage = 5;
            var filterName = "Name of Product";

            var result = service.GetFilteredProductsPagingAsync<ProductTest>(
                page,
                itemsPerPage,
                filterName);

            Assert.Equal(1, result.Result.Count);
        }

        [Fact]
        public async Task NewSaleIdProperlyUpdateBill()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                      .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var db = new ApplicationDbContext(options);
            var userManager = new Mock<UserManager<ApplicationUser>>(
                new Mock<IUserStore<ApplicationUser>>().Object, null, null, null, null, null, null, null, null)
                .Object;
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(db);
            var productRepository = new EfDeletableEntityRepository<Product>(db);
            var roleManager = this.GetRoleManagerMock<ApplicationRole>();
            var billsRepository = new EfDeletableEntityRepository<Bill>(db);
            var salesRepository = new EfDeletableEntityRepository<Sale>(db);
            var internetAccountTypesRepository = new EfDeletableEntityRepository<InternetAccountType>(db);
            var internetAccountRepository = new EfDeletableEntityRepository<InternetAccount>(db);
            var failuresRepository = new EfDeletableEntityRepository<Failure>(db);
            new MapperInitializationProfile();

            var service = new SaleGoodsService(
                userManager,
                userRepository,
                productRepository,
                roleManager.Object,
                billsRepository,
                salesRepository,
                internetAccountTypesRepository,
                internetAccountRepository,
                failuresRepository);
            var newSale = new Sale
            {
                Id = 1,
                Name = "New Sale",
            };
            await salesRepository.AddAsync(newSale);
            await salesRepository.SaveChangesAsync();
            var newUser = new ApplicationUser();
            await userRepository.AddAsync(newUser);
            var saleUser = new ApplicationUser();
            await userRepository.SaveChangesAsync();

            string saleUserId = saleUser.Id;
            string selectedUserId = newUser.Id;

            var result = service.NewSaleId(saleUserId, selectedUserId);

            Assert.True(result.Result);
        }

        public Product NewProduct()
        {
            return new Product
            {
                Name = "Name of Product",
                SellPrice = 20m,
                BayPrice = 15m,
                StockQuantity = 1,
                Description = "Description",
            };
        }

        public class ProductTest : IMapFrom<Product>
        {
            public int Id { get; set; }
        }

        Mock<RoleManager<TIdentityRole>> GetRoleManagerMock<TIdentityRole>() where TIdentityRole : IdentityRole
        {
            return new Mock<RoleManager<TIdentityRole>>(
            new Mock<IRoleStore<TIdentityRole>>().Object,
            new IRoleValidator<TIdentityRole>[0],
            new Mock<ILookupNormalizer>().Object,
            new Mock<IdentityErrorDescriber>().Object,
            new Mock<ILogger<RoleManager<TIdentityRole>>>().Object);
        }
    }
}
