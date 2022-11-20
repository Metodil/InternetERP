namespace InternetERP.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using InternetERP.Data.Models;

    public class StatusFailureSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.StatusFailures.Any())
            {
                return;
            }

            await dbContext.StatusFailures.AddAsync(new StatusFailure { Name = "Register", Description = "On first registration" });
            await dbContext.StatusFailures.AddAsync(new StatusFailure { Name = "In propgress", Description = "Taked from Failure Team" });
            await dbContext.StatusFailures.AddAsync(new StatusFailure { Name = "Finished", Description = "Failure is repaired" });

            await dbContext.SaveChangesAsync();
        }
    }
}
