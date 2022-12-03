namespace InternetERP.Services.Data.Administration
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using InternetERP.Data.Common.Repositories;
    using InternetERP.Data.Models;
    using InternetERP.Services.Data.Administration.Contracts;
    using InternetERP.Services.Mapping;
    using InternetERP.Web.ViewModels.Administration.Towns;
    using Microsoft.EntityFrameworkCore;

    public class TownsService : ITownsService
    {
        private readonly IDeletableEntityRepository<Town> townsRepository;

        public TownsService(IDeletableEntityRepository<Town> townsRepository)
        {
            this.townsRepository = townsRepository;
        }

        public IEnumerable<KeyValuePair<string, string>> GetAllTownsAsKetValuePairs()
        {
            var townsList = this.townsRepository.AllAsNoTracking().Select(x => new
            {
                x.Id,
                x.Name,
            })
                .OrderBy(x => x.Name)
                .ToList()
                .Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name));

            return townsList;
        }

        public async Task<IEnumerable<T>> GetAllTownsPagingAsync<T>(int page, int itemsPerPage)
        {
            return await this.townsRepository
                .AllAsNoTrackingWithDeleted()
                .OrderBy(x => x.Name)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .To<T>()
                .ToListAsync();
        }

        public async Task<int> CountAsync()
        {
            return await this.townsRepository
                .AllAsNoTracking()
                .CountAsync();
        }

        public async Task<Town> GetTownByIdAsync(int id)
        {
            return await this.townsRepository
                .AllAsNoTrackingWithDeleted()
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<bool> Update(TownInputModel town)
        {
            var townToUpdate = await this.townsRepository
                            .AllWithDeleted()
                            .FirstOrDefaultAsync(t => t.Id == town.Id);
            townToUpdate.Name = town.Name;
            var result = true;
            try
            {
                this.townsRepository.Update(townToUpdate);
                await this.townsRepository.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                result = false;
            }

            return result;
        }

        public async Task<Town> CreateAsync(TownInputModel townInput)
        {
            var town = new Town
            {
                Name = townInput.Name,
            };

            if (!await this.TownExist(townInput.Id))
            {
                await this.townsRepository.AddAsync(town);
                await this.townsRepository.SaveChangesAsync();
            }

            return town;
        }

        public async Task<bool> TownExist(int id)
        {
            return await this.townsRepository
                .AllAsNoTrackingWithDeleted()
                .AnyAsync(t => t.Id == id);
        }

        public async Task<Town> Delete(int id)
        {
            var town = await this.townsRepository
             .All()
             .FirstAsync(x => x.Id == id);

            this.townsRepository.Delete(town);
            await this.townsRepository.SaveChangesAsync();
            return town;
        }
    }
}
