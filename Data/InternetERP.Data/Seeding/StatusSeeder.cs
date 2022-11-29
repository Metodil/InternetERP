namespace InternetERP.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using InternetERP.Data.Models;

    public class StatusSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (!dbContext.StatusFailures.Any())
            {
                await dbContext.StatusFailures.AddAsync(new StatusFailure { Name = "Registered", Description = "On first registration" });
                await dbContext.StatusFailures.AddAsync(new StatusFailure { Name = "In propgress", Description = "Taked from Failure Team" });
                await dbContext.StatusFailures.AddAsync(new StatusFailure { Name = "Finished", Description = "Failure is repaired" });
            }
            else if (!dbContext.StatusBills.Any())
            {
                await dbContext.StatusBills.AddAsync(new StatusBill { Name = "New Sale", Description = "On create new Sale" });
                await dbContext.StatusBills.AddAsync(new StatusBill { Name = "Add products", Description = "Adding products" });
                await dbContext.StatusBills.AddAsync(new StatusBill { Name = "Add services", Description = "Adding services" });
                await dbContext.StatusBills.AddAsync(new StatusBill { Name = "Checkout", Description = "Checkout payment" });
                await dbContext.StatusBills.AddAsync(new StatusBill { Name = "Make Invoice", Description = "If needed create invoice" });
                await dbContext.StatusBills.AddAsync(new StatusBill { Name = "Finished", Description = "Sale is finished" });
            }
            else
            {
                return;
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
