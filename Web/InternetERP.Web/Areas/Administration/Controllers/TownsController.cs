namespace InternetERP.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using InternetERP.Common;
    using InternetERP.Data.Models;
    using InternetERP.Services.Data.Administration.Contracts;
    using InternetERP.Web.ViewModels.Administration.Towns;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

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
            var model = new AllTownsViewModel
            {
                Towns = await this.townsService.GetAllTownsPagingAsync<TownListViewModel>(
                    id, GlobalConstants.ItemsPerPageList),
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

            // TODO show mess on success
            return this.View(town);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (await this.townsService.CountAsync() == 0)
            {
                return this.NotFound();
            }

            var town = await this.townsService.GetTownByIdAsync(id);
            if (town == null)
            {
                return this.NotFound();
            }

            return this.View(town);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("Id, Name")] TownInputModel town)
        {
            if (id == null || !await this.townsService.TownExist((int)id))
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                await this.townsService.Update(town);
            }

            return this.View(town);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || !await this.townsService.TownExist((int)id))
            {
                return this.NotFound();
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
