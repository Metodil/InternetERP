namespace InternetERP.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using InternetERP.Common;
    using InternetERP.Services.Data.Administration.Contracts;
    using InternetERP.Web.ErrorHandlingMiddleware.Exceptions;
    using InternetERP.Web.ViewModels.Administration.Towns;
    using Microsoft.AspNetCore.Mvc;

    public class TownsController : AdministrationController
    {
        private readonly ITownsService townsService;

        public TownsController(ITownsService townsService)
        {
            this.townsService = townsService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int id = 1)
        {
            var towns = await this.townsService.GetAllTownsPagingAsync<TownListViewModel>(
                    id, GlobalConstants.ItemsPerPageList);
            var model = new AllTownsViewModel
            {
                Towns = towns,
                ItemsPerPage = GlobalConstants.ItemsPerPageList,
                PageNumber = id,
                AspAction = nameof(this.Index),
                ItemsCount = await this.townsService.CountAsync(),
            };

            return this.View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")] TownInputModel town)
        {
            if (this.ModelState.IsValid)
            {
                var newTown = await this.townsService.CreateAsync(town);
                return this.View(newTown);
            }

            return this.RedirectToAction(nameof(this.Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (await this.townsService.CountAsync() == 0)
            {
                throw new NotFoundException($"Town ID {id} not found.");
            }

            var town = await this.townsService.GetTownByIdAsync(id);
            if (town == null)
            {
              throw new NotFoundException($"Town ID {id} not found.");
            }

            return this.View(town);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("Id, Name")] TownInputModel town)
        {
            if (id == null || !await this.townsService.TownExist((int)id))
            {
                throw new NotFoundException($"Town ID {id} not found.");
            }

            if (this.ModelState.IsValid)
            {
                await this.townsService.Update(town);
            }

            return this.RedirectToAction(nameof(this.Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || !await this.townsService.TownExist((int)id))
            {
                throw new NotFoundException($"Town ID {id} not found.");
            }

            var town = await this.townsService
                .GetTownByIdAsync((int)id);

            return this.View(town);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await this.townsService.CountAsync() == 0)
            {
                return this.Problem("Entity Town is empty.");
            }

            await this.townsService.Delete(id);

            return this.RedirectToAction(nameof(this.Index));
        }
    }
}
