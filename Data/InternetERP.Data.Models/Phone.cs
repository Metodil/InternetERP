using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InternetERP.Models
{
    public class Phone
    {
        public Phone()
        {
            this.Employees = new HashSet<Employee>();
            this.Accounts = new HashSet<Account>();
        }
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string PhoneNumber { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<Account> Accounts { get; set; }

    }
}
