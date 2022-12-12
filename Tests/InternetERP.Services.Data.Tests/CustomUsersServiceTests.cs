namespace InternetERP.Services.Data.Tests
{
    using System;
    using System.Threading.Tasks;
    using InternetERP.Data;
    using InternetERP.Data.Common.Repositories;
    using InternetERP.Data.Models;
    using InternetERP.Data.Repositories;
    using InternetERP.Services.Data.Administration;
    using InternetERP.Services.Data.Contracts;
    using InternetERP.Services.Data.Employee;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using Moq;
    using Xunit;

    public class CustomUsersServiceTests
    {
        [Fact]
        public async Task SetEmailAsyncProperlySetEmail()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                      .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var db = new ApplicationDbContext(options);
            var usersRepository = new EfDeletableEntityRepository<ApplicationUser>(db);
            var userManager = new Mock<UserManager<ApplicationUser>>(new Mock<IUserStore<ApplicationUser>>().Object, null, null, null, null, null, null, null, null);
            var roleManager = new Mock<RoleManager<ApplicationRole>>(new Mock<IRoleStore<ApplicationRole>>().Object, null, null, null, null);

            new MapperInitializationProfile();

            var service = new CustomUsersService(
                userManager.Object,
                usersRepository,
                roleManager.Object);
            var userId = Guid.NewGuid().ToString();
            var newUser = new ApplicationUser
            {
                Id = userId,
                Email = "email@internetert.com",
            };
            await usersRepository.AddAsync(newUser);
            await usersRepository.SaveChangesAsync();
            var emailForUpdate = "email_updated@internetert.com";

            var result = await service.SetEmailAsync(userId, emailForUpdate);

            Assert.Equal(userId, result.Id);
            Assert.Equal(emailForUpdate, result.Email);
        }
    }
}
