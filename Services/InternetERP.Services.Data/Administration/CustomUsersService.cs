namespace InternetERP.Services.Data.Administration
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using InternetERP.Data.Common.Repositories;
    using InternetERP.Data.Models;
    using InternetERP.Services.Data.Contracts;
    using InternetERP.Services.Mapping;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    public class CustomUsersService : ICustomUsersService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        private readonly Microsoft.AspNetCore.Identity.RoleManager<ApplicationRole> roleManager;

        public CustomUsersService(
            IDeletableEntityRepository<ApplicationUser> userRepository,
            RoleManager<ApplicationRole> roleManager)
        {
            this.userRepository = userRepository;
            this.roleManager = roleManager;
        }

        public async Task<ApplicationUser> SetEmailAsync(string userId, string newEmail)
        {
            var user = await this.userRepository
                .All()
                .FirstOrDefaultAsync(x => x.Id == userId);

            user.Email = newEmail;
            await this.userRepository.SaveChangesAsync();

            return user;
        }

        public async Task<int> SetFirstNameAsync(string id, string firstName)
        {
            var user = await this.userRepository
                .All()
                .FirstOrDefaultAsync(x => x.Id == id);

            user.FirstName = firstName;
            return await this.userRepository.SaveChangesAsync();
        }

        public async Task<int> SetLastNameAsync(string id, string lastName)
        {
            var user = await this.userRepository
                .All()
                .FirstOrDefaultAsync(x => x.Id == id);

            user.LastName = lastName;
 
            return await this.userRepository.SaveChangesAsync();
        }

        public async Task<int> SetDistrictAsync(string id, string district)
        {
            var user = await this.userRepository
                .All()
                .FirstOrDefaultAsync(x => x.Id == id);

            user.District = district;

            return await this.userRepository.SaveChangesAsync();
        }

        public async Task<int> SetStreetAsync(string id, string street)
        {
            var user = await this.userRepository
                .All()
                .FirstOrDefaultAsync(x => x.Id == id);

            user.Street = street;

            return await this.userRepository.SaveChangesAsync();
        }

        public async Task<int> SetTownIdAsync(string id, int townId)
        {
            var user = await this.userRepository
                .All()
                .FirstOrDefaultAsync(x => x.Id == id);

            user.TownId = townId;

            return await this.userRepository.SaveChangesAsync();
        }

        public async Task<int> SetNoteAsync(string id, string note)
        {
            var user = await this.userRepository
                .All()
                .FirstOrDefaultAsync(x => x.Id == id);

            user.Note = note;

            return await this.userRepository.SaveChangesAsync();
        }

        public async Task<ApplicationUser> SetProfilePhotoAsync(string userId, string newProfilePhotoUrl)
        {
            var user = await this.userRepository
                .All()
                .FirstOrDefaultAsync(x => x.Id == userId);

            await this.userRepository.SaveChangesAsync();

            return user;
        }

        public async Task<IEnumerable<string>> GetAllEmailsAsync()
        {
            return await this.userRepository
                .All()
                .Select(x => x.Email)
                .ToListAsync();
        }

        public bool CheckForEmailsAsync(string email)
        {
            return this.userRepository
                .All()
                .Any(x => x.Email == email);

        }

        public async Task<IEnumerable<T>> GetAllUsersAsync<T>(string employee = null)
        {
            if (employee != null)
            {
                var role = await this.roleManager.Roles.SingleAsync(r => r.Name == employee);

                return await this.userRepository
                .All()
                .Where(u => u.Roles.Any(r => r.RoleId == role.Id))
                .To<T>()
                .ToListAsync();
            }

            return await this.userRepository
                .All()
                .To<T>()
                .ToListAsync();
        }

        public async Task<string> GetDistrictAsync(string id)
        {
            var user = await this.userRepository
                .All()
                .FirstOrDefaultAsync(x => x.Id == id);

            return user.District;
        }

        public async Task<string> GetFirstNameAsync(string id)
        {
            var user = await this.userRepository
                .All()
                .FirstOrDefaultAsync(x => x.Id == id);

            return user.FirstName;
        }

        public async Task<string> GetLastNameAsync(string id)
        {
            var user = await this.userRepository
                   .All()
                   .FirstOrDefaultAsync(x => x.Id == id);

            return user.LastName;
        }

        public async Task<string> GetNoteAsync(string id)
        {
            var user = await this.userRepository
                .All()
                .FirstOrDefaultAsync(x => x.Id == id);

            return user.Note;
        }

        public async Task<string> GetStreetAsync(string id)
        {
            var user = await this.userRepository
                .All()
                .FirstOrDefaultAsync(x => x.Id == id);

            return user.Street;
        }

        public async Task<int> GetTownIdAsync(string id)
        {
            var user = await this.userRepository
             .All()
             .FirstOrDefaultAsync(x => x.Id == id);
            var townId = user.TownId == null ? 0 : user.TownId;
            return (int)townId;
        }

        public async Task<ApplicationUser> GetUserByIdAsync(string id)
        {
            return await this.userRepository
                .All()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<T> GetUserByIdAsync<T>(string id)
        {
            return await this.userRepository
                .All()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstAsync();
        }

        public async Task<int> GetUsersCountAsync(string trainerId = null)
        {
            if (trainerId == null)
            {
                return await this.userRepository
                    .All()
                    .CountAsync();
            }
            else
            {
                return await this.userRepository
                    .All()

                    // .Where(x => x.Trainers.Any(y => y.TrainerId == trainerId))
                    .CountAsync();
            }
        }
    }
}
