#nullable disable

namespace InternetERP.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using InternetERP.Data.Common.Models;
    using InternetERP.Data.Models;
    using Microsoft.EntityFrameworkCore.Metadata.Internal;

    public class Customer : BaseDeletableModel<int>
    {
        public Customer()
        {
            this.Phones = new HashSet<Phone>();
        }

        [Required]
        [MaxLength(150)]
        public string Name { get; set; }

        [Required]
        public int TownId { get; set; }

        public virtual Town Town { get; set; }

        [Required]
        [MaxLength(50)]
        public string Address { get; set; }

        [Required]
        public string VATNumber { get; set; }

        [Required]
        public string BulstatNumber { get; set; }

        [Required]
        [MaxLength(80)]
        public string MOL { get; set; }

        [Required]
        [MaxLength(80)]
        public string ReceivedFrom { get; set; }

        public virtual ICollection<Phone> Phones { get; set; }
    }
}
