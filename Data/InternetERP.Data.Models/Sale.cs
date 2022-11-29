#nullable disable

namespace InternetERP.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using InternetERP.Data.Common.Models;
    using Microsoft.EntityFrameworkCore.Metadata.Internal;

    public class Sale : BaseDeletableModel<int>
    {
        [Required]
        [MaxLength(250)]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal SellPrice { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal BayPrice { get; set; }

        [Required]
        public int StockQuantity { get; set; }

        public string BillId { get; set; }

        public Bill Bill { get; set; }

        public int? ProductId { get; set; }

        public Product Product { get; set; }

        #pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        public string? InernetAccountId { get; set; }
        #pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.

        public InternetAccount InternetAccount { get; set; }

        public int? FailureId { get; set; }

        public Failure Failure { get; set; }

        public int? InvoiceId { get; set; }

        public Invoice Invoice { get; set; }

        public string Note { get; set; }
    }
}
