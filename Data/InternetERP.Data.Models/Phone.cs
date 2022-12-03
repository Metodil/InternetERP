#nullable disable

namespace InternetERP.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using InternetERP.Data.Common.Models;

    public class Phone : BaseDeletableModel<int>
    {
        [Required]
        [MaxLength(30)]
        public string PhoneNumber { get; set; }

        public string Description { get; set; }

        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
