namespace InternetERP.Services.Data.Home
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using InternetERP.Data.Common.Repositories;
    using InternetERP.Data.Models;
    using InternetERP.Services.Data.Home.Contracts;
    using Microsoft.EntityFrameworkCore;

    public class HomeService : IHomeService
    {
        private readonly IDeletableEntityRepository<Product> productsRepository;

        public HomeService(
            IDeletableEntityRepository<Product> productsRepository)
        {
            this.productsRepository = productsRepository;
        }

        public async Task<ICollection<Product>> GetPromoProducts()
        {
            return await this.productsRepository
                .AllAsNoTracking()
                .Include(p => p.Images)

                // .Where(p => p.)
                .Take(3)
                .ToListAsync();
        }
    }
}
