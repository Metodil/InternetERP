namespace InternetERP.Services.Data.Tests
{
    using System;
    using System.Threading.Tasks;

    using InternetERP.Data;
    using InternetERP.Data.Models;
    using InternetERP.Data.Repositories;
    using InternetERP.Services.Data.Employee;
    using InternetERP.Web.ViewModels.Employee.Failure;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using Moq;
    using Newtonsoft.Json;
    using Xunit;

    public class FailuresServiceTests
    {
        [Fact]
        public async Task GetAllAccountsReturnProperlyCount()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                      .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var db = new ApplicationDbContext(options);
            var internetAccountsRepository = new EfDeletableEntityRepository<InternetAccount>(db);
            var failureRepository = new EfDeletableEntityRepository<Failure>(db);
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(db);
            var logger = new Mock<ILogger<FailuresService>>();
            new MapperInitializationProfile();

            var service = new FailuresService(
                internetAccountsRepository,
                failureRepository,
                userRepository,
                logger.Object);
            await internetAccountsRepository.AddAsync(this.NewInternetAccount());
            await internetAccountsRepository.AddAsync(this.NewInternetAccount());
            await internetAccountsRepository.AddAsync(this.NewInternetAccount());
            await internetAccountsRepository.SaveChangesAsync();

            var result = await service.GetAllAccounts();

            Assert.Equal(3, result.Count);
        }

        [Fact]
        public async Task GetAllInternetAcountsListReturnProperlyResult()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                      .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var db = new ApplicationDbContext(options);
            var internetAccountsRepository = new EfDeletableEntityRepository<InternetAccount>(db);
            var failureRepository = new EfDeletableEntityRepository<Failure>(db);
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(db);
            var logger = new Mock<ILogger<FailuresService>>();
            new MapperInitializationProfile();

            var service = new FailuresService(
                internetAccountsRepository,
                failureRepository,
                userRepository,
                logger.Object);
            await internetAccountsRepository.AddAsync(this.NewInternetAccount());
            await internetAccountsRepository.AddAsync(this.NewInternetAccount());
            await internetAccountsRepository.AddAsync(this.NewInternetAccount());
            await internetAccountsRepository.SaveChangesAsync();

            var result = await service.GetAllInternetAcountsList();

            Assert.NotNull(result);
            var firstSelectListItem = new SelectListItem
            {
                Value = string.Empty,
                Text = "Select an account",
            };
            var objExpect = JsonConvert.SerializeObject(firstSelectListItem);
            var objResult = JsonConvert.SerializeObject(result[0]);
            Assert.Equal(objExpect, objResult);

            var secondSelectListItem = new SelectListItem
            {
                Value = "1",
                Text = " ",
            };
            objExpect = JsonConvert.SerializeObject(secondSelectListItem);
            objResult = JsonConvert.SerializeObject(result[1]);
            Assert.Equal(objExpect, objResult);
        }

        [Fact]
        public async Task CreateFailureProperlyCreate()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                      .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var db = new ApplicationDbContext(options);
            var internetAccountsRepository = new EfDeletableEntityRepository<InternetAccount>(db);
            var failureRepository = new EfDeletableEntityRepository<Failure>(db);
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(db);
            var logger = new Mock<ILogger<FailuresService>>();
            new MapperInitializationProfile();

            var service = new FailuresService(
                internetAccountsRepository,
                failureRepository,
                userRepository,
                logger.Object);

            var createUserId = Guid.NewGuid().ToString();
            var shortDescription = "ShortDescription";
            var note = "Note";
            var accountId = 1;
            var newFailureInputModel = new FailureInputModel
            {
                ShortDescription = shortDescription,
                Note = note,
                SelectedAccountId = accountId,
            };

            var result = await service.CreateFailure(createUserId, newFailureInputModel);

            Assert.True(result);
            var resultFailure = await failureRepository
                .AllAsNoTracking()
                .FirstOrDefaultAsync();
            Assert.NotNull(resultFailure);
            Assert.Equal(shortDescription, resultFailure.ShortDescription);
            Assert.Equal(note, resultFailure.Note);
        }

        [Fact]
        public async Task GetInternetUserByIdProperlyReturnInternetUser()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                      .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var db = new ApplicationDbContext(options);
            var internetAccountsRepository = new EfDeletableEntityRepository<InternetAccount>(db);
            var failureRepository = new EfDeletableEntityRepository<Failure>(db);
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(db);
            var logger = new Mock<ILogger<FailuresService>>();
            new MapperInitializationProfile();

            var service = new FailuresService(
                internetAccountsRepository,
                failureRepository,
                userRepository,
                logger.Object);

            var internetUserId = Guid.NewGuid().ToString();
            var newUser = new ApplicationUser
            {
                Id = internetUserId,
            };
            await userRepository.AddAsync(newUser);
            await userRepository.SaveChangesAsync();
            var accountId = 1;
            var newInternetAccount = new InternetAccount
            {
                Id = accountId,
                InternetUserId = internetUserId,
                InternetName = "Nick name",
            };
            await internetAccountsRepository
                .AddAsync(newInternetAccount);
            await internetAccountsRepository.SaveChangesAsync();

            var result = await service.GetInternetUserById(accountId);

            Assert.NotNull(result);
            Assert.Equal(internetUserId, result.Id);
        }

        public InternetAccount NewInternetAccount()
        {
            var internetUserId = Guid.NewGuid().ToString();
            return new InternetAccount
            {
                InternetUserId = internetUserId,
                AccountTypeId = 1,
                ExparedDate = DateTime.UtcNow,
                InternetName = "Nick name",
                МonthlyPayment = 20m,
            };
        }
    }
}
