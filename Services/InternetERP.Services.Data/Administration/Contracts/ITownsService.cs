namespace InternetERP.Services.Data.Administration.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using InternetERP.Data.Models;
    using InternetERP.Web.ViewModels.Administration.Towns;

    public interface ITownsService
    {
        public IEnumerable<KeyValuePair<string, string>> GetAllTownsAsKetValuePairs();

        public Task<IEnumerable<T>> GetAllTownsPagingAsync<T>(int page, int itemsPerPage);

        public Task<int> CountAsync();

        public Task<Town> GetTownByIdAsync(int id);

        public Task<bool> Update(TownInputModel town);

        public Task<Town> CreateAsync(TownInputModel town);

        public Task<bool> TownExist(int id);

        public Task<Town> Delete(int id);
    }
}
