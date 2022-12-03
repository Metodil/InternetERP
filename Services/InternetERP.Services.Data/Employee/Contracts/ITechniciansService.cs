namespace InternetERP.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using InternetERP.Web.ViewModels.Employee.Technicians;

    public interface ITechniciansService
    {
        Task ChangeFailureStatus(FailureEditInputModel input, string createUserId, int statusFailureId);

        Task<int> CountAsync(int filterByStatus);

        Task<ICollection<T>> GetAllFailuresAsync<T>(int page, int itemsPerPage, int filterByStatus);

        Task<T> GetFailuresByIdAsync<T>(int failureId);

        Task SaveFailure(FailureEditInputModel input);
    }
}
