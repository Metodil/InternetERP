namespace InternetERP.Services.Data.Employee
{
    using System.Collections.Generic;
    using System.Data.Common;
    using System.Linq;
    using System.Threading.Tasks;

    using InternetERP.Common;
    using InternetERP.Data.Common.Repositories;
    using InternetERP.Data.Models;
    using InternetERP.Services.Data.Contracts;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    public class FailureTeamsService : IFailureTeamsService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        private readonly IDeletableEntityRepository<FailureTeam> failureTeamsRepository;
        private readonly IDeletableEntityRepository<Employee> employeeRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public FailureTeamsService(
            IDeletableEntityRepository<ApplicationUser> userRepository,
            IDeletableEntityRepository<FailureTeam> failureTeamsRepository,
            IDeletableEntityRepository<Employee> employeeRepository,
            UserManager<ApplicationUser> userManager)
        {
            this.userRepository = userRepository;
            this.failureTeamsRepository = failureTeamsRepository;
            this.employeeRepository = employeeRepository;
            this.userManager = userManager;
        }

        public async Task<int> CountAsync()
        {
            return await this.failureTeamsRepository
                .AllAsNoTracking()
                .CountAsync();
        }

        public async Task<ICollection<FailureTeam>> GetAllTeamsAsync()
        {
            return await this.failureTeamsRepository
                .AllAsNoTracking()
                .ToListAsync();
        }

        public async Task<ICollection<ApplicationUser>> GetEmployeesInTeamAsync(string managedTeam)
        {
            var team = this.failureTeamsRepository
                .AllAsNoTracking()
                .Where(t => t.Name == managedTeam)
                .FirstOrDefault();
            if (team != null)
            {
                var employeeInTeam = new HashSet<string>(this.employeeRepository
                    .AllAsNoTracking()
                    .Where(e => e.FailureTeamId == team.Id)
                    .Select(e => e.EmployeeUserId));
                var users = await this.userRepository
                    .AllAsNoTracking()
                    .Where(u => employeeInTeam.Contains(u.Id))
                    .ToListAsync();
                return users;
            }

            return null;
        }

        public async Task<ICollection<ApplicationUser>> GetTechnicianEmployeesAsync()
        {
            return await this.userManager
                .GetUsersInRoleAsync(GlobalConstants.TechnicianRoleName);
        }

        public async Task<bool> UpdateEmployeeToTeam(string managedTeam, ICollection<string> selectedEmployees)
        {
            var result = true;
            var failuteTeam = this.failureTeamsRepository
                .AllAsNoTracking()
                .Where(ft => ft.Name == managedTeam)
                .FirstOrDefault();
            if (failuteTeam == null)
            {
                return false;
            }

            var employeesForDelete = await this.employeeRepository
                .All()
                .Where(e => e.FailureTeamId == failuteTeam.Id)
                .ToListAsync();
            foreach (var employee in employeesForDelete)
            {
                employee.FailureTeamId = null;
            }

            await this.employeeRepository.SaveChangesAsync();

            foreach (var selectedEmployee in selectedEmployees)
            {
                var employeeForUpdate = await this.employeeRepository
                    .All()
                    .Where(e => e.EmployeeUserId == selectedEmployee)
                    .FirstOrDefaultAsync();
                try
                {
                        employeeForUpdate.FailureTeamId = failuteTeam.Id;
                        this.employeeRepository.Update(employeeForUpdate);
                    }
                    catch (DbException)
                    {
                        // TODO Log ERROR
                        result = false;
                    }
            }

            await this.employeeRepository.SaveChangesAsync();
            return result;
        }
    }
}
