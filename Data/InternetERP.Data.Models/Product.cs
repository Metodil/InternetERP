#nullable disable

namespace InternetERP.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using InternetERP.Data.Common.Models;
    using InternetERP.Data.Seeding;

    public class Product : BaseDeletableModel<int>
    {
        public Product()
        {
            this.Images = new HashSet<Image>();
        }

        [Required]
        [MaxLength(150)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal SellPrice { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal BayPrice { get; set; }

        [Required]
        public int StockQuantity { get; set; }

        #pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        public int? PromotionId { get; set; }
        #pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.

        public Promotion Promotion { get; set; }

        public string Description { get; set; } = string.Empty;

        public virtual ICollection<Image> Images { get; set; }
    }
}
