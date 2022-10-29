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

        public int? ProductId { get; set; }

        public Product Product { get; set; }

        public int? InernetPaymentId { get; set; }

        public InternetPayment InernetPayment { get; set; }

        public int? FailureId { get; set; }

        public Failure Failure { get; set; }

        public int? InvoiceId { get; set; }

        public Invoice Invoice { get; set; }

        public string Note { get; set; }
    }
}
