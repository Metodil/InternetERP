namespace InternetERP.Services.Data.Tests
{
    using System;
    using System.Threading.Tasks;

    using InternetERP.Data;
    using InternetERP.Data.Models;
    using InternetERP.Data.Repositories;
    using InternetERP.Services.Data.Home;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class HomeServiceTest
    {
        [Fact]
        public async Task GetPromoProductsMustReturn3Product()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                           .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var db = new ApplicationDbContext(options);
            var productsRepository = new EfDeletableEntityRepository<Product>(db);

            var service = new HomeService(productsRepository);

            await productsRepository.AddAsync(this.NewProduct());
            await productsRepository.AddAsync(this.NewProduct());
            await productsRepository.AddAsync(this.NewProduct());
            await productsRepository.SaveChangesAsync();

            var result = service.GetPromoProducts();

            Assert.NotNull(result);
            Assert.Equal(3, result.Result.Count);
        }

        public Product NewProduct()
        {
            return new Product
            {
                Name = "TL-WR841N 300Mbps Wireless N Router, Atheros",
                SellPrice = 37.00m,
                BayPrice = 32.00m,
                StockQuantity = 5,
                Description = "2T2R, 2.4GHz, 802.11n/g/b, Built-in 4-port Switch, with 2 fixed antennas",
                PromotionId = 1,
            };
        }
    }
}
