namespace InternetERP.Data.Seeding
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using InternetERP.Data.Common.Models;
    using InternetERP.Data.Models;

    public class Promotion : BaseDeletableModel<int>
    {
        public Promotion()
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
