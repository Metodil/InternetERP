#nullable disable

namespace InternetERP.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using InternetERP.Data.Common.Models;

    public class Employee : BaseDeletableModel<int>
    {
        public Employee()
        {
        }

        public string EmployeeUserId { get; set; }

        public virtual ApplicationUser EmployeeUser { get; set; }

        [Required]
        public DateTime HireDate { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Salary { get; set; }

        public int? FailureTeamId { get; set; }

        public virtual FailureTeam FailureTeams { get; set; }
    }
}
