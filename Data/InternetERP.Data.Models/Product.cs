#nullable disable

namespace InternetERP.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using InternetERP.Data.Common.Models;

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

        public string Description { get; set; } = string.Empty;

        public virtual ICollection<Image> Images { get; set; }
    }
}
