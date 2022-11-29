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
        private readonly IDeletableEntityRepository<StatusFailure> statusFailureRepository;

        public TechniciansService(
            IDeletableEntityRepository<Failure> failureRepository,
            IDeletableEntityRepository<StatusFailure> statusFailureRepository)
        {
            this.failureRepository = failureRepository;
            this.statusFailureRepository = statusFailureRepository;
        }

        public async Task<ICollection<T>> GetAllFailuresAsync<T>(
            int page,
            int itemsPerPage,
            int filterByStatus)
        {
            if (filterByStatus == 0)
            {
                return await this.failureRepository
                    .AllAsNoTracking()
                    .Include(f => f.StatusFailure)
                    .Include(f => f.FailurePhases)
                    .ThenInclude(fp => fp.FailureTeam)
                    .Include(f => f.FailurePhases)
                    .ThenInclude(fp => fp.User)
                    .Include(f => f.FailurePhases)
                    .ThenInclude(fp => fp.StatusFailure)
                    .OrderByDescending(f => f.CreatedOn)
                    .Skip((page - 1) * itemsPerPage)
                    .Take(itemsPerPage)
                    .To<T>()
                    .ToListAsync();
            }

            var count = await this.statusFailureRepository
                .AllAsNoTracking()
                .CountAsync();

            if (filterByStatus > count || filterByStatus < 0)
            {
                filterByStatus = 0;
            }

            return await this.failureRepository
                .AllAsNoTracking()
                .Include(f => f.StatusFailure)
                .Include(f => f.FailurePhases)
                .ThenInclude(fp => fp.FailureTeam)
                .Include(f => f.FailurePhases)
                .ThenInclude(fp => fp.User)
                .Include(f => f.FailurePhases)
                .ThenInclude(fp => fp.StatusFailure)
                .OrderByDescending(f => f.CreatedOn)
                .Where(f => f.StatusFailure.Id == filterByStatus)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .To<T>()
                .ToListAsync();
        }

        public async Task<int> CountAsync(int filterByStatus)
        {
            if (filterByStatus == 0)
            {
                return await this.failureRepository
                                    .All()
                                    .CountAsync();
            }

            return await this.failureRepository
                                .All()
                                .Where(f => f.StatusFailure.Id == filterByStatus)
                                .CountAsync();
        }
    }
}
