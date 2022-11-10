namespace InternetERP.Services.Data.Administration
{
    using System;
    using System.Threading.Tasks;

    using InternetERP.Data.Models;
    using InternetERP.Services.Data.Contracts;
    using Microsoft.AspNetCore.Identity;

    public class ProfileService : IProfileService
    {
        private readonly ICustomUsersService customUsersService;
        private readonly UserManager<ApplicationUser> userManager;

        public ProfileService(
            ICustomUsersService customUsersService,
            UserManager<ApplicationUser> userManager)
        {
            this.customUsersService = customUsersService;
            this.userManager = userManager;
        }

        // TODO make one metod for all
        public async Task<string> UpdatePhoneNumber(ApplicationUser user, string phoneNumber)
        {
            var statusMessage = string.Empty;
            var phoneNumberBase = await this.userManager.GetPhoneNumberAsync(user);
            if (phoneNumber != phoneNumberBase)
            {
                var setPhoneResult = await this.userManager.SetPhoneNumberAsync(user, phoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    statusMessage = "Unexpected error when trying to set phone number.";
                }
            }

            return statusMessage;
        }

        public async Task<string> UpdateFirstName(string userId, string firstName)
        {
            var statusMessage = string.Empty;
            var firstNameBase = await this.customUsersService.GetFirstNameAsync(userId);
            if (firstName != firstNameBase)
            {
                var setFirstNameResult = await this.customUsersService.SetFirstNameAsync(userId, firstName);
                if (setFirstNameResult == 0)
                {
                    statusMessage = "Unexpected error when trying to set first name.";
                }
            }

            return statusMessage;
        }

        public async Task<string> UpdateLastName(string userId, string lastName)
        {
            var statusMessage = string.Empty;
            var lastNameBase = await this.customUsersService.GetLastNameAsync(userId);
            if (lastName != lastNameBase)
            {
                var setLastNameResult = await this.customUsersService.SetFirstNameAsync(userId, lastName);
                if (setLastNameResult == 0)
                {
                    statusMessage = "Unexpected error when trying to set last name.";
                }
            }

            return statusMessage;
        }

        public async Task<string> UpdateTownId(string userId, int townId)
        {
            var statusMessage = string.Empty;
            var townIdBase = await this.customUsersService.GetTownIdAsync(userId);
            if (townId == 0)
            {
                statusMessage = "Town is not selected.";
            }
            else if (townId != townIdBase)
            {
                var setTownIdResult = await this.customUsersService.SetTownIdAsync(userId, townId);
                if (setTownIdResult == 0)
                {
                    statusMessage = "Unexpected error when trying to set town.";
                }
            }

            return statusMessage;
        }

        public async Task<string> UpdateDistrictr(string userId, string district)
        {
            var statusMessage = string.Empty;
            var districtBase = await this.customUsersService.GetDistrictAsync(userId);
            if (district != districtBase)
            {
                var setDistrictResult = await this.customUsersService.SetDistrictAsync(userId, district);
                if (setDistrictResult == 0)
                {
                    statusMessage = "Unexpected error when trying to set district.";
                }
            }

            return statusMessage;
        }

        public async Task<string> UpdateStreet(string userId, string street)
        {
            var statusMessage = string.Empty;
            var streetBase = await this.customUsersService.GetStreetAsync(userId);
            if (street != streetBase)
            {
                var setStreetResult = await this.customUsersService.SetStreetAsync(userId, street);
                if (setStreetResult == 0)
                {
                    statusMessage = "Unexpected error when trying to set street.";
                }
            }

            return statusMessage;
        }

        public async Task<string> UpdateNote(string userId, string note)
        {
            var statusMessage = string.Empty;
            var noteBase = await this.customUsersService.GetNoteAsync(userId);
            if (note != noteBase)
            {
                var setNoteResult = await this.customUsersService.SetNoteAsync(userId, note);
                if (setNoteResult == 0)
                {
                    statusMessage = "Unexpected error when trying to set note.";
                }
            }

            return statusMessage;
        }
    }
}
