namespace InternetERP.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using InternetERP.Data;
    using InternetERP.Data.Models;
    using InternetERP.Data.Repositories;
    using InternetERP.Services.Data.Administration;
    using InternetERP.Services.Data.Employee;
    using InternetERP.Services.Mapping;
    using InternetERP.Web.ViewModels.Administration.Towns;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;
    using Xunit;
    using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

    public class TownsServiceTests
    {
        [Fact]
        public async Task GetAllTownsAsKetValuePairsProperlyRetrunData()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                      .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var db = new ApplicationDbContext(options);
            var townsRepository = new EfDeletableEntityRepository<Town>(db);

            var service = new TownsService(
                townsRepository);
            var townId = 1;
            var townName = "First Name";
            var newTown = new Town
            {
                Id = townId,
                Name = townName,
            };
            await townsRepository.AddAsync(newTown);
            newTown = new Town
            {
                Id = 2,
                Name = "Second Name",
            };
            await townsRepository.AddAsync(newTown);
            await townsRepository.SaveChangesAsync();

            var result = service.GetAllTownsAsKetValuePairs();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            var firstKeyValuePair = new KeyValuePair<string, string>(
                townId.ToString(),
                townName);
            var resultValue = result.FirstOrDefault();
            var objExpect = JsonConvert.SerializeObject(firstKeyValuePair);
            var objResult = JsonConvert.SerializeObject(resultValue);
            Assert.Equal(objExpect, objResult);
        }

        [Fact]
        public async Task GetAllTownsPagingAsyncProperlyRetrunTowns()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                      .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var db = new ApplicationDbContext(options);
            var townsRepository = new EfDeletableEntityRepository<Town>(db);
            new MapperInitializationProfile();

            var service = new TownsService(
                townsRepository);
            var newTown = new Town { Id = 1, Name = "New name" };
            await townsRepository.AddAsync(newTown);
            newTown = new Town { Id = 2, Name = "New name 1"};
            await townsRepository.AddAsync(newTown);
            newTown = new Town { Id = 3, Name = "New name 2" };
            await townsRepository.AddAsync(newTown);
            newTown = new Town { Id = 4, Name = "New name 3" };
            await townsRepository.AddAsync(newTown);
            newTown = new Town { Id = 5, Name = "New name 4" };
            await townsRepository.AddAsync(newTown);
            newTown = new Town { Id = 6, Name = "New name 5" };
            await townsRepository.AddAsync(newTown);
            await townsRepository.SaveChangesAsync();

            var page = 1;
            var itemsPerPage = 5;
            var result = service.GetAllTownsPagingAsync<TownViewModelTest>(page, itemsPerPage);

            Assert.NotNull(result);
            Assert.Equal(5, result.Result.Count());
        }

        [Fact]
        public async Task CountAsyncProperlyRetrunCounts()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                      .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var db = new ApplicationDbContext(options);
            var townsRepository = new EfDeletableEntityRepository<Town>(db);
            new MapperInitializationProfile();

            var service = new TownsService(
                townsRepository);
            var newTown = new Town { Id = 1, Name = "New name" };
            await townsRepository.AddAsync(newTown);
            newTown = new Town { Id = 2, Name = "New name 1" };
            await townsRepository.AddAsync(newTown);
            newTown = new Town { Id = 3, Name = "New name 2" };
            await townsRepository.AddAsync(newTown);
            newTown = new Town { Id = 4, Name = "New name 3" };
            await townsRepository.AddAsync(newTown);
            await townsRepository.SaveChangesAsync();

            var result = service.CountAsync();

            Assert.NotNull(result);
            Assert.Equal(4, result.Result);
        }

        [Fact]
        public async Task GetTownByIdAsyncProperlyRetrunTown()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                      .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var db = new ApplicationDbContext(options);
            var townsRepository = new EfDeletableEntityRepository<Town>(db);
            new MapperInitializationProfile();

            var service = new TownsService(
                townsRepository);
            var newTown = new Town { Id = 1, Name = "New name" };
            await townsRepository.AddAsync(newTown);
            var townName = "New name 1";
            newTown = new Town { Id = 2, Name = townName };
            await townsRepository.AddAsync(newTown);
            newTown = new Town { Id = 3, Name = "New name 2" };
            await townsRepository.AddAsync(newTown);
            newTown = new Town { Id = 4, Name = "New name 3" };
            await townsRepository.AddAsync(newTown);
            await townsRepository.SaveChangesAsync();

            var result = service.GetTownByIdAsync(2);

            Assert.NotNull(result);
            Assert.Equal(townName, result.Result.Name);
        }

        [Fact]
        public async Task UpdateProperlySetTownName()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                      .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var db = new ApplicationDbContext(options);
            var townsRepository = new EfDeletableEntityRepository<Town>(db);
            new MapperInitializationProfile();

            var service = new TownsService(
                townsRepository);
            var newTown = new Town { Id = 1, Name = "New name" };
            await townsRepository.AddAsync(newTown);
            var townName = "New name 1";
            newTown = new Town { Id = 2, Name = townName };
            await townsRepository.AddAsync(newTown);
            newTown = new Town { Id = 3, Name = "New name 2" };
            await townsRepository.AddAsync(newTown);
            newTown = new Town { Id = 4, Name = "New name 3" };
            await townsRepository.AddAsync(newTown);
            await townsRepository.SaveChangesAsync();


            var townNameForUpdate = "New name 1 Updated";
            var newTownForUpdate = new TownInputModel
            {
                Id = 2,
                Name = townNameForUpdate,
            };

            var result = service.Update(newTownForUpdate);

            Assert.NotNull(result);

            var resultTown = await townsRepository
                .AllAsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == newTownForUpdate.Id);

            Assert.NotNull(resultTown);
            Assert.Equal(townNameForUpdate, resultTown.Name);
        }

        [Fact]
        public async Task CreateAsyncProperlyCreateTown()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                      .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var db = new ApplicationDbContext(options);
            var townsRepository = new EfDeletableEntityRepository<Town>(db);
            new MapperInitializationProfile();

            var service = new TownsService(
                townsRepository);
            var newTown = new Town { Id = 1, Name = "New name" };
            await townsRepository.AddAsync(newTown);
            var townName = "New name 1";
            newTown = new Town { Id = 2, Name = townName };
            await townsRepository.AddAsync(newTown);
            newTown = new Town { Id = 3, Name = "New name 2" };
            await townsRepository.AddAsync(newTown);
            newTown = new Town { Id = 4, Name = "New name 3" };
            await townsRepository.AddAsync(newTown);
            await townsRepository.SaveChangesAsync();

            var townIdForCreate = 5;
            var townNameForCreate = "New name 5 Create";
            var newTownForCreate = new TownInputModel
            {
                Id = townIdForCreate,
                Name = townNameForCreate,
            };

            var result = service.CreateAsync(newTownForCreate);

            var resultTown = await townsRepository
                .AllAsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == townIdForCreate);
            Assert.NotNull(resultTown);
            Assert.Equal(townNameForCreate, resultTown.Name);
        }

        [Fact]
        public async Task TownExistReturnTrueWhenExist()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                      .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var db = new ApplicationDbContext(options);
            var townsRepository = new EfDeletableEntityRepository<Town>(db);
            new MapperInitializationProfile();

            var service = new TownsService(
                townsRepository);
            var townId = 1;
            var newTown = new Town { Id = townId, Name = "New name" };
            await townsRepository.AddAsync(newTown);
            await townsRepository.SaveChangesAsync();

            var result = await service.TownExist(townId);

            Assert.True(result);
        }

        [Fact]
        public async Task TownExistReturnFalseWhenNotExist()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                      .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var db = new ApplicationDbContext(options);
            var townsRepository = new EfDeletableEntityRepository<Town>(db);
            new MapperInitializationProfile();

            var service = new TownsService(
                townsRepository);
            var townId = 1;
            var newTown = new Town { Id = townId, Name = "New name" };
            await townsRepository.AddAsync(newTown);
            await townsRepository.SaveChangesAsync();
            var newTownId = 2;
            var result = await service.TownExist(newTownId);

            Assert.False(result);
        }

        [Fact]
        public async Task DeleteProperlyDeleteTown()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                      .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var db = new ApplicationDbContext(options);
            var townsRepository = new EfDeletableEntityRepository<Town>(db);
            new MapperInitializationProfile();

            var service = new TownsService(
                townsRepository);
            var newTown = new Town { Id = 1, Name = "New name" };
            await townsRepository.AddAsync(newTown);
            var townName = "New name 1";
            newTown = new Town { Id = 2, Name = townName };
            await townsRepository.AddAsync(newTown);
            var townIdForDelete = 3;
            var townNameForDelete = "New name 2 Detele";
            newTown = new Town { Id = townIdForDelete, Name = townNameForDelete};
            await townsRepository.AddAsync(newTown);
            newTown = new Town { Id = 4, Name = "New name 3" };
            await townsRepository.AddAsync(newTown);
            await townsRepository.SaveChangesAsync();

            var result = service.Delete(townIdForDelete);

            Assert.NotNull(result);
            Assert.Equal(townNameForDelete, result.Result.Name);
        }

        public class TownViewModelTest : IMapFrom<Town>
        {
            public int Id { get; set; }

            public string Name { get; set; }
        }
    }
}
