#nullable disable

namespace InternetERP.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using InternetERP.Data.Common.Models;

    public class JobTitle : BaseDeletableModel<int>
    {
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
