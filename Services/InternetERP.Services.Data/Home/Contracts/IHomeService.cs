namespace InternetERP.Services.Data.Home.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using InternetERP.Data.Models;

    public interface IHomeService
    {
        Task<ICollection<Product>> GetPromoProducts();
    }
}
