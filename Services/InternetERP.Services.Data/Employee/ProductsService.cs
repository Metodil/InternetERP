namespace InternetERP.Services.Data.Employee
{
    using System;
#nullable disable
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using InternetERP.Common;
    using InternetERP.Data.Common.Repositories;
    using InternetERP.Data.Models;
    using InternetERP.Services.Data.Contracts;
    using InternetERP.Services.Mapping;
    using InternetERP.Web.ViewModels.Employee.Manager;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    public class ProductsService : IProductsService
    {
        private readonly IDeletableEntityRepository<Product> productsRepository;
        private readonly IRepository<Image> imagesRepository;
        private readonly IFileService fileService;
        private readonly IHostingEnvironment environment;
        private readonly ILogger<ProductsService> logger;
        private readonly string[] allowedExtensions = new[] { "jpg", "png" };

        public ProductsService(
            IDeletableEntityRepository<Product> productsRepository,
            IRepository<Image> imagesRepository,
            IFileService fileService,
            IHostingEnvironment environment,
            ILogger<ProductsService> logger)
        {
            this.productsRepository = productsRepository;
            this.imagesRepository = imagesRepository;
            this.fileService = fileService;
            this.environment = environment;
            this.logger = logger;
        }
#nullable enable

        public async Task<ICollection<T>> GetFilteredProductsPagingAsync<T>(
            int page,
            int itemsPerPage,
            string? filterBy = null,
            string? categoryFilter = null)
        {
            if (filterBy == null)
            {
                return await this.productsRepository
                                .All()
                                .OrderBy(x => x.Name)
                                .Skip((page - 1) * itemsPerPage)
                                .Take(itemsPerPage)
                                .To<T>()
                                .ToListAsync();
            }
            else
            {
                var filterList = filterBy
                    .Split(' ', System.StringSplitOptions.RemoveEmptyEntries)
                    .ToList();
                return await this.productsRepository
                                .All()
                                .OrderBy(x => x.Name)
                                .Where(p => p.Name.Contains(filterBy))
                                .Skip((page - 1) * itemsPerPage)
                                .Take(itemsPerPage)
                                .To<T>()
                                .ToListAsync();
            }
        }

        public async Task<int> CountAsync(string? filterBy = null)
        {
            if (filterBy == null)
            {
                return await this.productsRepository
                                .All()
                                .CountAsync();
            }

            return await this.productsRepository
                                .All()
                                .Where(p => p.Name.Contains(filterBy))
                                .CountAsync();
        }

        public async Task<T> GetProductByIdAsync<T>(int id)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await this.productsRepository
                .AllWithDeleted()
                .Where(p => p.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();
#pragma warning restore CS8603 // Possible null reference return.
        }

        public async Task<bool> ProductExist(int id)
        {
            return await this.productsRepository
                .AllAsNoTrackingWithDeleted()
                .AnyAsync(t => t.Id == id);
        }

        public async Task<bool> CreateAsync(ProductInputModelView productInput)
        {
            var result = true;
            var response = string.Empty;
            var newProduct = new Product
            {
                Name = productInput.Name,
                SellPrice = productInput.SellPrice,
                BayPrice = productInput.BayPrice,
                StockQuantity = productInput.StockQuantity,
                Description = productInput.Description,
            };

            var uploadResponce = await this.AddImageToProduct(newProduct, productInput);
            if (!string.IsNullOrEmpty(uploadResponce))
            {
                response += uploadResponce;
                result = false;
            }

            if (result)
            {
                try
                {
                    await this.productsRepository.AddAsync(newProduct);
                    await this.productsRepository.SaveChangesAsync();
                    response = Environment.NewLine + "Product is created successful.";
                }
                catch (DbUpdateConcurrencyException)
                {
                    response = Environment.NewLine + "Error on creating product.";
                    result = false;
                }
            }

            productInput.Response = response;
            return result;
        }

        public async Task<bool> UpdateAsync(ProductInputModelView productInput)
        {
            var response = string.Empty;
            var result = true;
            var productForUpdate = await this.productsRepository
                .All()
                .Where(p => p.Id == productInput.Id)
                .FirstAsync();
            productForUpdate.Name = productInput.Name;
            productForUpdate.SellPrice = productInput.SellPrice;
            productForUpdate.BayPrice = productInput.BayPrice;
            productForUpdate.StockQuantity = productInput.StockQuantity;
            productForUpdate.Description = productInput.Description;

            var uploadResponce = await this.AddImageToProduct(productForUpdate, productInput);
            if (!string.IsNullOrEmpty(uploadResponce))
            {
                response += uploadResponce;
                result = false;
            }

            if (result)
            {
                try
                {
                    this.productsRepository.Update(productForUpdate);
                    await this.productsRepository.SaveChangesAsync();
                    response = Environment.NewLine + "Product is updated successful.";
                }
                catch (DbUpdateConcurrencyException duce)
                {
                    this.logger.LogError(productInput.Id + " " + productInput.Name + " " + duce.Message);
                    response = Environment.NewLine + "Error on updating product.";
                    result = false;
                }
            }

            productInput.Response = response;
            return result;
        }

        public async Task<bool> DeleteProductImage(string imageId)
        {
            var result = true;
            var image = await this.imagesRepository
                .All()
                .FirstOrDefaultAsync(i => i.Id == imageId);
            if (image != null)
            {
                var path = Path.GetFullPath(Path.Combine(
                    this.environment.WebRootPath,
                    GlobalConstants.RootPathForImages,
                    GlobalConstants.ProductsPathForImages));

                var imageName = string.Empty;
                if (image.Name != null)
                {
                    imageName = $"{image.Name}";
                }
                else
                {
                    imageName = $"{image.Id}.{image.Extension}";
                }

                this.imagesRepository.Delete(image);
                await this.imagesRepository.SaveChangesAsync();
                await this.fileService.DeleteImageProduct(imageName, path);
            }
            else
            {
                result = false;
            }

            return result;
        }

        public async Task<bool> Delete(int id)
        {
            var result = true;
            var product = this.productsRepository.All().FirstOrDefault(x => x.Id == id);
            if (product != null)
            {
                try
                {
                    var imagesForDelete = await this.imagesRepository.All().Where(i => i.ProductId == id).ToListAsync();
                    var path = Path.GetFullPath(Path.Combine(
                        this.environment.WebRootPath,
                        GlobalConstants.RootPathForImages,
                        GlobalConstants.ProductsPathForImages));
                    var imageName = string.Empty;
                    foreach (var image in imagesForDelete)
                    {
                        if (image.Name != null)
                        {
                            imageName = $"{image.Name}";
                        }
                        else
                        {
                            imageName = $"{image.Id}.{image.Extension}";
                        }

                        await this.fileService.DeleteImageProduct(imageName, path);
                        this.imagesRepository.Delete(image);
                    }

                    await this.imagesRepository.SaveChangesAsync();
                    this.productsRepository.Delete(product);
                    await this.imagesRepository.SaveChangesAsync();
                }
                catch (Exception)
                {
                    result = false;
                }
            }
            else
            {
                result = false;
            }

            return result;
        }

        public Dictionary<string, string> CreateImageUrlList(ICollection<Image> images)
        {
            var imageUrlList = new Dictionary<string, string>();
            foreach (var image in images)
            {
                imageUrlList.Add(image.Id, "/images/" + image.Path + "/" + (image.Name != null ? image.Name : image.Id + "." + image.Extension));
            }

            return imageUrlList;
        }

        public async Task<string> AddImageToProduct(Product productForUpdate, ProductInputModelView productInput)
        {
            var result = string.Empty;
            if (productInput.ImageUpload != null && productInput.ImageUpload.Length > 0)
            {
                try
                {
                    var extension = Path.GetExtension(productInput.ImageUpload.FileName).TrimStart('.').ToLower();
                    if (!this.allowedExtensions.Any(x => extension.EndsWith(x)))
                    {
                        throw new Exception($"Invalid image extension {extension}");
                    }

                    var image = new Image
                    {
                        @Path = GlobalConstants.ProductsPathForImages,
                        Extension = extension,
                    };
                    productForUpdate.Images.Add(image);
                    await this.productsRepository.SaveChangesAsync();
                    var path = Path.GetFullPath(Path.Combine(
                        this.environment.WebRootPath,
                        GlobalConstants.RootPathForImages,
                        GlobalConstants.ProductsPathForImages));
                    var imageName = $"{image.Id}.{extension}";
                    if (productInput.ImageUpload.FileName != GlobalConstants.DummyTestImage)
                    {
                        if (!await this.fileService.UploadFile(imageName, path, productInput.ImageUpload))
                        {
                            result = "File Upload Failed";
                        }
                    }
                }
                catch (Exception ex)
                {
                    result = $"File Upload Failed: {ex.Message}";
                }
            }

            return result;
        }
    }
}
