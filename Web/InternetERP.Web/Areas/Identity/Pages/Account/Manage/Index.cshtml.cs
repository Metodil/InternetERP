// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
namespace InternetERP.Web.Areas.Identity.Pages.Account.Manage
{
    using System;
#nullable disable

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    using System.Threading.Tasks;

    using InternetERP.Data.Models;
    using InternetERP.Services.Data.Administration.Contracts;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ICustomUsersService customUsersService;
        private readonly ITownsService townsService;
        private readonly IProfileService profileService;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ICustomUsersService customUsersService,
            ITownsService townsService,
            IProfileService profileService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.customUsersService = customUsersService;
            this.townsService = townsService;
            this.profileService = profileService;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }

            [Required]
            [MaxLength(30)]
            [MinLength(2)]
            [Display(Name = "First name")]
            public string FirstName { get; set; }

            [Required]
            [MaxLength(30)]
            [MinLength(2)]
            [Display(Name = "Last name")]
            public string LastName { get; set; }

            [Display(Name = "Town")]
            public int TownId { get; set; }

            [MaxLength(30)]
            public string District { get; set; }

            [MaxLength(50)]
            public string Street { get; set; }

            [MaxLength(255)]
            public string Note { get; set; }

            public IEnumerable<KeyValuePair<string, string>> Towns { get; set; }

            public string ProfilePictureUrl { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (user == null)
            {
                return this.NotFound($"Unable to load user with ID '{this.userManager.GetUserId(this.User)}'.");
            }

            await this.LoadAsync(user);
            return this.Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (user == null)
            {
                return this.NotFound($"Unable to load user with ID '{this.userManager.GetUserId(this.User)}'.");
            }

            if (!this.ModelState.IsValid)
            {
                await this.LoadAsync(user);
                return this.Page();
            }

            var returnMessage = new StringBuilder();
            returnMessage.AppendLine(await this.profileService.UpdatePhoneNumber(user, this.Input.PhoneNumber));
            returnMessage.AppendLine(await this.profileService.UpdateFirstName(user.Id, this.Input.FirstName));
            returnMessage.AppendLine(await this.profileService.UpdateLastName(user.Id, this.Input.LastName));
            returnMessage.AppendLine(await this.profileService.UpdateTownId(user.Id, this.Input.TownId));
            returnMessage.AppendLine(await this.profileService.UpdateDistrictr(user.Id, this.Input.District));
            returnMessage.AppendLine(await this.profileService.UpdateStreet(user.Id, this.Input.Street));
            returnMessage.AppendLine(await this.profileService.UpdateNote(user.Id, this.Input.Note));
            if (!string.IsNullOrEmpty(returnMessage.ToString().Trim()))
            {
                this.StatusMessage += returnMessage + Environment.NewLine;
                return this.RedirectToPage();
            }

            await this.signInManager.RefreshSignInAsync(user);
            this.StatusMessage = "Your profile has been updated";
            return this.RedirectToPage();
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await this.userManager.GetUserNameAsync(user);
            var phoneNumber = await this.userManager.GetPhoneNumberAsync(user);
            var firstName = await this.customUsersService.GetFirstNameAsync(user.Id);
            var lastName = await this.customUsersService.GetLastNameAsync(user.Id);
            var district = await this.customUsersService.GetDistrictAsync(user.Id);
            var street = await this.customUsersService.GetStreetAsync(user.Id);
            var note = await this.customUsersService.GetNoteAsync(user.Id);
            var towns = this.townsService.GetAllTownsAsKetValuePairs();
            var townId = await this.customUsersService.GetTownIdAsync(user.Id);
            var picture = user.ProfilePictureUrl; 
            this.Username = userName;

            this.Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                FirstName = firstName,
                LastName = lastName,
                District = district,
                Street = street,
                Note = note,
                TownId = townId,
                Towns = towns,
                ProfilePictureUrl = picture,
            };
        }
    }
}
