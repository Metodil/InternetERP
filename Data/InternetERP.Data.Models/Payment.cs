namespace InternetERP.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using InternetERP.Data.Common.Models;
    using Microsoft.EntityFrameworkCore.Metadata.Internal;

    public class Payment : BaseDeletableModel<int>
    {
        [Requared]
        public string BillId { get; set; }

        public Bill Bill { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        [Requared]
        public string Provider { get; set; }
    }
}
