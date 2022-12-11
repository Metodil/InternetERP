namespace InternetERP.Services.Data.Tests
{
    using System;
    using System.Threading.Tasks;

    using InternetERP.Common;
    using InternetERP.Data;
    using InternetERP.Data.Models;
    using InternetERP.Data.Repositories;
    using InternetERP.Services.Data.Employee;
    using InternetERP.Services.Mapping;
    using InternetERP.Web.ViewModels.Employee.Technicians;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class TechniciansServiceTests
    {
        [Fact]
        public async Task GetAllFailuresAsyncReturnProperlyFailureCouts()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                  .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var db = new ApplicationDbContext(options);
            var failureRepository = new EfDeletableEntityRepository<Failure>(db);
            var statusFailureRepository = new EfDeletableEntityRepository<StatusFailure>(db);
            var employeesRepository = new EfDeletableEntityRepository<Employee>(db);
            var failurePhasesRepository = new EfDeletableEntityRepository<FailurePhase>(db);
            new MapperInitializationProfile();
            var service = new TechniciansService(
                failureRepository,
                statusFailureRepository,
                employeesRepository,
                failurePhasesRepository);
            var page = 1;
            var itemsPerPage = 5;
            var filterByStatus = 0;
            await failureRepository.AddAsync(this.NewFailure());
            await failureRepository.AddAsync(this.NewFailure());
            await failureRepository.AddAsync(this.NewFailure());
            await failureRepository.AddAsync(this.NewFailure());
            await failureRepository.AddAsync(this.NewFailure());
            await failureRepository.AddAsync(this.NewFailure());
            await failureRepository.SaveChangesAsync();

            var result = service.GetAllFailuresAsync<AllFailuresViewModelTest>(
                page,
                itemsPerPage,
                filterByStatus);

            Assert.Equal(5, result.Result.Count);
        }

        [Fact]
        public async Task GetAllFailuresAsyncReturnProperlyFailureCoutsAndFilterByIsSet()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                  .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var db = new ApplicationDbContext(options);
            var failureRepository = new EfDeletableEntityRepository<Failure>(db);
            var statusFailureRepository = new EfDeletableEntityRepository<StatusFailure>(db);
            var employeesRepository = new EfDeletableEntityRepository<Employee>(db);
            var failurePhasesRepository = new EfDeletableEntityRepository<FailurePhase>(db);
            new MapperInitializationProfile();
            var service = new TechniciansService(
                failureRepository,
                statusFailureRepository,
                employeesRepository,
                failurePhasesRepository);

            await statusFailureRepository.AddAsync(new StatusFailure
            {
                Name = "First status",
            });
            await statusFailureRepository.AddAsync(new StatusFailure
            {
                Name = "Second status",
            });
            await statusFailureRepository.SaveChangesAsync();

            await failureRepository.AddAsync(this.NewFailure());
            await failureRepository.AddAsync(this.NewFailure());
            await failureRepository.AddAsync(this.NewFailure());
            var createUserId = Guid.NewGuid().ToString();
            var newFailure = new Failure
            {
                ShortDescription = "ShortDescription",
                Note = "It is second time!",
                Price = 10m,
                AccountId = 1,
                CreateUserId = createUserId,
                StatusFailureId = 2,
                FailurePhaseId = 1,
            };
            await failureRepository.AddAsync(newFailure);
            await failureRepository.AddAsync(this.NewFailure());
            await failureRepository.AddAsync(this.NewFailure());
            await failureRepository.SaveChangesAsync();
            var page = 1;
            var itemsPerPage = 5;
            var filterByStatus = 2;
            var result = service.GetAllFailuresAsync<AllFailuresViewModelTest>(
                page,
                itemsPerPage,
                filterByStatus);

            Assert.Equal(1, result.Result.Count);
        }

        [Fact]
        public async Task CountAsyncGetProperlyCountWhenFilterByStatusIsNotUsed()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var db = new ApplicationDbContext(options);
            var failureRepository = new EfDeletableEntityRepository<Failure>(db);
            var statusFailureRepository = new EfDeletableEntityRepository<StatusFailure>(db);
            var employeesRepository = new EfDeletableEntityRepository<Employee>(db);
            var failurePhasesRepository = new EfDeletableEntityRepository<FailurePhase>(db);

            var service = new TechniciansService(
                failureRepository,
                statusFailureRepository,
                employeesRepository,
                failurePhasesRepository);

            await failureRepository.AddAsync(this.NewFailure());
            await failureRepository.AddAsync(this.NewFailure());
            await failureRepository.AddAsync(this.NewFailure());
            await failureRepository.AddAsync(this.NewFailure());
            await failureRepository.AddAsync(this.NewFailure());
            await failureRepository.AddAsync(this.NewFailure());
            await failureRepository.SaveChangesAsync();

            var filterByStatus = 0;
            var result = service.CountAsync(filterByStatus);

            Assert.Equal(6, result.Result);
        }

        [Fact]
        public async Task CountAsyncGetProperlyCountWhenFilterByStatusIsUsed()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var db = new ApplicationDbContext(options);
            var failureRepository = new EfDeletableEntityRepository<Failure>(db);
            var statusFailureRepository = new EfDeletableEntityRepository<StatusFailure>(db);
            var employeesRepository = new EfDeletableEntityRepository<Employee>(db);
            var failurePhasesRepository = new EfDeletableEntityRepository<FailurePhase>(db);

            var service = new TechniciansService(
                failureRepository,
                statusFailureRepository,
                employeesRepository,
                failurePhasesRepository);
            await statusFailureRepository.AddAsync(new StatusFailure
            {
                Name = "First status",
            });
            await statusFailureRepository.AddAsync(new StatusFailure
            {
                Name = "Second status",
            });

            await statusFailureRepository.SaveChangesAsync();

            var newStatus = this.NewFailure();
            await failureRepository.AddAsync(newStatus);
            newStatus = this.NewFailure();
            newStatus.StatusFailureId = 2;
            await failureRepository.AddAsync(newStatus);
            newStatus = this.NewFailure();
            newStatus.StatusFailureId = 2;
            await failureRepository.AddAsync(newStatus);
            newStatus = this.NewFailure();
            await failureRepository.AddAsync(newStatus);
            await failureRepository.AddAsync(newStatus);
            await failureRepository.AddAsync(newStatus);
            await failureRepository.SaveChangesAsync();

            var filterByStatus = 2;
            var result = service.CountAsync(filterByStatus);

            Assert.Equal(2, result.Result);
        }

        [Fact]
        public async Task SaveFailurProperlyAddFailure()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var db = new ApplicationDbContext(options);
            var failureRepository = new EfDeletableEntityRepository<Failure>(db);
            var statusFailureRepository = new EfDeletableEntityRepository<StatusFailure>(db);
            var employeesRepository = new EfDeletableEntityRepository<Employee>(db);
            var failurePhasesRepository = new EfDeletableEntityRepository<FailurePhase>(db);

            var service = new TechniciansService(
                failureRepository,
                statusFailureRepository,
                employeesRepository,
                failurePhasesRepository);

            var createUserId = Guid.NewGuid().ToString();
            var newFailure = new Failure
            {
                ShortDescription = "ShortDescription",
                Note = "It is second time!",
                Price = 10m,
                AccountId = 1,
                CreateUserId = createUserId,
                StatusFailureId = 2,
                FailurePhaseId = 1,
            };
            await failureRepository.AddAsync(newFailure);
            await failureRepository.SaveChangesAsync();

            var upatedShortDescription = "Updated ShortDescription";
            var updatedNote = "Updated It is second time!";
            var updateFailure = new FailureEditInputModel
            {
                Id = 1,
                ShortDescription = upatedShortDescription,
                Note = updatedNote,
                Price = 20m,
            };
            await service.SaveFailure(updateFailure);

            var result = failureRepository
                .AllAsNoTracking()
                .FirstOrDefaultAsync();

            Assert.Equal(upatedShortDescription, result.Result.ShortDescription);
            Assert.Equal(updatedNote, result.Result.Note);
        }

        [Fact]
        public async Task GetFailuresByIdAsyncProperlyReturnFailure()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var db = new ApplicationDbContext(options);
            var failureRepository = new EfDeletableEntityRepository<Failure>(db);
            var statusFailureRepository = new EfDeletableEntityRepository<StatusFailure>(db);
            var employeesRepository = new EfDeletableEntityRepository<Employee>(db);
            var failurePhasesRepository = new EfDeletableEntityRepository<FailurePhase>(db);

            var service = new TechniciansService(
                failureRepository,
                statusFailureRepository,
                employeesRepository,
                failurePhasesRepository);
            var createUserId = Guid.NewGuid().ToString();
            var failureId = 2;
            var newFailure = new Failure
            {
                Id = failureId,
                ShortDescription = "ShortDescription",
                Note = "It is second time!",
                Price = 10m,
                AccountId = 1,
                CreateUserId = createUserId,
                StatusFailureId = 1,
                FailurePhaseId = 1,
            };
            await failureRepository.AddAsync(newFailure);
            await failureRepository.SaveChangesAsync();

            var result = service.GetFailuresByIdAsync<FailureEditInputModel>(failureId);

            var resultFailure = failureRepository
                .AllAsNoTracking()
                .FirstOrDefaultAsync();

            Assert.Equal(failureId, resultFailure.Result.Id);
        }

        [Fact]
        public async Task ChangeFailureStatusProperlyChangeStatus()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var db = new ApplicationDbContext(options);
            var failureRepository = new EfDeletableEntityRepository<Failure>(db);
            var statusFailureRepository = new EfDeletableEntityRepository<StatusFailure>(db);
            var employeesRepository = new EfDeletableEntityRepository<Employee>(db);
            var failurePhasesRepository = new EfDeletableEntityRepository<FailurePhase>(db);

            var service = new TechniciansService(
                failureRepository,
                statusFailureRepository,
                employeesRepository,
                failurePhasesRepository);
            var createUserId = Guid.NewGuid().ToString();

            var employee = new Employee
            {
                EmployeeUserId = createUserId,
                FailureTeamId = 2,
            };
            await employeesRepository.AddAsync(employee);

            var newFailure = new Failure
            {
                Id = 1,
                ShortDescription = "ShortDescription",
                Note = "It is second time!",
                Price = 10m,
                AccountId = 1,
                CreateUserId = createUserId,
                StatusFailureId = 1,
                FailurePhaseId = 1,
            };
            await failureRepository.AddAsync(newFailure);
            await failureRepository.SaveChangesAsync();

            var newFailureInput = new FailureEditInputModel
            {
                Id = 1,
                ShortDescription = "ShortDescription",
                Note = "It is second time!",
                Price = 10m,
            };
            await service.ChangeFailureStatus(
                newFailureInput,
                createUserId,
                GlobalConstants.FailureFinishedStatusId);

            var resultFailure = failureRepository
                .AllAsNoTracking()
                .FirstOrDefaultAsync();

            var resultFailurePhases = failurePhasesRepository
                .AllAsNoTracking()
                .FirstOrDefaultAsync();

            Assert.Equal(GlobalConstants.FailureFinishedStatusId, resultFailure.Result.StatusFailureId);
            Assert.Equal(createUserId, resultFailurePhases.Result.UserId);
        }

        public Failure NewFailure()
        {
            var createUserId = Guid.NewGuid().ToString();
            return new Failure
            {
                ShortDescription = "ShortDescription",
                Note = "It is second time!",
                AccountId = 1,
                CreateUserId = createUserId,
                StatusFailureId = 1,
            };
        }

        public class AllFailuresViewModelTest : IMapFrom<Failure>
        {
            public int Id { get; set; }
        }
    }
}
