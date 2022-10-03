using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InternetERP.Models
{
    public class StatusFailure
    {
        public StatusFailure()
        {
            this.Failures = new HashSet<Failure>();
        }
        public int Id { get; set; }
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
        [MaxLength(255)]
        public string Description { get; set; }
        public virtual ICollection<Failure> Failures { get; set; }
    }
}
