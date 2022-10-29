#nullable disable

namespace InternetERP.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using InternetERP.Data.Common.Models;

    public class InternetAccountType : BaseDeletableModel<int>
    {
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }
    }
}
