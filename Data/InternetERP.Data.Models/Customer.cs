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

        [MaxLength(30)]
        public string District { get; set; }

        [Required]
        [MaxLength(50)]
        public string Street { get; set; }

        [Required]
        public string VATNumber { get; set; }

        [Required]
        public int BulstatNumber { get; set; }

        [Required]
        [MaxLength(80)]
        public string MOL { get; set; }

        [Required]
        [MaxLength(80)]
        public string ReceivedFrom { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal BayPrice { get; set; }

        public virtual ICollection<Phone> Phones { get; set; }
    }
}
