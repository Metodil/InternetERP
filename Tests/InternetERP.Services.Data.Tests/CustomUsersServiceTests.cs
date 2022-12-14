namespace InternetERP.Services.Data.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using InternetERP.Data;
    using InternetERP.Data.Models;
    using InternetERP.Data.Repositories;
    using InternetERP.Services.Data.Administration;
    using InternetERP.Services.Mapping;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
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

        [Fact]
        public async Task SetFirstNameAsyncProperlySetName()
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
                FirstName = "firstName",
            };
            await usersRepository.AddAsync(newUser);
            await usersRepository.SaveChangesAsync();
            var firstNameForUpdate = "firstName updated";

            var result = await service.SetFirstNameAsync(userId, firstNameForUpdate);

            Assert.Equal(1, result);
            var resultUser = await usersRepository
                .AllAsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == userId);

            Assert.NotNull(resultUser);
            Assert.Equal(userId, resultUser.Id);
            Assert.Equal(firstNameForUpdate, resultUser.FirstName);
        }

        [Fact]
        public async Task SetLastNameAsyncAsyncProperlySetName()
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
                LastName = "lastName",
            };
            await usersRepository.AddAsync(newUser);
            await usersRepository.SaveChangesAsync();
            var lastNameForUpdate = "lastName updated";

            var result = await service.SetLastNameAsync(userId, lastNameForUpdate);

            Assert.Equal(1, result);
            var resultUser = await usersRepository
                .AllAsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == userId);

            Assert.NotNull(resultUser);
            Assert.Equal(userId, resultUser.Id);
            Assert.Equal(lastNameForUpdate, resultUser.LastName);
        }

        [Fact]
        public async Task SetDistrictAsyncAsyncProperlySetDistrict()
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
                District = "district",
            };
            await usersRepository.AddAsync(newUser);
            await usersRepository.SaveChangesAsync();
            var districteForUpdate = "district updated";

            var result = await service.SetDistrictAsync(userId, districteForUpdate);

            Assert.Equal(1, result);
            var resultUser = await usersRepository
                .AllAsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == userId);

            Assert.NotNull(resultUser);
            Assert.Equal(userId, resultUser.Id);
            Assert.Equal(districteForUpdate, resultUser.District);
        }

        [Fact]
        public async Task SetStreetAsyncAsyncProperlySetStreet()
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
                Street = "street",
            };
            await usersRepository.AddAsync(newUser);
            await usersRepository.SaveChangesAsync();
            var streetForUpdate = "street updated";

            var result = await service.SetStreetAsync(userId, streetForUpdate);

            Assert.Equal(1, result);
            var resultUser = await usersRepository
                .AllAsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == userId);

            Assert.NotNull(resultUser);
            Assert.Equal(userId, resultUser.Id);
            Assert.Equal(streetForUpdate, resultUser.Street);
        }

        [Fact]
        public async Task SetTownIdAsyncProperlySetTownId()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                      .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var db = new ApplicationDbContext(options);
            var usersRepository = new EfDeletableEntityRepository<ApplicationUser>(db);
            var townsRepository = new EfDeletableEntityRepository<Town>(db);
            var userManager = new Mock<UserManager<ApplicationUser>>(new Mock<IUserStore<ApplicationUser>>().Object, null, null, null, null, null, null, null, null);
            var roleManager = new Mock<RoleManager<ApplicationRole>>(new Mock<IRoleStore<ApplicationRole>>().Object, null, null, null, null);

            new MapperInitializationProfile();

            var service = new CustomUsersService(
                userManager.Object,
                usersRepository,
                roleManager.Object);
            var newTown = new Town
            {
                Id = 1,
                Name = "Name 1",
            };
            await townsRepository.AddAsync(newTown);
            newTown = new Town
            {
                Id = 2,
                Name = "Name 2",
            };
            await townsRepository.AddAsync(newTown);
            await townsRepository.SaveChangesAsync();
            var userId = Guid.NewGuid().ToString();
            var newUser = new ApplicationUser
            {
                Id = userId,
                TownId = 1,
            };
            await usersRepository.AddAsync(newUser);
            await usersRepository.SaveChangesAsync();
            var townIdForUpdate = 2;

            var result = await service.SetTownIdAsync(userId, townIdForUpdate);

            Assert.Equal(1, result);
            var resultUser = await usersRepository
                .AllAsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == userId);

            Assert.NotNull(resultUser);
            Assert.Equal(userId, resultUser.Id);
            Assert.Equal(townIdForUpdate, resultUser.TownId);
        }

        [Fact]
        public async Task SetNoteAsyncAsyncProperlySetNote()
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
                Note = "note",
            };
            await usersRepository.AddAsync(newUser);
            await usersRepository.SaveChangesAsync();
            var noteForUpdate = "note updated";

            var result = await service.SetNoteAsync(userId, noteForUpdate);

            Assert.Equal(1, result);
            var resultUser = await usersRepository
                .AllAsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == userId);

            Assert.NotNull(resultUser);
            Assert.Equal(userId, resultUser.Id);
            Assert.Equal(noteForUpdate, resultUser.Note);
        }

        [Fact]
        public async Task GetAllEmailsAsyncProperlyRetrunMailCount()
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
                Email = "Email first",
            };
            await usersRepository.AddAsync(newUser);
            userId = Guid.NewGuid().ToString();
            newUser = new ApplicationUser
            {
                Id = userId,
                Email = "Email second",
            };
            await usersRepository.AddAsync(newUser);
            await usersRepository.SaveChangesAsync();

            var result = await service.GetAllEmailsAsync();

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task CheckForEmailsAsyncProperlyRetrunMail()
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
                Email = "Email first",
            };
            await usersRepository.AddAsync(newUser);
            userId = Guid.NewGuid().ToString();
            var emailForSearch = "Email second";
            newUser = new ApplicationUser
            {
                Id = userId,
                Email = emailForSearch,
            };
            await usersRepository.AddAsync(newUser);
            await usersRepository.SaveChangesAsync();

            var result = service.CheckForEmailsAsync(emailForSearch);
            Assert.True(result);
        }

        [Fact]
        public async Task CheckForEmailsAsyncProperlyFalseWhenMailIsNotFound()
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
                Email = "Email first",
            };
            await usersRepository.AddAsync(newUser);
            userId = Guid.NewGuid().ToString();
            newUser = new ApplicationUser
            {
                Id = userId,
                Email = "Email second",
            };
            await usersRepository.AddAsync(newUser);
            await usersRepository.SaveChangesAsync();

            var emailNotFound = "Email not found";
            var result = service.CheckForEmailsAsync(emailNotFound);
            Assert.False(result);
        }

        [Fact]
        public async Task GetAllUsersAsyncProperlyRetrunUsersCount()
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
                Email = "Email first",
            };
            await usersRepository.AddAsync(newUser);
            userId = Guid.NewGuid().ToString();
            newUser = new ApplicationUser
            {
                Id = userId,
                Email = "Email second",
            };
            await usersRepository.AddAsync(newUser);
            await usersRepository.SaveChangesAsync();

            var result = await service.GetAllUsersAsync<UsersTest>();

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GGetAllUsersPagingAsyncProperlyRetrunUsers()
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
                Email = "Email first",
            };
            await usersRepository.AddAsync(newUser);
            userId = Guid.NewGuid().ToString();
            newUser = new ApplicationUser
            {
                Id = userId,
                Email = "Email second",
            };
            await usersRepository.AddAsync(newUser);
            userId = Guid.NewGuid().ToString();
            newUser = new ApplicationUser
            {
                Id = userId,
                Email = "Email 3",
            };
            await usersRepository.AddAsync(newUser);
            userId = Guid.NewGuid().ToString();
            newUser = new ApplicationUser
            {
                Id = userId,
                Email = "Email 4",
            };
            await usersRepository.AddAsync(newUser);
            userId = Guid.NewGuid().ToString();
            newUser = new ApplicationUser
            {
                Id = userId,
                Email = "Email 5",
            };
            await usersRepository.AddAsync(newUser);
            userId = Guid.NewGuid().ToString();
            newUser = new ApplicationUser
            {
                Id = userId,
                Email = "Email 6",
            };
            await usersRepository.AddAsync(newUser);
            await usersRepository.SaveChangesAsync();

            var page = 1;
            var itemPerPage = 5;
            var result = await service.GetAllUsersPagingAsync<UsersTest>(page, itemPerPage);

            Assert.Equal(5, result.Count());
        }

        [Fact]
        public async Task GetDistrictAsyncProperlyRetrunDistrict()
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
            var districtForSearch = "district";
            var newUser = new ApplicationUser
            {
                Id = userId,
                District = districtForSearch,
            };
            await usersRepository.AddAsync(newUser);
            await usersRepository.SaveChangesAsync();

            var result = await service.GetDistrictAsync(userId);

            Assert.Equal(districtForSearch, result);
        }

        [Fact]
        public async Task GetFirstNameAsyncProperlyRetrunFirstName()
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
            var firstNameForSearch = "first name";
            var newUser = new ApplicationUser
            {
                Id = userId,
                FirstName = firstNameForSearch,
            };
            await usersRepository.AddAsync(newUser);
            await usersRepository.SaveChangesAsync();

            var result = await service.GetFirstNameAsync(userId);

            Assert.Equal(firstNameForSearch, result);
        }

        [Fact]
        public async Task GetLasttNameAsyncProperlyRetrunLastName()
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
            var lastNameForSearch = "last name";
            var newUser = new ApplicationUser
            {
                Id = userId,
                LastName = lastNameForSearch,
            };
            await usersRepository.AddAsync(newUser);
            await usersRepository.SaveChangesAsync();

            var result = await service.GetLastNameAsync(userId);

            Assert.Equal(lastNameForSearch, result);
        }

        [Fact]
        public async Task GetNoteAsyncProperlyRetrunNote()
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
            var noteForSearch = "note";
            var newUser = new ApplicationUser
            {
                Id = userId,
                Note = noteForSearch,
            };
            await usersRepository.AddAsync(newUser);
            await usersRepository.SaveChangesAsync();

            var result = await service.GetNoteAsync(userId);

            Assert.Equal(noteForSearch, result);
        }

        [Fact]
        public async Task GetStreetAsyncProperlyRetrunStreet()
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
            var streetForSearch = "street";
            var newUser = new ApplicationUser
            {
                Id = userId,
                Street = streetForSearch,
            };
            await usersRepository.AddAsync(newUser);
            await usersRepository.SaveChangesAsync();

            var result = await service.GetStreetAsync(userId);

            Assert.Equal(streetForSearch, result);
        }

        [Fact]
        public async Task GetTownIdAsyncProperlyRetrunTownId()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                      .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var db = new ApplicationDbContext(options);
            var usersRepository = new EfDeletableEntityRepository<ApplicationUser>(db);
            var townsRepository = new EfDeletableEntityRepository<Town>(db);
            var userManager = new Mock<UserManager<ApplicationUser>>(new Mock<IUserStore<ApplicationUser>>().Object, null, null, null, null, null, null, null, null);
            var roleManager = new Mock<RoleManager<ApplicationRole>>(new Mock<IRoleStore<ApplicationRole>>().Object, null, null, null, null);

            new MapperInitializationProfile();

            var service = new CustomUsersService(
                userManager.Object,
                usersRepository,
                roleManager.Object);
            var newTown = new Town
            {
                Id = 1,
                Name = "name",
            };
            await townsRepository.AddAsync(newTown);
            await townsRepository.SaveChangesAsync();
            var userId = Guid.NewGuid().ToString();
            var townIdForSearch = 1;
            var newUser = new ApplicationUser
            {
                Id = userId,
                TownId = townIdForSearch,
            };
            await usersRepository.AddAsync(newUser);
            await usersRepository.SaveChangesAsync();

            var result = await service.GetTownIdAsync(userId);

            Assert.Equal(townIdForSearch, result);
        }

        [Fact]
        public async Task GetUserByIdAsyncProperlyRetrunUser()
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
            };
            await usersRepository.AddAsync(newUser);
            await usersRepository.SaveChangesAsync();

            var result = await service.GetUserByIdAsync(userId);

            Assert.Equal(userId, result.Id);
        }

        [Fact]
        public async Task GetUserByIdAsyncWithTProperlyRetrunUser()
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
            };
            await usersRepository.AddAsync(newUser);
            await usersRepository.SaveChangesAsync();

            var result = await service.GetUserByIdAsync<UsersTest>(userId);

            Assert.Equal(userId, result.Id);
        }

        [Fact]
        public async Task CountAsyncProperlyRetrunCount()
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
            };
            await usersRepository.AddAsync(newUser);
            await usersRepository.SaveChangesAsync();

            var result = await service.CountAsync();

            Assert.Equal(1, result);
        }

        [Fact]
        public async Task GetFilteredUsersPagingAsyncProperlyRetrunUsers()
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
                FirstName = "Name first",
            };
            await usersRepository.AddAsync(newUser);
            userId = Guid.NewGuid().ToString();
            var nameForSearch = "Name second";
            newUser = new ApplicationUser
            {
                Id = userId,
                FirstName = nameForSearch,
            };
            await usersRepository.AddAsync(newUser);
            userId = Guid.NewGuid().ToString();
            newUser = new ApplicationUser
            {
                Id = userId,
                Email = "Email 3",
            };
            await usersRepository.AddAsync(newUser);
            userId = Guid.NewGuid().ToString();
            newUser = new ApplicationUser
            {
                Id = userId,
                Email = "Email 4",
            };
            await usersRepository.AddAsync(newUser);
            userId = Guid.NewGuid().ToString();
            newUser = new ApplicationUser
            {
                Id = userId,
                Email = "Email 5",
            };
            await usersRepository.AddAsync(newUser);
            userId = Guid.NewGuid().ToString();
            newUser = new ApplicationUser
            {
                Id = userId,
                Email = "Email 6",
            };
            await usersRepository.AddAsync(newUser);
            await usersRepository.SaveChangesAsync();

            var page = 1;
            var itemPerPage = 5;
            var result = await service.GetFilteredUsersPagingAsync<UsersTest>(page, itemPerPage, nameForSearch);

            Assert.Single(result);
        }

        public class UsersTest : IMapFrom<ApplicationUser>
        {
            public string Id { get; set; }
        }
    }
}
