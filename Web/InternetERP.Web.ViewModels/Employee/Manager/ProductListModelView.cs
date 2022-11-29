namespace InternetERP.Web.ViewModels.Employee.Manager
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using InternetERP.Data.Models;
    using InternetERP.Services.Mapping;

    public class ProductListModelView : IMapFrom<Product>, IHaveCustomMappings
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

        public string ImageUrl { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Product, ProductInputModelView>()
                .ForMember(x => x.ImageUrl, opt =>
                opt.MapFrom(i =>
                    "/images/" + i.Images.FirstOrDefault().Path + "/" +
                    (i.Images.FirstOrDefault().Name != null ?
                    i.Images.FirstOrDefault().Name :
                    i.Images.FirstOrDefault().Id +
                    "." + i.Images.FirstOrDefault().Extension)));
        }
    }
}
