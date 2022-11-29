namespace InternetERP.Web.Areas.Employee.Controllers.Sales
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using InternetERP.Services.Data.Contracts;
    using InternetERP.Web.ViewModels.Employee.Failure;
    using Microsoft.AspNetCore.Mvc;

    public class FailuresController : EmployeeController
    {
        private readonly IFailuresService failuresService;

        public FailuresController(
            IFailuresService failuresService)
        {
            this.failuresService = failuresService;
        }

        [HttpGet]
        public async Task<IActionResult> SelectAccount()
        {
            var model = new SelectUserInputModel
            {
                InternetAccounts = await this.failuresService.GetAllInternetAcountsList(),
            };

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SelectAccount(SelectUserInputModel input)
        {
            var internetUser = await this.failuresService.GetInternetUserById(input.SelectedAccountId);
            input.FullName = internetUser.FirstName + " " + internetUser.LastName;
            input.Phone = internetUser.PhoneNumber;
            input.Email = internetUser.Email;
            input.Address = internetUser.Town.Name + ", " + internetUser.Street;
            input.InternetAccounts = await this.failuresService.GetAllInternetAcountsList();
            return this.RedirectToAction("Create", input);
        }

        [HttpGet]
        public IActionResult Create(SelectUserInputModel input)
        {
            var model = new FailureInputModel
            {
                FullName = input.FullName,
                Phone = input.Phone,
                Email = input.Email,
                Address = input.Address,
                SelectedAccountId = input.SelectedAccountId,
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
            var newModel = new FailureInputModel
            {
                FullName = input.FullName,
                Phone = input.Phone,
                Email = input.Email,
                Address = input.Address,
                SelectedAccountId = input.SelectedAccountId,
                SuccessMsg = "Failure create successfully.",
            };

            return this.View(newModel);
        }
    }
}
