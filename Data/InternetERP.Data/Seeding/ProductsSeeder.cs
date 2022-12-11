namespace InternetERP.Data.Seeding
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using InternetERP.Data.Models;
    using InternetERP.Data.Seeding.Dtos;
    using Newtonsoft.Json;

    public class ProductsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (!dbContext.Promotions.Any())
            {
                var promotion = new Promotion
                {
                    Name = "Best Selling",
                    Description = "Our best selling products",
                };
                await dbContext.Promotions.AddAsync(promotion);
                await dbContext.SaveChangesAsync();
            }

            if (!dbContext.Products.Any())
            {
                var products = JsonConvert.DeserializeObject<ProductDto[]>(File.ReadAllText(@"../../Data/InternetERP.Data/Seeding/Data/Products.json"));

                int promotionId = 1;
                foreach (var product in products)
                {
                    var newProduct = new Product()
                    {
                        Name = product.Name,
                        SellPrice = product.SellPrice,
                        BayPrice = product.BayPrice,
                        StockQuantity = product.StockQuantity,
                        Description = product.Description,
                    };
                    if (product.Promotion)
                    {
                        newProduct.PromotionId = promotionId;
                    }

                    if (product.Images.Count > 0)
                    {
                        foreach (var imageProduct in product.Images)
                        {
                            var image = new Image()
                            {
                                Name = imageProduct.Name,
                                Path = "Products",
                                Extension = "jpg",
                            };
                            newProduct.Images.Add(image);
                        }
                    }

                    await dbContext.Products.AddAsync(newProduct);
                    await dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
