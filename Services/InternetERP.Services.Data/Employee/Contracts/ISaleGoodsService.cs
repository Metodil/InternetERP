namespace InternetERP.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using InternetERP.Data.Models;
    using InternetERP.Web.ViewModels.Employee.Manager;
    using InternetERP.Web.ViewModels.Employee.Sales;

    public interface ISaleGoodsService
    {
        Task<int> CountAsync(string filterBy = null);

        Task<ICollection<T>> GetAllUsersAsync<T>();

        Task<BillInfo> GetBillInfo(string billId);

        Task<T> GetCurrentSaleId<T>(string id);

        Task<Failure> GetFailureById(int id);

        Task<ICollection<Failure>> GetFailures(int internetAccountUserId);

        Task<ICollection<T>> GetFilteredProductsPagingAsync<T>(
            int page,
            int itemsPerPage,
            string filterBy = null,
            string categoryFilter = null);

        Task<InternetAccount> GetInternetAccountInfo();

        Task<ICollection<Sale>> GetSales(string saleId);

        Task<ICollection<InternetAccountType>> GetServices();

        Task<bool> NewSaleId(string saleUserId, string selectedUserId);

        Task<bool> SaleFailureAmount(PayFailureViewModel input);

        Task<bool> SellProduct(AllProductsSalesViewModel input);

        Task<bool> SellService(SaleServicesViewModel input);

        Task UpdateCheckout(CheckoutViewModel input);
    }
}
