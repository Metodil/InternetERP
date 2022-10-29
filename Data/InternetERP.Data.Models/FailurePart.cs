#nullable disable

namespace InternetERP.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using InternetERP.Data.Common.Models;

    public class FailurePart : BaseDeletableModel<int>
    {
        [Required]
        public int PartId { get; set; }

        public virtual Product Part { get; set; }

        [Required]
        public int FailureId { get; set; }

        public virtual Failure Failure { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal SellPrice { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal BayPrice { get; set; }

        public bool Paid { get; set; }
    }
}
