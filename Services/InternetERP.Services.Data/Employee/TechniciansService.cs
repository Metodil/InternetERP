namespace InternetERP.Services.Data.Employee
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using InternetERP.Data.Common.Repositories;
    using InternetERP.Data.Models;
    using InternetERP.Services.Data.Contracts;
    using InternetERP.Services.Mapping;
    using Microsoft.EntityFrameworkCore;

    public class TechniciansService : ITechniciansService
    {
        private readonly IDeletableEntityRepository<Failure> failureRepository;

        public TechniciansService(
            IDeletableEntityRepository<Failure> failureRepository)
        {
            this.failureRepository = failureRepository;
        }

        public async Task<ICollection<T>> GetAllFailuresAsync<T>(
            int page,
            int itemsPerPage,
            #pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
            string? filterByStatus)
            #pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        {
            if (filterByStatus == null)
            {
                return await this.failureRepository
                    .AllAsNoTracking()
                    .Include(f => f.StatusFailure)
                    .OrderByDescending(f => f.CreatedOn)
                    .Skip((page - 1) * itemsPerPage)
                    .Take(itemsPerPage)
                    .To<T>()
                    .ToListAsync();
            }

            return await this.failureRepository
                .AllAsNoTracking()
                .Include(f => f.StatusFailure)
                .OrderByDescending(f => f.CreatedOn)
                .Where(f => f.StatusFailure.Name == filterByStatus)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .To<T>()
                .ToListAsync();
        }

        public async Task<int> CountAsync(string filterByStatus)
        {
            return await this.failureRepository
                                .All()
                                .Where(f => f.StatusFailure.Name == filterByStatus)
                                .CountAsync();
        }
    }
}
