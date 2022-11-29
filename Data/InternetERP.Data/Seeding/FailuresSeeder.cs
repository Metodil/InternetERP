namespace InternetERP.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using InternetERP.Data.Models;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;

    public class FailuresSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (!dbContext.Failures.Any())
            {
                for (int i = 0; i < 20; i++)
                {
                    await AddFailure(dbContext);
                }

                await dbContext.SaveChangesAsync();
            }
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

            var registerDate = GetRandDate();
            newFailure.CreatedOn = registerDate;
            await dbContext.SaveChangesAsync();

            if (statusFailure.Name == "Registered")
            {
                await NewFailurePhase(dbContext, newFailure.Id, 1, "Started on: ", registerDate.AddDays(2));
            }

            if (statusFailure.Name == "In propgress")
            {
                await NewFailurePhase(dbContext, newFailure.Id, 1, "Started on: ", registerDate.AddDays(2));
                await NewFailurePhase(dbContext, newFailure.Id, 2, "Visited on: ", registerDate.AddDays(3));
            }

            if (statusFailure.Name == "Finished")
            {
                await NewFailurePhase(dbContext, newFailure.Id, 1, "Started on: ", registerDate.AddDays(2));
                await NewFailurePhase(dbContext, newFailure.Id, 2, "Visited on: ", registerDate.AddDays(3));
                await NewFailurePhase(dbContext, newFailure.Id, 3, "Finished on: ", registerDate.AddDays(5));
                newFailure.FinishDate = registerDate.AddDays(5);
                await dbContext.SaveChangesAsync();
            }
        }

        private static async Task NewFailurePhase(
            ApplicationDbContext dbContext,
            int failureId,
            int statusFailureId,
            string noteText,
            DateTime createDate)
        {
            var rand = new Random();
            int toSkip = rand.Next(0, dbContext.Users
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
                FailureId = failureId,
                UserId = technicianUser.EmployeeUserId,
                FailureTeamId = (int)technicianUser.FailureTeamId,
                StatusFailureId = statusFailureId,
            };
            newFailurePhase.Note = noteText + DateTime.Now.ToString("HH:mm");
            await dbContext.FailurePhases.AddAsync(newFailurePhase);
            await dbContext.SaveChangesAsync();
            newFailurePhase.CreatedOn = createDate;
            await dbContext.SaveChangesAsync();
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
