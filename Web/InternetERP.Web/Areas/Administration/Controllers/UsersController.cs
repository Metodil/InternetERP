namespace InternetERP.Web.Areas.Administration.Controllers
{
    using System.Text;
    using System.Threading.Tasks;

    using InternetERP.Common;
    using InternetERP.Services.Data.Administration.Contracts;
    using InternetERP.Web.ViewModels.Administration.Users;
    using Microsoft.AspNetCore.Mvc;

    public class UsersController : AdministrationController
    {
        private readonly ICustomUsersService usersService;
        private readonly IProfileService profileService;

        public UsersController(
            ICustomUsersService usersService,
            IProfileService profileService)
        {
            this.usersService = usersService;
            this.profileService = profileService;
        }

        [HttpGet]
        public async Task<IActionResult> All(int id = 1)
        {
            var model = new AllUsersViewModel
            {
                Users = await this.usersService.GetAllUsersPagingAsync<UserListItemViewModel>(
                    id, GlobalConstants.ItemsPerPageList),
                ItemsPerPage = GlobalConstants.ItemsPerPageList,
                PageNumber = id,
                AspAction = nameof(this.All),
                ItemsCount = await this.usersService.CountAsync(),
            };

            return this.View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Profile(string userId)
        {
            var model = await this.usersService.GetUserByIdAsync<ProfileInputModel>(userId);
            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Profile(ProfileInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            // ToDO add new Town ip profile and manage user
            var returnMessage = new StringBuilder();
            var user = await this.usersService.GetUserByIdAsync(input.Id);
            returnMessage.AppendLine(await this.profileService.UpdatePhoneNumber(user, input.PhoneNumber));
            returnMessage.AppendLine(await this.profileService.UpdateFirstName(input.Id, input.FirstName));
            returnMessage.AppendLine(await this.profileService.UpdateLastName(user.Id, input.LastName));
            returnMessage.AppendLine(await this.profileService.UpdateTownId(user.Id, input.TownId));
            returnMessage.AppendLine(await this.profileService.UpdateDistrictr(user.Id, input.District));
            returnMessage.AppendLine(await this.profileService.UpdateStreet(user.Id, input.Street));
            returnMessage.AppendLine(await this.profileService.UpdateNote(user.Id, input.Note));
            if (!string.IsNullOrEmpty(returnMessage.ToString().Trim()))
            {
                input.ReturnMessage = returnMessage.ToString();
                return this.View(input);
            }

            return this.RedirectToAction(nameof(this.All));
        }
    }
}
