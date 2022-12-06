namespace InternetERP.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using InternetERP.Data.Common.Models;

    public class Promotions : BaseDeletableModel<int>
    {
        public Promotions()
        {
            this.Products = new HashSet<Product>();
        }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
