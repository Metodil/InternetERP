namespace InternetERP.Data.Seeding
{
    using System;
    using System.Data;
    using System.Linq;
    using System.Threading.Tasks;

    using InternetERP.Common;
    using InternetERP.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public class AddUsersSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            string[] roles = new string[] { GlobalConstants.AdministratorRoleName };
            await SeedUserAsync(userManager, GlobalConstants.DataForSeeding.AdminEmail, GlobalConstants.DataForSeeding.AdminName, roles, dbContext);

            roles = new string[] { GlobalConstants.EmployeeRoleName, GlobalConstants.ManagerRoleName };
            await SeedUserAsync(userManager, GlobalConstants.DataForSeeding.EmployeeManagerEmail, GlobalConstants.DataForSeeding.EmployeeManagerName, roles, dbContext);

            roles = new string[] { GlobalConstants.EmployeeRoleName, GlobalConstants.ManagerRoleName };
            await SeedUserAsync(userManager, "EmailManger1@internetERP.com", "MangerName1", roles, dbContext);
            await SeedUserAsync(userManager, "EmailManger2@internetERP.com", "MangerName2", roles, dbContext);
            await SeedUserAsync(userManager, "EmailManger3@internetERP.com", "MangerName3", roles, dbContext);
            await SeedUserAsync(userManager, "EmailManger4@internetERP.com", "MangerName4", roles, dbContext);
            await SeedUserAsync(userManager, "EmailManger5@internetERP.com", "MangerName5", roles, dbContext);
            await SeedUserAsync(userManager, "EmailManger6@internetERP.com", "MangerName6", roles, dbContext);
            await SeedUserAsync(userManager, "EmailManger7@internetERP.com", "MangerName7", roles, dbContext);
            await SeedUserAsync(userManager, "EmailManger8@internetERP.com", "MangerName8", roles, dbContext);
            await SeedUserAsync(userManager, "EmailManger9@internetERP.com", "MangerName9", roles, dbContext);
            await SeedUserAsync(userManager, "EmailManger10@internetERP.com", "MangerName10", roles, dbContext);

            roles = new string[] { GlobalConstants.EmployeeRoleName, GlobalConstants.TechnicianRoleName };
            await SeedUserAsync(userManager, "EmailTechnician1@internetERP.com", "TechnicianName1", roles, dbContext);
            await SeedUserAsync(userManager, "EmailTechnician2@internetERP.com", "TechnicianName2", roles, dbContext);
            await SeedUserAsync(userManager, "EmailTechnician3@internetERP.com", "TechnicianName3", roles, dbContext);
            await SeedUserAsync(userManager, "EmailTechnician4@internetERP.com", "TechnicianName4", roles, dbContext);
            await SeedUserAsync(userManager, "EmailTechnician5@internetERP.com", "TechnicianName5", roles, dbContext);
            await SeedUserAsync(userManager, "EmailTechnician6@internetERP.com", "TechnicianName6", roles, dbContext);
            await SeedUserAsync(userManager, "EmailTechnician7@internetERP.com", "TechnicianName7", roles, dbContext);
            await SeedUserAsync(userManager, "EmailTechnician8@internetERP.com", "TechnicianrName8", roles, dbContext);
            await SeedUserAsync(userManager, "EmailTechnician9@internetERP.com", "TechnicianName9", roles, dbContext);
            await SeedUserAsync(userManager, "EmailTechnician10@internetERP.com", "TechnicianName10", roles, dbContext);

            roles = new string[] { GlobalConstants.EmployeeRoleName, GlobalConstants.SalesRoleName };
            await SeedUserAsync(userManager, GlobalConstants.DataForSeeding.EmployeeSalesEmail, GlobalConstants.DataForSeeding.EmployeeSalesName, roles, dbContext);

            roles = new string[] { GlobalConstants.EmployeeRoleName, GlobalConstants.SalesRoleName };
            await SeedUserAsync(userManager, "EmailSales1@internetERP.com", "SalesName1", roles, dbContext);
            await SeedUserAsync(userManager, "EmailSales2@internetERP.com", "SalesName2", roles, dbContext);
            await SeedUserAsync(userManager, "EmailSales3@internetERP.com", "SalesName3", roles, dbContext);
            await SeedUserAsync(userManager, "EmailSales4@internetERP.com", "SalesName4", roles, dbContext);
            await SeedUserAsync(userManager, "EmailSales5@internetERP.com", "SalesName5", roles, dbContext);
            await SeedUserAsync(userManager, "EmailSales6@internetERP.com", "SalesName6", roles, dbContext);
            await SeedUserAsync(userManager, "EmailSales7@internetERP.com", "SalesName7", roles, dbContext);
            await SeedUserAsync(userManager, "EmailSales8@internetERP.com", "SalesName8", roles, dbContext);
            await SeedUserAsync(userManager, "EmailSales9@internetERP.com", "SalesName9", roles, dbContext);
            await SeedUserAsync(userManager, "EmailSales10@internetERP.com", "SalesName10", roles, dbContext);

            roles = new string[] { GlobalConstants.InternetAccountRoleName };
            await SeedUserAsync(userManager, GlobalConstants.DataForSeeding.InternetAccountEmail, GlobalConstants.DataForSeeding.InternetAccountName, roles, dbContext);

            roles = new string[] { GlobalConstants.InternetAccountRoleName };
            await SeedUserAsync(userManager, "EmailIAccount1@internetERP.com", "IAccountName1", roles, dbContext);
            await SeedUserAsync(userManager, "EmailIAccount3@internetERP.com", "IAccountName3", roles, dbContext);
            await SeedUserAsync(userManager, "EmailIAccount4@internetERP.com", "IAccountName4", roles, dbContext);
            await SeedUserAsync(userManager, "EmailIAccount5@internetERP.com", "IAccountName5", roles, dbContext);
            await SeedUserAsync(userManager, "EmailIAccount6@internetERP.com", "IAccountName6", roles, dbContext);
            await SeedUserAsync(userManager, "EmailIAccount2@internetERP.com", "IAccountName2", roles, dbContext);
            await SeedUserAsync(userManager, "EmailIAccount7@internetERP.com", "IAccountName7", roles, dbContext);
            await SeedUserAsync(userManager, "EmailIAccount8@internetERP.com", "IAccountName8", roles, dbContext);
            await SeedUserAsync(userManager, "EmailIAccount9@internetERP.com", "IAccountName9", roles, dbContext);
            await SeedUserAsync(userManager, "EmailIAccount10@internetERP.com", "IAccountName10", roles, dbContext);
        }

        private static async Task SeedUserAsync(UserManager<ApplicationUser> userManager, string userEmail, string firstName, string[] roles, ApplicationDbContext dbContext)
        {
            var user = await userManager.Users.FirstOrDefaultAsync(x => x.Email == userEmail);
            if (user == null)
            {
                var result = new ApplicationUser
                {
                    Email = userEmail,
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAEAACcQAAAAEOr5MhkFf5aTLQxqzFSfcqPFcoNXXsNWFfFG7xCzzgrPNJCFAlIZHh/WV8sBBZiMHw==", // 123456  emailPass=WebProject1
                    FirstName = firstName,
                    LastName = "LastName",
                    UserName = userEmail,
                };
                var creationResult = await userManager.CreateAsync(result);

                if (creationResult.Succeeded)
                {
                    user = await userManager.Users.FirstOrDefaultAsync(x => x.Email == userEmail);
                    await UpdateRole(userManager, userEmail, roles, dbContext, user);
                }

                if (roles.Contains(GlobalConstants.InternetAccountRoleName))
                {
                    await UpdateInternetAccount(dbContext, user);
                }

                if (roles.Contains(GlobalConstants.TechnicianRoleName))
                {
                    await UpdateTechnician(dbContext, user);
                }
            }
            else
            {
                await UpdateRole(userManager, userEmail, roles, dbContext, user);
                if (roles.Contains(GlobalConstants.InternetAccountRoleName))
                {
                    await UpdateInternetAccount(dbContext, user);
                }

                if (roles.Contains(GlobalConstants.TechnicianRoleName))
                {
                    await UpdateTechnician(dbContext, user);
                }
            }

            await dbContext.SaveChangesAsync();
        }

        private static async Task UpdateTechnician(ApplicationDbContext dbContext, ApplicationUser user)
        {
            var technician = await dbContext.Employees
                .Where(u => u.EmployeeUserId == user.Id)
                .FirstOrDefaultAsync();
            if (technician.FailureTeamId == null)
            {
                Random rand = new Random();
                int toSkip = rand.Next(0, dbContext.FailureTeams.Count());
                var newTeamId = dbContext.FailureTeams
                    .Select(ft => ft.Id)
                    .OrderBy(r => Guid.NewGuid()).Skip(toSkip).Take(1)
                    .First();
                technician.FailureTeamId = newTeamId;
            }
        }

        private static async Task UpdateInternetAccount(ApplicationDbContext dbContext, ApplicationUser user)
        {
            var internetAccount = await dbContext.InternetAccounts
                .Where(a => a.InternetUserId == user.Id)
                .FirstOrDefaultAsync();
            if (internetAccount == null)
            {
                Random rnd = new Random();
                int internerAccountTypeId = rnd.Next(1, 4);
                decimal montlyPayment = await dbContext.InternetAccountTypes
                    .Where(at => at.Id == internerAccountTypeId)
                    .Select(p => p.MontlyPrice)
                    .FirstAsync();
                var newInternetAccount = new InternetAccount
                {
                    InternetUserId = user.Id,
                    InternetName = "Nic-" + user.FirstName,
                    AccountTypeId = internerAccountTypeId,
                    МonthlyPayment = montlyPayment,
                    ExparedDate = DateTime.Now,
                };
                await dbContext.InternetAccounts.AddAsync(newInternetAccount);
            }

            UpdateAddess(user);
        }

        private static async Task UpdateRole(UserManager<ApplicationUser> userManager, string userEmail, string[] roles, ApplicationDbContext dbContext, ApplicationUser user)
        {
            foreach (var roleName in roles)
            {
                await SeedRoleAsync(userManager, roleName, userEmail, dbContext);
                if (roleName == GlobalConstants.EmployeeRoleName)
                {
                    await AddToEmploeesAsync(user, dbContext);
                }
            }
        }

        private static async Task SeedRoleAsync(UserManager<ApplicationUser> userManager, string roleName, string userEmail, ApplicationDbContext dbContext)
        {
            var user = await userManager.Users.FirstAsync(x => x.Email == userEmail);
            var isInRole = await userManager.IsInRoleAsync(user, roleName);
            if (!isInRole)
            {
                await userManager.AddToRoleAsync(user, roleName);
            }
        }

        private static async Task AddToEmploeesAsync(ApplicationUser user, ApplicationDbContext dbContext)
        {
            var employee = dbContext
                .Employees
                .Where(e => e.EmployeeUserId == user.Id)
                .FirstOrDefault();
            if (employee == null)
            {
                var newEmployee = new Employee
                {
                    EmployeeUserId = user.Id,
                    HireDate = DateTime.Now,
                    Salary = 5000,
                };
                await dbContext
                   .Employees.AddAsync(newEmployee);
            }
        }

        private static void UpdateAddess(ApplicationUser user)
        {
            if (user.TownId == null)
            {
                user.TownId = 1;
                user.Street = GetAddresSeeder();
            }
        }

        private static string GetAddresSeeder()
        {
            var addresses = new string[]
            {
            "ul. YERUSALIM, 1 BL.24",
            "ul. YANKO SAKAZOV, 56",
            "ul. GRAF IGNATIEV, 7",
            "ul. TOTLEBEN, 8",
            "ul. G.M.DIMITROV 94",
            "ul. P. YAVOROV, 2",
            "ul. TSARIGRADSKO SHOSE 117",
            "ul. SERDIKA, 4",
            "ul. TSAR BORIS III, 1",
            "ul. HAYDUSHKA POLYANA, 8",
            };
            Random random = new Random();
            int index = random.Next(0, addresses.Length);
            return addresses[index];
        }
    }
}
