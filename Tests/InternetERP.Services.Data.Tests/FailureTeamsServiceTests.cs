namespace InternetERP.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using InternetERP.Data;
    using InternetERP.Data.Models;
    using InternetERP.Data.Repositories;
    using InternetERP.Services.Data.Employee;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Xunit;

    public class FailureTeamsServiceTests
    {
        [Fact]
        public async Task CountAsyncProperlyReturnCount()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                      .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var db = new ApplicationDbContext(options);
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(db);
            var failureTeamsRepository = new EfDeletableEntityRepository<FailureTeam>(db);
            var employeeRepository = new EfDeletableEntityRepository<Employee>(db);
            var userManager = new Mock<UserManager<ApplicationUser>>(
                new Mock<IUserStore<ApplicationUser>>().Object, null, null, null, null, null, null, null, null)
                .Object;

            var service = new FailureTeamsService(
                userRepository,
                failureTeamsRepository,
                employeeRepository,
                userManager);
            var newFailureTeam = new FailureTeam
            {
                Name = "Name of team",
            };
            await failureTeamsRepository.AddAsync(newFailureTeam);
            await failureTeamsRepository.SaveChangesAsync();

            var result = await service.CountAsync();

            Assert.Equal(1, result);
        }

        [Fact]
        public async Task GetAllTeamsAsyncProperlyReturnTeams()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                      .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var db = new ApplicationDbContext(options);
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(db);
            var failureTeamsRepository = new EfDeletableEntityRepository<FailureTeam>(db);
            var employeeRepository = new EfDeletableEntityRepository<Employee>(db);
            var userManager = new Mock<UserManager<ApplicationUser>>(
                new Mock<IUserStore<ApplicationUser>>().Object, null, null, null, null, null, null, null, null)
                .Object;

            var service = new FailureTeamsService(
                userRepository,
                failureTeamsRepository,
                employeeRepository,
                userManager);
            var newFailureTeam = new FailureTeam
            {
                Name = "Name of team",
            };
            await failureTeamsRepository.AddAsync(newFailureTeam);
            newFailureTeam = new FailureTeam
            {
                Name = "Name of team second",
            };
            await failureTeamsRepository.AddAsync(newFailureTeam);
            newFailureTeam = new FailureTeam
            {
                Name = "Name of team third",
            };
            await failureTeamsRepository.AddAsync(newFailureTeam);
            await failureTeamsRepository.SaveChangesAsync();

            var result = await service.GetAllTeamsAsync();

            Assert.NotNull(result);
            Assert.Equal(3, result.Count);
        }

        [Fact]
        public async Task GetEmployeesInTeamAsyncProperlyReturnEmployeeInTeams()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                      .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var db = new ApplicationDbContext(options);
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(db);
            var failureTeamsRepository = new EfDeletableEntityRepository<FailureTeam>(db);
            var employeeRepository = new EfDeletableEntityRepository<Employee>(db);
            var userManager = new Mock<UserManager<ApplicationUser>>(
                new Mock<IUserStore<ApplicationUser>>().Object, null, null, null, null, null, null, null, null)
                .Object;

            var service = new FailureTeamsService(
                userRepository,
                failureTeamsRepository,
                employeeRepository,
                userManager);
            var teamName = "Name of team";
            var newFailureTeam = new FailureTeam
            {
                Id = 1,
                Name = teamName,
            };
            await failureTeamsRepository.AddAsync(newFailureTeam);
            await failureTeamsRepository.SaveChangesAsync();

            var userIdFirst = Guid.NewGuid().ToString();
            var newEmployee = new Employee
            {
                EmployeeUserId = userIdFirst,
                HireDate = DateTime.UtcNow,
                Salary = 1000m,
                FailureTeamId = 1,
            };
            await employeeRepository.AddAsync(newEmployee);
            var userIdSecond = Guid.NewGuid().ToString();
            newEmployee = new Employee
            {
                EmployeeUserId = userIdFirst,
                HireDate = DateTime.UtcNow,
                Salary = 1000m,
                FailureTeamId = 2,
            };
            await employeeRepository.AddAsync(newEmployee);
            await employeeRepository.SaveChangesAsync();

            var newUser = new ApplicationUser
            {
                Id = userIdFirst,
            };
            await userRepository.AddAsync(newUser);
            newUser = new ApplicationUser
            {
                Id = userIdSecond,
            };
            await userRepository.AddAsync(newUser);
            await userRepository.SaveChangesAsync();

            var result = await service.GetEmployeesInTeamAsync(teamName);

            Assert.NotNull(result);
            Assert.Equal(1, result.Count);
            var firstIdResult = result
                .FirstOrDefault();
            Assert.Equal(userIdFirst, firstIdResult.Id);
        }

        [Fact]
        public async Task GetEmployeesInTeamAsyncReturnNullWhenTeamNameIsNotExist()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                      .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var db = new ApplicationDbContext(options);
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(db);
            var failureTeamsRepository = new EfDeletableEntityRepository<FailureTeam>(db);
            var employeeRepository = new EfDeletableEntityRepository<Employee>(db);
            var userManager = new Mock<UserManager<ApplicationUser>>(
                new Mock<IUserStore<ApplicationUser>>().Object, null, null, null, null, null, null, null, null)
                .Object;

            var service = new FailureTeamsService(
                userRepository,
                failureTeamsRepository,
                employeeRepository,
                userManager);
            var teamName = "Name of team";
            var newFailureTeam = new FailureTeam
            {
                Id = 1,
                Name = teamName,
            };
            await failureTeamsRepository.AddAsync(newFailureTeam);
            await failureTeamsRepository.SaveChangesAsync();

            var userIdFirst = Guid.NewGuid().ToString();
            var newEmployee = new Employee
            {
                EmployeeUserId = userIdFirst,
                HireDate = DateTime.UtcNow,
                Salary = 1000m,
                FailureTeamId = 1,
            };
            await employeeRepository.AddAsync(newEmployee);
            var userIdSecond = Guid.NewGuid().ToString();
            newEmployee = new Employee
            {
                EmployeeUserId = userIdSecond,
                HireDate = DateTime.UtcNow,
                Salary = 1000m,
                FailureTeamId = 2,
            };
            await employeeRepository.AddAsync(newEmployee);
            await employeeRepository.SaveChangesAsync();

            var newUser = new ApplicationUser
            {
                Id = userIdFirst,
            };
            await userRepository.AddAsync(newUser);
            newUser = new ApplicationUser
            {
                Id = userIdSecond,
            };
            await userRepository.AddAsync(newUser);
            await userRepository.SaveChangesAsync();
            var teamNameNotExist = "Team name not exists";
            var result = await service.GetEmployeesInTeamAsync(teamNameNotExist);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetTechnicianEmployeesAsyncReturnNullIsUsersNotExist()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                      .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var db = new ApplicationDbContext(options);
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(db);
            var failureTeamsRepository = new EfDeletableEntityRepository<FailureTeam>(db);
            var employeeRepository = new EfDeletableEntityRepository<Employee>(db);
            var userManager = new Mock<UserManager<ApplicationUser>>(
                new Mock<IUserStore<ApplicationUser>>().Object, null, null, null, null, null, null, null, null)
                .Object;

            var user = new ApplicationUser { UserName = "Test Name" };
            ICollection<ApplicationUser> users = new List<ApplicationUser>();
            users.Add(user);

            // userManager
            //    .Setup(m => m.GetUsersInRoleAsync(GlobalConstants.TechnicianRoleName))
            //    .Returns(await Task.Result(users));
            var service = new FailureTeamsService(
                userRepository,
                failureTeamsRepository,
                employeeRepository,
                userManager);

            var result = await service.GetTechnicianEmployeesAsync();

            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateEmployeeToTeamProperlyUpdate()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                      .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var db = new ApplicationDbContext(options);
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(db);
            var failureTeamsRepository = new EfDeletableEntityRepository<FailureTeam>(db);
            var employeeRepository = new EfDeletableEntityRepository<Employee>(db);
            var userManager = new Mock<UserManager<ApplicationUser>>(
                new Mock<IUserStore<ApplicationUser>>().Object, null, null, null, null, null, null, null, null)
                .Object;
            var service = new FailureTeamsService(
                userRepository,
                failureTeamsRepository,
                employeeRepository,
                userManager);

            var user = new ApplicationUser { UserName = "Test Name" };
            ICollection<ApplicationUser> users = new List<ApplicationUser>();
            users.Add(user);

            var managedTeamName = "Name of team";
            var newFailureTeam = new FailureTeam
            {
                Id = 1,
                Name = managedTeamName,
            };
            await failureTeamsRepository.AddAsync(newFailureTeam);
            await failureTeamsRepository.SaveChangesAsync();

            var userIdFirst = Guid.NewGuid().ToString();
            var newEmployee = new Employee
            {
                EmployeeUserId = userIdFirst,
                HireDate = DateTime.UtcNow,
                Salary = 1000m,
                FailureTeamId = 1,
            };
            await employeeRepository.AddAsync(newEmployee);
            var userIdSecond = Guid.NewGuid().ToString();
            newEmployee = new Employee
            {
                EmployeeUserId = userIdSecond,
                HireDate = DateTime.UtcNow,
                Salary = 1000m,
                FailureTeamId = 2,
            };
            await employeeRepository.AddAsync(newEmployee);
            var userIdThird = Guid.NewGuid().ToString();
            newEmployee = new Employee
            {
                EmployeeUserId = userIdThird,
                HireDate = DateTime.UtcNow,
                Salary = 1000m,
                FailureTeamId = 3,
            };
            await employeeRepository.AddAsync(newEmployee);
            await employeeRepository.SaveChangesAsync();
            ICollection<string> selectedEmployees = new List<string>();
            selectedEmployees.Add(userIdFirst);
            selectedEmployees.Add(userIdSecond);

            var result = await service.UpdateEmployeeToTeam(
                managedTeamName,
                selectedEmployees);

            Assert.True(result);
            var resultEmployees = await employeeRepository
                .AllAsNoTracking()
                .Where(x => x.FailureTeamId == 1)
                .ToListAsync();

            Assert.Equal(2, resultEmployees.Count);
        }
    }
}
