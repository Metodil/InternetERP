namespace InternetERP.Data.Seeding.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using InternetERP.Data.Models;

    public class FailureTeamSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.FailureTeams.Any())
            {
                return;
            }

            await dbContext.FailureTeams.AddAsync(new FailureTeam { Name = "Blue", Description = "First Team" });
            await dbContext.FailureTeams.AddAsync(new FailureTeam { Name = "Red", Description = "Second Team" });
            await dbContext.FailureTeams.AddAsync(new FailureTeam { Name = "Yellow", Description = "Third Team" });

            await dbContext.SaveChangesAsync();
        }
    }
}
