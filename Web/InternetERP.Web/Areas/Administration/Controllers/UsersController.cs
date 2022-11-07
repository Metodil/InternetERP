namespace InternetERP.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using InternetERP.Services.Data.Contracts;
    using InternetERP.Web.ViewModels.Administration.Users;
    using Microsoft.AspNetCore.Mvc;

    public class UsersController : AdministrationController
    {
        private readonly ICustomUsersService usersService;

        public UsersController(ICustomUsersService usersService)
        {
            this.usersService = usersService;
        }

        public async Task<IActionResult> All()
        {
            var model = new AllUsersViewModel
            {
                Users = await this.usersService.GetAllUsersAsync<UserListItemViewModel>(),
            };

            return this.View(model);
        }
    }
}
