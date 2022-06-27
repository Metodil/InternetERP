using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InternetERP.Models
{
    public class Part
    {
        public Part()
        {
            this.FailuresParts = new HashSet<FailurePart>();
        }
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        public string Description { get; set; }
        [Required]
        public int StockQuantity { get; set; }
        public virtual ICollection<FailurePart> FailuresParts { get; set; }
    }
}
