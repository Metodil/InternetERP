namespace InternetERP.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using InternetERP.Web.ViewModels.Employee.Technicians;

    public interface ITechniciansService
    {
        Task<int> CountAsync(string filterByStatus);

        Task<ICollection<T>> GetAllFailuresAsync<T>(int page, int itemsPerPage, string filterByStatus);
    }
}
