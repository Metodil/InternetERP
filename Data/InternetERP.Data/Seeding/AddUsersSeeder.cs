namespace InternetERP.Data.Seeding
{
    using System;
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
            await SeedUserAsync(userManager, GlobalConstants.DataForSeeding.AdminEmail, GlobalConstants.DataForSeeding.AdminName,  roles);

            roles = new string[] { GlobalConstants.EmployeeRoleName, GlobalConstants.ManagerRoleName };
            await SeedUserAsync(userManager, GlobalConstants.DataForSeeding.EmployeeManagerEmail, GlobalConstants.DataForSeeding.EmployeeManagerName, roles);

            roles = new string[] { GlobalConstants.EmployeeRoleName, GlobalConstants.SalesRoleName };
            await SeedUserAsync(userManager, GlobalConstants.DataForSeeding.EmployeeSalesEmail, GlobalConstants.DataForSeeding.EmployeeSalesName, roles);

            roles = new string[] { GlobalConstants.InternetAccountRoleName };
            await SeedUserAsync(userManager, GlobalConstants.DataForSeeding.InternetAccountEmail, GlobalConstants.DataForSeeding.InternetAccountName, roles);
        }

        private static async Task SeedUserAsync(UserManager<ApplicationUser> userManager, string userEmail, string firstName, string[] roles)
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
                    foreach (var roleName in roles)
                    {
                        await SeedRoleAsync(userManager, roleName, userEmail);
                    }
                }
            }
        }

        private static async Task SeedRoleAsync(UserManager<ApplicationUser> userManager, string roleName, string userEmail)
        {
            var user = await userManager.Users.FirstAsync(x => x.Email == userEmail);
            var isInRole = await userManager.IsInRoleAsync(user, roleName);
            if (!isInRole)
            {
                await userManager.AddToRoleAsync(user, roleName);
            }
        }
    }
}
