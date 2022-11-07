// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
namespace InternetERP.Web.Areas.Identity.Pages.Account.Manage
{
#nullable disable

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;

    using InternetERP.Data.Models;
    using InternetERP.Services.Data.Contracts;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ICustomUsersService customUsersService;
        private readonly ITownsService townsService;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ICustomUsersService customUsersService,
            ITownsService townsService)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this.customUsersService = customUsersService;
            this.townsService = townsService;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
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
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await this._userManager.GetUserNameAsync(user);
            var phoneNumber = await this._userManager.GetPhoneNumberAsync(user);
            var firstName = await this.customUsersService.GetFirstNameAsync(user.Id);
            var lastName = await this.customUsersService.GetLastNameAsync(user.Id);
            var district = await this.customUsersService.GetDistrictAsync(user.Id);
            var street = await this.customUsersService.GetStreetAsync(user.Id);
            var note = await this.customUsersService.GetNoteAsync(user.Id);
            var towns = await this.townsService.GetAllTownsAsKetValuePairsAsync();
            var townId = await this.customUsersService.GetTownIdAsync(user.Id);
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
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await this._userManager.GetUserAsync(this.User);
            if (user == null)
            {
                return this.NotFound($"Unable to load user with ID '{this._userManager.GetUserId(this.User)}'.");
            }

            await this.LoadAsync(user);
            return this.Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await this._userManager.GetUserAsync(this.User);
            if (user == null)
            {
                return this.NotFound($"Unable to load user with ID '{this._userManager.GetUserId(this.User)}'.");
            }

            if (!this.ModelState.IsValid)
            {
                await this.LoadAsync(user);
                return this.Page();
            }

            // TODO make one metod for all
            var phoneNumber = await this._userManager.GetPhoneNumberAsync(user);
            if (this.Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await this._userManager.SetPhoneNumberAsync(user, this.Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    this.StatusMessage = "Unexpected error when trying to set phone number.";
                    return this.RedirectToPage();
                }
            }

            var firstName = await this.customUsersService.GetFirstNameAsync(user.Id);
            if (this.Input.FirstName != firstName)
            {
                var setFirstNameResult = await this.customUsersService.SetFirstNameAsync(user.Id, this.Input.FirstName);
                if (setFirstNameResult == 0)
                {
                    this.StatusMessage = "Unexpected error when trying to set first name.";
                    return this.RedirectToPage();
                }
            }

            var lastName = await this.customUsersService.GetLastNameAsync(user.Id);
            if (this.Input.LastName != lastName)
            {
                var setLastNameResult = await this.customUsersService.SetFirstNameAsync(user.Id, this.Input.LastName);
                if (setLastNameResult == 0)
                {
                    this.StatusMessage = "Unexpected error when trying to set last name.";
                    return this.RedirectToPage();
                }
            }

            var townId = await this.customUsersService.GetTownIdAsync(user.Id);
            if (this.Input.TownId == 0)
            {
                this.StatusMessage = "Town is not selected.";
                return this.RedirectToPage();
            }
            else if (this.Input.TownId != townId)
            {
                var setTownIdResult = await this.customUsersService.SetTownIdAsync(user.Id, this.Input.TownId);
                if (setTownIdResult == 0)
                {
                    this.StatusMessage = "Unexpected error when trying to set town.";
                    return this.RedirectToPage();
                }
            }

            var district = await this.customUsersService.GetDistrictAsync(user.Id);
            if (this.Input.District != district)
            {
                var setDistrictResult = await this.customUsersService.SetDistrictAsync(user.Id, this.Input.District);
                if (setDistrictResult == 0)
                {
                    this.StatusMessage = "Unexpected error when trying to set district.";
                    return this.RedirectToPage();
                }
            }

            var street = await this.customUsersService.GetStreetAsync(user.Id);
            if (this.Input.Street != street)
            {
                var setStreetResult = await this.customUsersService.SetStreetAsync(user.Id, this.Input.Street);
                if (setStreetResult == 0)
                {
                    this.StatusMessage = "Unexpected error when trying to set street.";
                    return this.RedirectToPage();
                }
            }

            var note = await this.customUsersService.GetNoteAsync(user.Id);
            if (this.Input.Note != note)
            {
                var setNoteResult = await this.customUsersService.SetNoteAsync(user.Id, this.Input.Note);
                if (setNoteResult == 0)
                {
                    this.StatusMessage = "Unexpected error when trying to set note.";
                    return this.RedirectToPage();
                }
            }

            await this._signInManager.RefreshSignInAsync(user);
            this.StatusMessage = "Your profile has been updated";
            return this.RedirectToPage();
        }
    }
}
