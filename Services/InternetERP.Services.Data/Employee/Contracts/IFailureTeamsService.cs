namespace InternetERP.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using InternetERP.Data.Models;

    public interface IFailureTeamsService
    {
        Task<int> CountAsync();

        public Task<ICollection<FailureTeam>> GetAllTeamsAsync();

        Task<ICollection<ApplicationUser>> GetEmployeesInTeamAsync(string managedTeam);

        Task<ICollection<ApplicationUser>> GetTechnicianEmployeesAsync();

        Task<bool> UpdateEmployeeToTeam(string managedTeam, ICollection<string> selectedEmployee);
    }
}
