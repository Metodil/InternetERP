#nullable disable

namespace InternetERP.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using InternetERP.Data.Common.Models;

    public class Phone : BaseDeletableModel<int>
    {
        public Phone()
        {
            this.Employees = new HashSet<Employee>();
            this.InternetAccounts = new HashSet<InternetAccount>();
            this.Customers = new HashSet<Customer>();
        }

        [Required]
        [MaxLength(30)]
        public string PhoneNumber { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }

        public virtual ICollection<InternetAccount> InternetAccounts { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }
    }
}
