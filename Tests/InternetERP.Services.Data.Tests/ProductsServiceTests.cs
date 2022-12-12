namespace InternetERP.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using InternetERP.Common;
    using InternetERP.Data;
    using InternetERP.Data.Models;
    using InternetERP.Data.Repositories;
    using InternetERP.Services.Data.Contracts;
    using InternetERP.Services.Data.Employee;
    using InternetERP.Services.Mapping;
    using InternetERP.Web.ViewModels.Employee.Manager;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Internal;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using Moq;
    using Xunit;

    public class ProductsServiceTests
    {
        [Fact]
        public async Task GetFilteredProductsPagingAsyncProperlyWhenFilterIsNotSet()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                      .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var db = new ApplicationDbContext(options);
            var productRepository = new EfDeletableEntityRepository<Product>(db);
            var imagesRepository = new EfRepository<Image>(db);
            var fileService = new Mock<IFileService>();
            var environment = new Mock<IHostingEnvironment>();
            var logger = new Mock<ILogger<ProductsService>>();
            new MapperInitializationProfile();

            var service = new ProductsService(
                productRepository,
                imagesRepository,
                fileService.Object,
                environment.Object,
                logger.Object);
            await productRepository.AddAsync(this.NewProduct());
            await productRepository.AddAsync(this.NewProduct());
            await productRepository.AddAsync(this.NewProduct());
            await productRepository.AddAsync(this.NewProduct());
            await productRepository.AddAsync(this.NewProduct());
            await productRepository.AddAsync(this.NewProduct());
            await productRepository.SaveChangesAsync();

            var page = 1;
            var itemsPerPage = 5;
            var result = service.GetFilteredProductsPagingAsync<ProductsViewModelTest>(
                page,
                itemsPerPage);

            Assert.Equal(5, result.Result.Count);
        }

        [Fact]
        public async Task GetFilteredProductsPagingAsyncProperlyWhenFilterIsSet()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                      .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var db = new ApplicationDbContext(options);
            var productRepository = new EfDeletableEntityRepository<Product>(db);
            var imagesRepository = new EfRepository<Image>(db);
            var fileService = new Mock<IFileService>();
            var environment = new Mock<IHostingEnvironment>();
            var logger = new Mock<ILogger<ProductsService>>();
            new MapperInitializationProfile();

            var service = new ProductsService(
                productRepository,
                imagesRepository,
                fileService.Object,
                environment.Object,
                logger.Object);
            await productRepository.AddAsync(this.NewProduct());
            await productRepository.AddAsync(this.NewProduct());
            var newProduct = this.NewProduct();
            newProduct.Name = "Test name";
            await productRepository.AddAsync(newProduct);
            newProduct = this.NewProduct();
            newProduct.Name = "Test name";
            await productRepository.AddAsync(newProduct);
            await productRepository.AddAsync(this.NewProduct());
            await productRepository.AddAsync(this.NewProduct());
            await productRepository.SaveChangesAsync();

            var page = 1;
            var itemsPerPage = 5;
            var filterName = "Test name";
            var result = service.GetFilteredProductsPagingAsync<ProductsViewModelTest>(
                page,
                itemsPerPage,
                filterName);

            Assert.Equal(2, result.Result.Count);
        }

        [Fact]
        public async Task CountAsyncGetCountWhenFilterByStatusIsNotSet()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                     .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var db = new ApplicationDbContext(options);
            var productRepository = new EfDeletableEntityRepository<Product>(db);
            var imagesRepository = new EfRepository<Image>(db);
            var fileService = new Mock<IFileService>();
            var environment = new Mock<IHostingEnvironment>();
            var logger = new Mock<ILogger<ProductsService>>();
            new MapperInitializationProfile();

            var service = new ProductsService(
                productRepository,
                imagesRepository,
                fileService.Object,
                environment.Object,
                logger.Object);
            await productRepository.AddAsync(this.NewProduct());
            await productRepository.AddAsync(this.NewProduct());
            await productRepository.AddAsync(this.NewProduct());
            await productRepository.AddAsync(this.NewProduct());
            await productRepository.SaveChangesAsync();
            var result = service.CountAsync();

            Assert.Equal(4, result.Result);
        }

        [Fact]
        public async Task CountAsyncGetCountWhenFilterByStatusIsSet()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                     .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var db = new ApplicationDbContext(options);
            var productRepository = new EfDeletableEntityRepository<Product>(db);
            var imagesRepository = new EfRepository<Image>(db);
            var fileService = new Mock<IFileService>();
            var environment = new Mock<IHostingEnvironment>();
            var logger = new Mock<ILogger<ProductsService>>();
            new MapperInitializationProfile();

            var service = new ProductsService(
                productRepository,
                imagesRepository,
                fileService.Object,
                environment.Object,
                logger.Object);
            await productRepository.AddAsync(this.NewProduct());
            await productRepository.AddAsync(this.NewProduct());
            var newProduct = this.NewProduct();
            newProduct.Name = "Test name";
            await productRepository.AddAsync(newProduct);
            newProduct = this.NewProduct();
            newProduct.Name = "Test name";
            await productRepository.AddAsync(newProduct);
            await productRepository.AddAsync(this.NewProduct());
            await productRepository.AddAsync(this.NewProduct());
            await productRepository.SaveChangesAsync();
            var filterBy = "Test name";
            var result = service.CountAsync(filterBy);

            Assert.Equal(2, result.Result);
        }

        [Fact]
        public async Task GetFailuresByIdAsyncProperlyReturnFailure()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                     .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var db = new ApplicationDbContext(options);
            var productRepository = new EfDeletableEntityRepository<Product>(db);
            var imagesRepository = new EfRepository<Image>(db);
            var fileService = new Mock<IFileService>();
            var environment = new Mock<IHostingEnvironment>();
            var logger = new Mock<ILogger<ProductsService>>();
            new MapperInitializationProfile();

            var service = new ProductsService(
                productRepository,
                imagesRepository,
                fileService.Object,
                environment.Object,
                logger.Object);

            var productId = 2;
            var newProuct = new Product
            {
                Name = "ProductId 2",
                Id = productId,
            };
            await productRepository.AddAsync(newProuct);
            await productRepository.SaveChangesAsync();

            var result = service.GetProductByIdAsync<ProductsViewModelTest>(productId);

            Assert.Equal(productId, result.Result.Id);
        }

        [Fact]
        public async Task ProductExistProperlyReturnTrueIfExist()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                     .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var db = new ApplicationDbContext(options);
            var productRepository = new EfDeletableEntityRepository<Product>(db);
            var imagesRepository = new EfRepository<Image>(db);
            var fileService = new Mock<IFileService>();
            var environment = new Mock<IHostingEnvironment>();
            var logger = new Mock<ILogger<ProductsService>>();
            new MapperInitializationProfile();

            var service = new ProductsService(
                productRepository,
                imagesRepository,
                fileService.Object,
                environment.Object,
                logger.Object);

            var productId = 2;
            var newProuct = new Product
            {
                Name = "ProductId 2",
                Id = productId,
            };
            await productRepository.AddAsync(newProuct);
            await productRepository.SaveChangesAsync();

            var result = service.ProductExist(productId);

            Assert.True(result.Result);
        }

        [Fact]
        public async Task ProductExistProperlyReturnFalseIfNotExist()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                     .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var db = new ApplicationDbContext(options);
            var productRepository = new EfDeletableEntityRepository<Product>(db);
            var imagesRepository = new EfRepository<Image>(db);
            var fileService = new Mock<IFileService>();
            var environment = new Mock<IHostingEnvironment>();
            var logger = new Mock<ILogger<ProductsService>>();
            new MapperInitializationProfile();

            var service = new ProductsService(
                productRepository,
                imagesRepository,
                fileService.Object,
                environment.Object,
                logger.Object);

            var productId = 2;
            var newProuct = new Product
            {
                Name = "ProductId 2",
                Id = productId,
            };
            await productRepository.AddAsync(newProuct);
            await productRepository.SaveChangesAsync();

            var productIdNotExist = 3;
            var result = service.ProductExist(productIdNotExist);

            Assert.False(result.Result);
        }

        [Fact]
        public async Task CreateAsyncProperlyCreateProduct()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                     .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var db = new ApplicationDbContext(options);
            var productRepository = new EfDeletableEntityRepository<Product>(db);
            var imagesRepository = new EfRepository<Image>(db);
            var fileService = new Mock<IFileService>();
            var environment = new Mock<IHostingEnvironment>();
            var logger = new Mock<ILogger<ProductsService>>();
            new MapperInitializationProfile();

            var service = new ProductsService(
                productRepository,
                imagesRepository,
                fileService.Object,
                environment.Object,
                logger.Object);

            var newProuct = new ProductInputModelView
            {
                Name = "Name of Product",
                SellPrice = 20m,
                BayPrice = 15m,
                StockQuantity = 1,
                Description = "Description",
            };

            var result = await service.CreateAsync(newProuct);

            Assert.True(result);

            var createdProduct = productRepository
                .AllAsNoTracking()
                .FirstOrDefaultAsync();

            Assert.NotNull(createdProduct);
            var name = "Name of Product";
            var sellPrice = 20m;
            Assert.Equal(name, createdProduct.Result.Name);
            Assert.Equal(sellPrice, createdProduct.Result.SellPrice);
        }

        [Fact]
        public async Task UpdateAsyncProperlyUpdateProduct()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                     .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var db = new ApplicationDbContext(options);
            var productRepository = new EfDeletableEntityRepository<Product>(db);
            var imagesRepository = new EfRepository<Image>(db);
            var fileService = new Mock<IFileService>();
            var environment = new Mock<IHostingEnvironment>();
            var logger = new Mock<ILogger<ProductsService>>();
            new MapperInitializationProfile();

            var service = new ProductsService(
                productRepository,
                imagesRepository,
                fileService.Object,
                environment.Object,
                logger.Object);

            var newProuct = new Product
            {
                Id = 1,
                Name = "Name of Product",
                SellPrice = 20m,
                BayPrice = 15m,
                StockQuantity = 1,
                Description = "Description",
            };
            await productRepository.AddAsync(newProuct);
            await productRepository.SaveChangesAsync();

            var updatedProuct = new ProductInputModelView
            {
                Id = 1,
                Name = "Updated Name of Product",
                SellPrice = 40m,
                BayPrice = 35m,
                StockQuantity = 2,
                Description = "Updated Description",
            };

            var result = await service.UpdateAsync(updatedProuct);

            Assert.True(result);

            var createdProduct = productRepository
                .AllAsNoTracking()
                .FirstOrDefaultAsync();

            Assert.NotNull(createdProduct);
            var name = "Updated Name of Product";
            var sellPrice = 40m;
            Assert.Equal(name, createdProduct.Result.Name);
            Assert.Equal(sellPrice, createdProduct.Result.SellPrice);
        }

        [Fact]
        public async Task DeleteProductImageReturnFalseWhenImageNotFound()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                      .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var db = new ApplicationDbContext(options);
            var productRepository = new EfDeletableEntityRepository<Product>(db);
            var imagesRepository = new EfRepository<Image>(db);
            var fileService = new Mock<IFileService>();
            var environment = new Mock<IHostingEnvironment>();
            var logger = new Mock<ILogger<ProductsService>>();
            new MapperInitializationProfile();

            var service = new ProductsService(
                productRepository,
                imagesRepository,
                fileService.Object,
                environment.Object,
                logger.Object);
            await productRepository.SaveChangesAsync();

            var id = "Not found";
            var result = await service.DeleteProductImage(id);

            Assert.False(result);
        }

        [Fact]
        public async Task DeleteProductImageReturnTrueWhenImageIsDeleted()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                      .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var db = new ApplicationDbContext(options);
            var productRepository = new EfDeletableEntityRepository<Product>(db);
            var imagesRepository = new EfRepository<Image>(db);
            var fileService = new Mock<IFileService>();
            var environment = new Mock<IHostingEnvironment>();
            environment
                .Setup(m => m.WebRootPath)
                .Returns("WebRootPath:WebRootPathEnvironment");
            var logger = new Mock<ILogger<ProductsService>>();
            new MapperInitializationProfile();

            var service = new ProductsService(
                productRepository,
                imagesRepository,
                fileService.Object,
                environment.Object,
                logger.Object);
            var productId = Guid.NewGuid().ToString();
            var newImage = new Image
            {
                Id = productId,
                Path = "Path",
            };
            await imagesRepository.AddAsync(newImage);
            await imagesRepository.SaveChangesAsync();

            var result = await service.DeleteProductImage(productId);

            Assert.True(result);
        }

        [Fact]
        public async Task CreateImageUrlListReturnDictionary()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                      .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var db = new ApplicationDbContext(options);
            var productRepository = new EfDeletableEntityRepository<Product>(db);
            var imagesRepository = new EfRepository<Image>(db);
            var fileService = new Mock<IFileService>();
            var environment = new Mock<IHostingEnvironment>();
            environment
                .Setup(m => m.WebRootPath)
                .Returns("WebRootPath:WebRootPathEnvironment");
            var logger = new Mock<ILogger<ProductsService>>();
            new MapperInitializationProfile();

            var service = new ProductsService(
                productRepository,
                imagesRepository,
                fileService.Object,
                environment.Object,
                logger.Object);
            var productId = Guid.NewGuid().ToString();
            var newImage = new Image
            {
                Id = productId,
                Path = "Path",
                Name = "Name",
                Extension = "Extension",

            };
            var listImages = new List<Image>();
            listImages.Add(newImage);

            var result = service.CreateImageUrlList(listImages).FirstOrDefault();

            Assert.Equal(productId, result.Key);
            Assert.Equal("/images/Path/Name", result.Value);
        }

        [Fact]
        public async Task DeleteProperlyDeleteProduct()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                     .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var db = new ApplicationDbContext(options);
            var productRepository = new EfDeletableEntityRepository<Product>(db);
            var imagesRepository = new EfRepository<Image>(db);
            var fileService = new Mock<IFileService>();
            var environment = new Mock<IHostingEnvironment>();
            environment
                .Setup(m => m.WebRootPath)
                .Returns("WebRootPath:WebRootPathEnvironment");
            var logger = new Mock<ILogger<ProductsService>>();
            new MapperInitializationProfile();

            var service = new ProductsService(
                productRepository,
                imagesRepository,
                fileService.Object,
                environment.Object,
                logger.Object);
            var productId = 1;
            var newProuct = new Product
            {
                Id = productId,
                Name = "Name of Product",
                SellPrice = 20m,
                BayPrice = 15m,
                StockQuantity = 1,
                Description = "Description",
            };
            await productRepository.AddAsync(newProuct);
            await productRepository.SaveChangesAsync();
            var result = await service.Delete(productId);

            Assert.True(result);

            var productCount = await productRepository
                .AllAsNoTracking()
                .CountAsync();

            Assert.Equal(0, productCount);
        }

        [Fact]
        public async Task DeleteReturnFalseWhenIdNotExist()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                     .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var db = new ApplicationDbContext(options);
            var productRepository = new EfDeletableEntityRepository<Product>(db);
            var imagesRepository = new EfRepository<Image>(db);
            var fileService = new Mock<IFileService>();
            var environment = new Mock<IHostingEnvironment>();
            environment
                .Setup(m => m.WebRootPath)
                .Returns("WebRootPath:WebRootPathEnvironment");
            var logger = new Mock<ILogger<ProductsService>>();
            new MapperInitializationProfile();

            var service = new ProductsService(
                productRepository,
                imagesRepository,
                fileService.Object,
                environment.Object,
                logger.Object);
            var productId = 1;
            var newProuct = new Product
            {
                Id = productId,
                Name = "Name of Product",
                SellPrice = 20m,
                BayPrice = 15m,
                StockQuantity = 1,
                Description = "Description",
            };
            await productRepository.AddAsync(newProuct);
            await productRepository.SaveChangesAsync();

            var productIdNotExist = 3;
            var result = await service.Delete(productIdNotExist);

            Assert.False(result);

            var productCount = await productRepository
                .AllAsNoTracking()
                .CountAsync();

            Assert.Equal(1, productCount);
        }

        [Fact]
        public async Task AddImageToProductReturnEmptyStringWhenImageIsAddProperly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                     .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var db = new ApplicationDbContext(options);
            var productRepository = new EfDeletableEntityRepository<Product>(db);
            var imagesRepository = new EfRepository<Image>(db);
            var fileService = new Mock<IFileService>();
            var environment = new Mock<IHostingEnvironment>();
            environment
                .Setup(m => m.WebRootPath)
                .Returns("WebRootPath:WebRootPathEnvironment");
            var logger = new Mock<ILogger<ProductsService>>();
            new MapperInitializationProfile();

            var service = new ProductsService(
                productRepository,
                imagesRepository,
                fileService.Object,
                environment.Object,
                logger.Object);
            var productId = 1;
            var newProuct = new Product
            {
                Id = productId,
                Name = "Name of Product",
                SellPrice = 20m,
                BayPrice = 15m,
                StockQuantity = 1,
                Description = "Description",
            };
            await productRepository.AddAsync(newProuct);
            await productRepository.SaveChangesAsync();

            var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            IFormFile fileMock = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", GlobalConstants.DummyTestImage);
            var updateProduct = new ProductInputModelView
            {
                Id = productId,
                ImageUpload = fileMock,
            };
            var result = await service.AddImageToProduct(newProuct, updateProduct);

            Assert.Equal(string.Empty, result);
        }

        public Product NewProduct()
        {
            var createUserId = Guid.NewGuid().ToString();
            return new Product
            {
                Name = "Name of Product",
                SellPrice = 20m,
                BayPrice = 15m,
                StockQuantity = 1,
                Description = "Description",
            };
        }

        public class ProductsViewModelTest : IMapFrom<Product>
        {
            public int Id { get; set; }
        }
    }
}
