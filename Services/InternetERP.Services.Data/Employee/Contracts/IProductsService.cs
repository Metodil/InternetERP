namespace InternetERP.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using InternetERP.Data.Models;
    using InternetERP.Web.ViewModels.Employee.Manager;

    public interface IProductsService
    {
        Task<int> CountAsync(string filterBy = null);

        Task<bool> CreateAsync(ProductInputModelView productInput);

        Dictionary<string, string> CreateImageUrlList(ICollection<Image> images);

        public Task<bool> Delete(int id);

        Task<bool> DeleteProductImage(string imageId);

        Task<ICollection<T>> GetFilteredProductsPagingAsync<T>(int page, int itemsPerPage, string nameFilter = null, string categoryFilter = null);

        Task<T> GetProductByIdAsync<T>(int id);

        Task<bool> ProductExist(int id);

        Task<bool> UpdateAsync(ProductInputModelView product);
    }
}
