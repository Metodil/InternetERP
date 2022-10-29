#nullable disable

namespace InternetERP.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using InternetERP.Data.Common.Models;

    public class PaymentType : BaseDeletableModel<int>
    {
        [Required]
        [MaxLength(30)]
        public string Type { get; set; }

        public string Description { get; set; }
    }
}
