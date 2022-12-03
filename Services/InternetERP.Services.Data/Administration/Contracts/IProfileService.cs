namespace InternetERP.Services.Data.Administration.Contracts
{
    using System.Threading.Tasks;

    using InternetERP.Data.Models;

    public interface IProfileService
    {
        public Task<string> UpdatePhoneNumber(ApplicationUser user, string phoneNumber);

        public Task<string> UpdateFirstName(string userId, string firstName);

        public Task<string> UpdateLastName(string userId, string lastName);

        public Task<string> UpdateTownId(string userId, int townId);

        public Task<string> UpdateDistrictr(string userId, string district);

        public Task<string> UpdateStreet(string userId, string street);

        public Task<string> UpdateNote(string userId, string note);
    }
}
