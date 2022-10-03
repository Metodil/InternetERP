using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InternetERP.Models
{
    public class Employee
    {
        public Employee()
        {
            this.Teams = new HashSet<Team>();
            this.Phones = new HashSet<Phone>();
        }
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string FullName { get; set; }
        [Required]
        public int AddressId { get; set; }
        public virtual Address Address { get; set; }
        [Required]
        public int JobTitleId { get; set; }
        public virtual JobTitle JobbTitle { get; set; }
        [Required]
        public DateTime HireDate { get; set; }
        public decimal Salary { get; set; }
        public virtual ICollection<Team> Teams { get; set; }
        //todo FailuresTeams вместо EmployeeTeam
        public virtual ICollection<Phone> Phones { get; set; }
    }
}
