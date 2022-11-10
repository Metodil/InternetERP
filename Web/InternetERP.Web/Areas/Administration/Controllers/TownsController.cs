namespace InternetERP.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using InternetERP.Data.Common.Repositories;
    using InternetERP.Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    public class TownsController : AdministrationController
    {
        private readonly IDeletableEntityRepository<Town> dataRepository;

        public TownsController(IDeletableEntityRepository<Town> dataRepository)
        {
            this.dataRepository = dataRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var towns = await this.dataRepository.AllWithDeleted().ToListAsync();
            return this.View(towns);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Town town)
        {
            if (this.ModelState.IsValid)
            {
                await this.dataRepository.AddAsync(town);
                await this.dataRepository.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(town);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || this.dataRepository.All() == null)
            {
                return this.NotFound();
            }

            var category = await this.dataRepository.All().FirstOrDefaultAsync(x => x.Id == id);
            if (category == null)
            {
                return this.NotFound();
            }

            return this.View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Town town)
        {
            if (id != town.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    this.dataRepository.Update(town);
                    await this.dataRepository.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.TownExists(town.Id))
                    {
                        return this.NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(town);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || this.dataRepository.All() == null)
            {
                return this.NotFound();
            }

            var category = await this.dataRepository.All()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return this.NotFound();
            }

            return this.View(category);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (this.dataRepository.All() == null)
            {
                return this.Problem("Entity set 'ApplicationDbContext.Categories'  is null.");
            }

            var category = this.dataRepository.All().FirstOrDefault(x => x.Id == id);
            if (category != null)
            {
                this.dataRepository.Delete(category);
            }

            await this.dataRepository.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }


        private bool TownExists(int id)
        {
            return this.dataRepository.All().Any(e => e.Id == id);
        }
    }
}
