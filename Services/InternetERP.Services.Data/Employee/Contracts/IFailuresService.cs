namespace InternetERP.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using InternetERP.Data.Models;
    using InternetERP.Web.ViewModels.Employee.Failure;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public interface IFailuresService
    {
        Task<bool> CreateFailure(string createUserId, FailureInputModel input);

        Task<ICollection<InternetAccount>> GetAllAccounts();

        Task<List<SelectListItem>> GetAllInternetAcountsList();
        Task<int> GetInternetIdByUserId(string userId);
        Task<ApplicationUser> GetInternetUserById(int selectedAccountId);
    }
}
