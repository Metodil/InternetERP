namespace InternetERP.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using InternetERP.Data.Models;

    internal class TownsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Towns.Any())
            {
                return;
            }

            await dbContext.Towns.AddAsync(new Town { Name = "Sofia" });
            await dbContext.Towns.AddAsync(new Town { Name = "Plovdiv" });

            await dbContext.SaveChangesAsync();
        }
    }
}
