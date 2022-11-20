namespace InternetERP.Data.Seeding.Dtos
{
    using System.Collections.Generic;

    using InternetERP.Data.Models;

    public class ProductDto
    {
        public ProductDto()
        {
            this.Images = new HashSet<Image>();
        }

        public string Name { get; set; } = string.Empty;

        public decimal SellPrice { get; set; }

        public decimal BayPrice { get; set; }

        public int StockQuantity { get; set; }

        public string Description { get; set; } = string.Empty;

        public virtual ICollection<Image> Images { get; set; }
    }
}
