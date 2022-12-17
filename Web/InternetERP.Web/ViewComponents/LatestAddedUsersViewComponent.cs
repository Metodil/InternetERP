namespace InternetERP.Web.ViewComponents
{
    using System.Threading.Tasks;

    using InternetERP.Data.Common.Repositories;
    using InternetERP.Data.Models;
    using InternetERP.Services.Data.Administration.Contracts;
    using InternetERP.Web.ViewModels.Administration.Users;
    using Microsoft.AspNetCore.Mvc;

    [ViewComponent(Name = "LatestAddedUsers")]
    public class LatestAddedUsersViewComponent : ViewComponent
    {
        private readonly IDeletableEntityRepository<ApplicationUser> usersDepository;
        private readonly ICustomUsersService usersService;

        public LatestAddedUsersViewComponent(
            IDeletableEntityRepository<ApplicationUser> usersDepository,
            ICustomUsersService usersService)
        {
            this.usersDepository = usersDepository;
            this.usersService = usersService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var viewModel = new LatestsAddedUsersViewModel()
            {
                LatestAddedUsers = await this.usersService.GetLatestAddedUsersAsync(),
            };

            return this.View(viewModel);
        }
    }
}
