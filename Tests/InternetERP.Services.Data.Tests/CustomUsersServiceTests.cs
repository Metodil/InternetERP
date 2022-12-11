namespace InternetERP.Services.Data.Tests
{
    using System;

    using InternetERP.Data;
    using InternetERP.Data.Models;
    using InternetERP.Data.Repositories;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Moq;

    public class CustomUsersServiceTests
    {
        private readonly Mock<UserManager<ApplicationUser>> userManager;
        private readonly Mock<RoleManager<ApplicationRole>> roleManager;

        public CustomUsersServiceTests()
        {
            this.userManager = new Mock<UserManager<ApplicationUser>>(new Mock<IUserStore<ApplicationUser>>().Object, null, null, null, null, null, null, null, null);
            this.roleManager = new Mock<RoleManager<ApplicationRole>>(new Mock<IRoleStore<ApplicationRole>>().Object, null, null);
        }

        public CustomUsersServiceTests Before()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var db = new ApplicationDbContext(options);
            var usersRepository = new EfDeletableEntityRepository<ApplicationUser>(db);

            // var service = new CustomUsersService(
            //    this.userManager,
            //    usersRepository,
            //    this.roleManager);
            // return service;
            return null;
        }
    }
}
