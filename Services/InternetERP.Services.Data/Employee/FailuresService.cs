namespace InternetERP.Services.Data.Employee
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using InternetERP.Common;
    using InternetERP.Data.Common.Repositories;
    using InternetERP.Data.Models;
    using InternetERP.Services.Data.Contracts;
    using InternetERP.Web.ViewModels.Employee.Failure;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    public class FailuresService : IFailuresService
    {
        private readonly IDeletableEntityRepository<InternetAccount> internetAccountsRepository;
        private readonly IDeletableEntityRepository<Failure> failureRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        private readonly ILogger<FailuresService> logger;

        public FailuresService(
            IDeletableEntityRepository<InternetAccount> internetAccountsRepository,
            IDeletableEntityRepository<Failure> failureRepository,
            IDeletableEntityRepository<ApplicationUser> userRepository,
            ILogger<FailuresService> logger)
        {
            this.internetAccountsRepository = internetAccountsRepository;
            this.failureRepository = failureRepository;
            this.userRepository = userRepository;
            this.logger = logger;
        }

        public async Task<ICollection<InternetAccount>> GetAllAccounts()
        {
            return await this.internetAccountsRepository
                .AllAsNoTracking()
                .Include(ia => ia.InternetUser)
                .ToListAsync();
        }

        public async Task<List<SelectListItem>> GetAllInternetAcountsList()
        {
            var selectedListItem = new SelectListItem
            {
                Value = string.Empty,
                Text = "Select an account",
            };
            var selectedListItemsFromDb = await this.internetAccountsRepository
                .AllAsNoTracking()
                .Include(ia => ia.InternetUser)
                .Select(ia => new SelectListItem
                {
                    Value = ia.Id.ToString(),
                    Text = ia.InternetUser.FirstName + " " + ia.InternetUser.LastName,
                })
                .ToListAsync();
            var selectedListItems = new List<SelectListItem>();
            selectedListItems.Add(selectedListItem);
            selectedListItems.AddRange(selectedListItemsFromDb);
            return selectedListItems;
        }

        public async Task<bool> CreateFailure(string createUserId, FailureInputModel input)
        {
            var result = true;
            var newFailure = new Failure
            {
                ShortDescription = input.ShortDescription,
                Note = input.Note,
                AccountId = input.SelectedAccountId,
                CreateUserId = createUserId,
                StatusFailureId = GlobalConstants.FailureNewStatusId,
            };
            try
            {
                await this.failureRepository.AddAsync(newFailure);
                await this.failureRepository.SaveChangesAsync();
            }
            catch (System.Exception ex)
            {
                this.logger.LogError(ex.Message);
                result = false;
            }

            return result;
        }

        public async Task<ApplicationUser> GetInternetUserById(int selectedAccountId)
        {
            var applicationUserId = await this.internetAccountsRepository
                .AllAsNoTracking()
                .Where(u => u.Id == selectedAccountId)
                .Select(u => u.InternetUserId)
                .FirstAsync();

            return await this.userRepository
                .AllAsNoTracking()
                .Include(u => u.Town)
                .Where(u => u.Id == applicationUserId)
                .FirstAsync();
        }
    }
}
