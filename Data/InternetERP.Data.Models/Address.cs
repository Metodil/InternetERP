using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InternetERP.Models
{
    public class Address
    {
        public Address()
        {
            this.Accounts = new HashSet<Account>();
        }
        public int Id { get; set; }
        [Required]
        public int TownId { get; set; }
        public virtual Town Town { get; set; }
        [MaxLength(30)]
        public string District { get; set; }
        [Required]
        [MaxLength(50)]
        public string Street { get; set; }
        public string Note { get; set; }
        public virtual ICollection<Account> Accounts { get; set; }
    }
}
