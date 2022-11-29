#nullable disable

namespace InternetERP.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using InternetERP.Data.Common.Models;
    using Microsoft.EntityFrameworkCore.Metadata.Internal;

    public class InternetAccountType : BaseDeletableModel<int>
    {
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal MontlyPrice { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }
    }
}
