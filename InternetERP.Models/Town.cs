using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InternetERP.Models
{
    public class Town
    {
        public Town()
        {
            this.Addresses = new HashSet<Address>();
        }
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }
    }
}
