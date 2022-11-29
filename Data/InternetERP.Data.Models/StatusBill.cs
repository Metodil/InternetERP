namespace InternetERP.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using InternetERP.Data.Common.Models;

    public class StatusBill : BaseDeletableModel<int>
    {
        public StatusBill()
        {
            this.Bills = new HashSet<Bill>();
        }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }

        public virtual ICollection<Bill> Bills { get; set; }
    }
}
