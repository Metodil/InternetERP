namespace InternetERP.Web.Areas.InternetAccount.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using InternetERP.Common;
    using InternetERP.Data.Common.Repositories;
    using InternetERP.Data.Models;
    using InternetERP.Services.Data.Administration.Contracts;
    using InternetERP.Services.Data.Contracts;
    using InternetERP.Web.Areas.Employee.Controllers;
    using InternetERP.Web.ViewModels.Employee.Failure;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class FailureIAController : InternetAccountController
    {
        private readonly ICustomUsersService usersService;
        private readonly IFailuresService failuresService;

        public FailureIAController(
            ICustomUsersService usersService,
            IFailuresService failuresService,
            IDeletableEntityRepository<InternetAccount> internetAccountRepository)
        {
            this.usersService = usersService;
            this.failuresService = failuresService;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var internetUser = await this.usersService.GetUserByIdAsync(userId);
            var internetAccountId = await this.failuresService.GetInternetIdByUserId(userId);
            var fullName = internetUser.FirstName + " " + internetUser.LastName;
            var town = internetUser.Town.Name == null ? "Sofia" : internetUser.Town.Name;
            var address = town + " " +
                internetUser.District + " " +
                internetUser.Street;
            var model = new FailureInputModel
            {
                FullName = fullName,
                Phone = internetUser.PhoneNumber,
                Email = internetUser.Email,
                Address = address,
                SelectedAccountId = internetAccountId,
            };

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FailureInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var createUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            await this.failuresService.CreateFailure(createUserId, input);

            return this.RedirectToAction("Index", "Dashboard", new { area = "InternetAccount" });
        }
    }
}
