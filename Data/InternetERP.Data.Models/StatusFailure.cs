#nullable disable

namespace InternetERP.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using InternetERP.Data.Common.Models;

    public class StatusFailure : BaseDeletableModel<int>
    {
        public StatusFailure()
        {
            this.Failures = new HashSet<Failure>();
        }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }

        public virtual ICollection<Failure> Failures { get; set; }
    }
}
