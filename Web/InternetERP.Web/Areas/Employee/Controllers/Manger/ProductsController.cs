namespace InternetERP.Web.Areas.Employee.Controllers.Manger
{
    using System.Threading.Tasks;

    using InternetERP.Common;
    using InternetERP.Services.Data.Contracts;
    using InternetERP.Web.ViewModels.Employee.Manager;
    using Microsoft.AspNetCore.Mvc;

    public class ProductsController : EmployeeController
    {
        private readonly IProductsService productsService;

        public ProductsController(
            IProductsService productsService)
        {
            this.productsService = productsService;
        }

        [HttpGet]
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        public async Task<IActionResult> All(int id = 1, string? filterBy = null)
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        {
            var model = new AllProductsViewModel
            {
                Products = await this.productsService.GetFilteredProductsPagingAsync<ProductListModelView>(
                    id, GlobalConstants.ItemsPerPageList, filterBy),
                ItemsPerPage = GlobalConstants.ItemsPerPageList,
                PageNumber = id,
                AspAction = nameof(this.All),
                ItemsCount = await this.productsService.CountAsync(filterBy),
                FilterBy = filterBy,
            };

            return this.View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (await this.productsService.CountAsync() == 0)
            {
                // TODO check not found in controlles
                return this.NotFound();
            }

            var product = await this.productsService.GetProductByIdAsync<ProductInputModelView>(id);
            if (product == null)
            {
                return this.NotFound();
            }

            product.ImageUrlList = this.productsService.CreateImageUrlList(product.Images);
            return this.View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductInputModelView productInput)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(productInput);
            }

            if (!await this.productsService.UpdateAsync(productInput))
            {
                this.ModelState.AddModelError(string.Empty, productInput.Response);
                productInput.Response = string.Empty;
                productInput.ImageUrlList = this.productsService.CreateImageUrlList(productInput.Images);
                return this.View(productInput);
            }
            else
            {
                var productUpdated = await this.productsService
                    .GetProductByIdAsync<ProductInputModelView>(productInput.Id);
                productUpdated.ImageUrlList = this.productsService.CreateImageUrlList(productUpdated.Images);
                return this.View(productUpdated);
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeleteProductImage(string imageId, int productId)
        {
            await this.productsService.DeleteProductImage(imageId);
            var productForUpdate = await this.productsService
                .GetProductByIdAsync<ProductInputModelView>(productId);
            productForUpdate.ImageUrlList = this.productsService.CreateImageUrlList(productForUpdate.Images);
            return this.RedirectToAction("Edit", productForUpdate);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var newProduct = new ProductInputModelView();
            return this.View(newProduct);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductInputModelView productInput)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(productInput);
            }

            if (!await this.productsService.CreateAsync(productInput))
            {
                this.ModelState.AddModelError(string.Empty, productInput.Response);
                productInput.Response = string.Empty;
                return this.View(productInput);
            }
            else
            {
                var model = new AllProductsViewModel
                {
                    Products = await this.productsService.GetFilteredProductsPagingAsync<ProductListModelView>(
                        1, GlobalConstants.ItemsPerPageList, productInput.Name),
                    ItemsPerPage = GlobalConstants.ItemsPerPageList,
                    PageNumber = 1,
                    AspAction = nameof(this.All),
                    ItemsCount = await this.productsService.CountAsync(productInput.Name),
                    FilterBy = productInput.Name,
                };

                return this.RedirectToAction("All", model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(int productId)
        {
            var product = await this.productsService
                .GetProductByIdAsync<ProductInputModelView>(productId);
            product.ImageUrlList = this.productsService.CreateImageUrlList(product.Images);
            return this.View(product);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var productForDelete = await this.productsService
                .GetProductByIdAsync<ProductInputModelView>(id);
            productForDelete.ImageUrlList = this.productsService.CreateImageUrlList(productForDelete.Images);
            return this.View(productForDelete);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await this.productsService.Delete(id);
            var model = new AllProductsViewModel
            {
                Products = await this.productsService.GetFilteredProductsPagingAsync<ProductListModelView>(
                     1, GlobalConstants.ItemsPerPageList),
                ItemsPerPage = GlobalConstants.ItemsPerPageList,
                PageNumber = 1,
                AspAction = nameof(this.All),
                ItemsCount = await this.productsService.CountAsync(),
            };

            return this.RedirectToAction("All", model);
        }
    }
}
