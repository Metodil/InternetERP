namespace InternetERP.Web.ViewModels.Employee.Manager
{
    using System.Collections.Generic;

    using InternetERP.Data.Models;

    public class FailureTeamsViewModel
    {
        public FailureTeamsViewModel()
        {
            this.FailureTeams = new HashSet<FailureTeam>();
            this.EmployeesInTeam = new HashSet<ApplicationUser>();
            this.FreeEmployees = new HashSet<ApplicationUser>();
            this.SelectedEmployee = new HashSet<string>();
        }

        public string ManagedTeam { get; set; }

        public string SuccessfullyMsg { get; set; }

        public virtual ICollection<FailureTeam> FailureTeams { get; set; }

        public virtual ICollection<ApplicationUser> EmployeesInTeam { get; set; }

        public virtual ICollection<ApplicationUser> FreeEmployees { get; set; }

        public virtual ICollection<string> SelectedEmployee { get; set; }
    }
}
