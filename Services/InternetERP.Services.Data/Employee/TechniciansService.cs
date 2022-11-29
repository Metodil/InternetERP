namespace InternetERP.Services.Data.Employee
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using InternetERP.Common;
    using InternetERP.Data.Common.Repositories;
    using InternetERP.Data.Models;
    using InternetERP.Services.Data.Contracts;
    using InternetERP.Services.Mapping;
    using InternetERP.Web.ViewModels.Employee.Technicians;
    using Microsoft.EntityFrameworkCore;

    public class TechniciansService : ITechniciansService
    {
        private readonly IDeletableEntityRepository<Failure> failureRepository;
        private readonly IDeletableEntityRepository<StatusFailure> statusFailureRepository;
        private readonly IDeletableEntityRepository<Employee> employeesRepository;
        private readonly IDeletableEntityRepository<FailurePhase> failurePhasesRepository;

        public TechniciansService(
            IDeletableEntityRepository<Failure> failureRepository,
            IDeletableEntityRepository<StatusFailure> statusFailureRepository,
            IDeletableEntityRepository<Employee> employeesRepository,
            IDeletableEntityRepository<FailurePhase> failurePhasesRepository)
        {
            this.failureRepository = failureRepository;
            this.statusFailureRepository = statusFailureRepository;
            this.employeesRepository = employeesRepository;
            this.failurePhasesRepository = failurePhasesRepository;
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

        public async Task<T> GetFailuresByIdAsync<T>(int failureId)
        {
            return await this.failureRepository
                .All()
                .Include(f => f.StatusFailure)
                .Where(f => f.Id == failureId)
                .To<T>()
                .FirstAsync();
        }

        public async Task SaveFailure(FailureEditInputModel input)
        {
            var failure = await this.failureRepository
                .All()
                .Where(f => f.Id == input.Id)
                .FirstAsync();
            if (failure != null)
            {
                failure.ShortDescription = input.ShortDescription;
                failure.Note = input.Note;
                await this.failureRepository.SaveChangesAsync();
            }
        }

        public async Task ChangeFailureStatus(
            FailureEditInputModel input,
            string createUserId,
            int statusFailureId)
        {
            var teamId = await this.employeesRepository
                .AllAsNoTracking()
                .Where(e => e.EmployeeUserId == createUserId)
                .Select(e => e.FailureTeamId)
                .FirstAsync();
            var newFailurePhase = new FailurePhase
            {
                FailureId = input.Id,
                UserId = createUserId,
                FailureTeamId = (int)teamId,
                StatusFailureId = statusFailureId,
                Note = input.NoteNewStatus + DateTime.Now.ToString("HH:mm"),
            };

            await this.failurePhasesRepository.AddAsync(newFailurePhase);
            await this.failurePhasesRepository.SaveChangesAsync();

            var failure = await this.failureRepository
                .All()
                .Where(f => f.Id == input.Id)
                .FirstAsync();

            failure.StatusFailureId = statusFailureId;
            if (statusFailureId == GlobalConstants.FailureFinishedStatusId)
            {
                failure.FinishDate = DateTime.Now;
            }
            await this.failureRepository.SaveChangesAsync();
        }
    }
}
