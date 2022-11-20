namespace InternetERP.Web.ViewModels.Employee.Manager
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using AutoMapper;
    using InternetERP.Common;
    using InternetERP.Data.Models;
    using InternetERP.Services.Mapping;
    using Microsoft.AspNetCore.Http;

    public class ProductInputModelView : IMapFrom<Product>, IHaveCustomMappings
    {
        public ProductInputModelView()
        {
            this.Images = new HashSet<Image>();
            this.ImageUrlList = new Dictionary<string, string>();
        }

        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Product Name")]
        [StringLength(150, ErrorMessage = GlobalConstants.TextError, MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Sell Price")]
        [Range(typeof(decimal), "0.00", "999999.00", ErrorMessage = GlobalConstants.RangeErrorPrice)]
        public decimal SellPrice { get; set; }

        [Required]
        [Display(Name = "Bay Price")]
        [Range(typeof(decimal), "0.00", "999999.00", ErrorMessage = GlobalConstants.RangeErrorPrice)]
        public decimal BayPrice { get; set; }

        [Required]
        [Display(Name = "Quantity")]
        [Range(typeof(int), "0", "999999", ErrorMessage = GlobalConstants.RangeErrorPrice)]
        public int StockQuantity { get; set; }

        [Required]
        public string Description { get; set; } = string.Empty;

        public virtual ICollection<Image> Images { get; set; }

        public string Response { get; set; }

        public string ImageUrl { get; set; }

        public IFormFile ImageUpload { get; set; }

        public virtual Dictionary<string, string> ImageUrlList { get; set; }

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
