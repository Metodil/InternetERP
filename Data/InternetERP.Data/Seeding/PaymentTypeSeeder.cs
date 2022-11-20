namespace InternetERP.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using InternetERP.Data;
    using InternetERP.Data.Models;

    public class PaymentTypeSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.PaymentTypes.Any())
            {
                return;
            }

            await dbContext.PaymentTypes.AddAsync(new PaymentType { Type = "Cash", Description = "Cash is the original and oldest payment method: the physical coins and notes you’ll find in your wallet, an ATM or at the bank." });
            await dbContext.PaymentTypes.AddAsync(new PaymentType { Type = "Debit cards", Description = "Most popular payment method for both online and offline purchases." });
            await dbContext.PaymentTypes.AddAsync(new PaymentType { Type = "Credit cards", Description = "Credit cards have a pre-approved limit and are later paid back by the customer (with interest) over time." });
            await dbContext.PaymentTypes.AddAsync(new PaymentType { Type = "Mobile payments.", Description = "Mobile payments refer to payment methods that use your phone. These could be remote, in-person, or contactless payments." });
            await dbContext.PaymentTypes.AddAsync(new PaymentType { Type = "Bank transfers", Description = "Bank transfers are popular amongst B2B transactions and larger payments such as wholesale orders." });

            await dbContext.SaveChangesAsync();
        }
    }
}
