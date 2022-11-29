namespace InternetERP.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using InternetERP.Data.Models;
    using Microsoft.EntityFrameworkCore;

    public class FailuresSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            //if (!dbContext.Failures.Any())
            //{
            for (int i = 0; i < 20; i++)
            {
                await AddFailure(dbContext);
            }

            await dbContext.SaveChangesAsync();
            //}
        }

        private static async Task AddFailure(ApplicationDbContext dbContext)
        {
            Random rand = new Random();
            int toSkip = rand.Next(0, dbContext.InternetAccounts.Count());
            var internetUser = dbContext.InternetAccounts
                .OrderBy(r => Guid.NewGuid()).Skip(toSkip).Take(1)
                .First();
            rand = new Random();
            toSkip = rand.Next(0, dbContext.Users
                .Where(u => u.UserName.Contains("Sale"))
                .Count());
            var createdUser = dbContext.Users
                .Where(u => u.UserName.Contains("Sale"))
                .OrderBy(r => Guid.NewGuid()).Skip(toSkip).Take(1)
                .First();
            rand = new Random();
            toSkip = rand.Next(0, dbContext.StatusFailures.Count());
            var statusFailure = dbContext.StatusFailures
                .OrderBy(r => Guid.NewGuid()).Skip(toSkip).Take(1)
                .First();
            var newFailure = new Failure
            {
                ShortDescription = GetRandFailureDesc(),
                Note = "It is second time!",
                AccountId = internetUser.Id,
                CreateUserId = createdUser.Id,
                StatusFailureId = statusFailure.Id,
            };
            await dbContext.Failures.AddAsync(newFailure);
            await dbContext.SaveChangesAsync();

            newFailure.CreatedOn = GetRandDate();
            await dbContext.SaveChangesAsync();

            if (statusFailure.Name != "Registered")
            {
                rand = new Random();
                toSkip = rand.Next(0, dbContext.Users
                    .Where(u => u.UserName.Contains("Technician"))
                    .Count());
                var user = await dbContext.Users
                   .Where(u => u.UserName.Contains("Technician"))
                   .OrderBy(r => Guid.NewGuid()).Skip(toSkip).Take(1)
                   .FirstAsync();
                var technicianUser = await dbContext.Employees
                    .Include(e => e.FailureTeams)
                    .Where(a => a.EmployeeUserId == user.Id)
                    .FirstAsync();
                var newFailurePhase = new FailurePhase
                {
                    FailureId = newFailure.Id,
                    UserId = technicianUser.EmployeeUserId,
                    FailureTeamId = (int)technicianUser.FailureTeamId,
                    StatusFailureId = statusFailure.Id,
                };
                newFailurePhase.Note = "Started on: " + DateTime.Now.ToString("HH:mm");
                await dbContext.FailurePhases.AddAsync(newFailurePhase);
                await dbContext.SaveChangesAsync();

                if (statusFailure.Name == "Finished")
                {
                    var newFailurePhaseFinished = new FailurePhase
                    {
                        FailureId = newFailure.Id,
                        UserId = technicianUser.EmployeeUserId,
                        FailureTeamId = (int)technicianUser.FailureTeamId,
                        StatusFailureId = statusFailure.Id,
                    };
                    newFailurePhaseFinished.Note = "Finished on: " + DateTime.Now.ToString("HH:mm");
                    await dbContext.FailurePhases.AddAsync(newFailurePhaseFinished);
                    await dbContext.SaveChangesAsync();
                }
            }
        }

        private static string GetRandFailureDesc()
        {
            var failureNames = new string[]
            {
                "Range is too low.",
                "Speed is too slow.",
                "No ineternet connection.",
            };
            Random rnd = new Random();
            int toSkip = rnd.Next(0, failureNames.Count());
            var failureName = failureNames
                .OrderBy(r => Guid.NewGuid())
                .Skip(toSkip)
                .Take(1)
                .First();
            return failureName;
        }

        private static DateTime GetRandDate()
        {
            DateTime startDate = DateTime.Now;
            DateTime.TryParse("2022-01-01", out startDate);
            DateTime endDate = DateTime.Now;
            DateTime.TryParse("2022-11-20", out endDate);

            var randomTest = new Random();
            TimeSpan timeSpan = endDate - startDate;
            TimeSpan newSpan = new TimeSpan(0, randomTest.Next(0, (int)timeSpan.TotalMinutes), 0);
            DateTime newDate = startDate + newSpan;

            return newDate;
        }
    }
}
