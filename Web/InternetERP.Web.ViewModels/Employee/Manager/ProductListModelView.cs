namespace InternetERP.Web.ViewModels.Employee.Manager
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using InternetERP.Data.Models;
    using InternetERP.Services.Mapping;
    using Microsoft.AspNetCore.Http;

    public class ProductListModelView : IMapFrom<Product>
    {
        public ProductListModelView()
        {
            this.Images = new HashSet<Image>();
        }

        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public decimal SellPrice { get; set; }

        public decimal BayPrice { get; set; }

        public int StockQuantity { get; set; }

        public string Description { get; set; } = string.Empty;

        public virtual ICollection<Image> Images { get; set; }
    }
}
