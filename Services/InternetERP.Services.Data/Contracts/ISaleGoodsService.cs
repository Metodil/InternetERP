namespace InternetERP.Services.Data.Contracts
{
    using InternetERP.Web.ViewModels.Employee.Sales;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ISaleGoodsService
    {
        Task<int> CountAsync(string filterBy = null);

        Task<IEnumerable<T>> GetAllUsersAsync<T>();

        Task<T> GetCurrentSaleId<T>(string id);

        Task<IEnumerable<T>> GetFilteredUsersPagingAsync<T>(int page, int itemsPerPage, string filterBy = null, string categoryFilter = null);
    }
}
