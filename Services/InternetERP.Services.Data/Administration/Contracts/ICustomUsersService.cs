namespace InternetERP.Services.Data.Administration.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using InternetERP.Data.Models;

    public interface ICustomUsersService
    {
        public Task<string> GetFirstNameAsync(string id);

        public Task<string> GetLastNameAsync(string id);

        public Task<string> GetDistrictAsync(string id);

        public Task<int> GetTownIdAsync(string id);

        public Task<string> GetStreetAsync(string id);

        public Task<string> GetNoteAsync(string id);

        public Task<int> SetFirstNameAsync(string id, string firstName);

        public Task<int> SetLastNameAsync(string id, string lastName);

        public Task<int> SetTownIdAsync(string id, int townId);

        public Task<int> SetDistrictAsync(string id, string lastName);

        public Task<int> SetStreetAsync(string id, string lastName);

        public Task<int> SetNoteAsync(string id, string lastName);

        // TODO remove trainerID
        public Task<int> CountAsync();

        public Task<ApplicationUser> SetEmailAsync(string userId, string newEmail);

        public Task<IEnumerable<string>> GetAllEmailsAsync();

        public Task<IEnumerable<T>> GetAllUsersPagingAsync<T>(int page, int itemsPerPage);

        public Task<IEnumerable<T>> GetAllUsersAsync<T>();

        public Task<IEnumerable<string>> GetUserRolesNameAsync(string userId);

        public Task<T> GetUserByIdAsync<T>(string id);

        public Task<ApplicationUser> GetUserByIdAsync(string id);

        public bool CheckForEmailsAsync(string email);

        Task<ICollection<ApplicationUser>> GetLatestAddedUsersAsync();
    }
}
