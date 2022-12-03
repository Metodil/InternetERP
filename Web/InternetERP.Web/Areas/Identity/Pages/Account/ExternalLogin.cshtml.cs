namespace InternetERP.Web.Areas.Identity.Pages.Account
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using InternetERP.Data.Models;
    using InternetERP.Services.Data.Administration.Contracts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Logging;

    [AllowAnonymous]
    public class ExternalLoginModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IEmailSender emailSender;
        private readonly ICustomUsersService customUsersService;
        private readonly ILogger<ExternalLoginModel> logger;

        public ExternalLoginModel(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            ILogger<ExternalLoginModel> logger,
            IEmailSender emailSender,
            ICustomUsersService customUsersService)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.logger = logger;
            this.emailSender = emailSender;
            this.customUsersService = customUsersService;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string LoginProvider { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }
        }

        public IActionResult OnGetAsync()
        {
            return this.RedirectToPage("./Login");
        }

        public IActionResult OnPost(string provider, string returnUrl = null)
        {
            // Request a redirect to the external login provider.
            var redirectUrl = this.Url.Page("./ExternalLogin", pageHandler: "Callback", values: new { returnUrl });
            var properties = this.signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties);
        }

        public async Task<IActionResult> OnGetCallbackAsync(string returnUrl = null, string remoteError = null)
        {
            returnUrl = returnUrl ?? this.Url.Content("~/");
            if (remoteError != null)
            {
                this.ErrorMessage = $"Error from external provider: {remoteError}";
                return this.RedirectToPage("./Login", new { ReturnUrl = returnUrl });
            }

            var info = await this.signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                this.ErrorMessage = "Error loading external login information.";
                return this.RedirectToPage("./Login", new { ReturnUrl = returnUrl });
            }

            // Sign in the user with this external login provider if the user already has a login.
            var result = await this.signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
            if (result.Succeeded)
            {
                this.logger.LogInformation("{Name} logged in with {LoginProvider} provider.", info.Principal.Identity.Name, info.LoginProvider);
                return this.LocalRedirect(returnUrl);
            }

            if (result.IsLockedOut)
            {
                return this.RedirectToPage("./Lockout");
            }
            else
            {
                // If the user does not have an account, then ask the user to create an account.
                ApplicationUser user;

                if (info.LoginProvider == "Facebook")
                {
                    var firstName = info.Principal.FindFirstValue(ClaimTypes.Name).Split(" ")[0];
                    var lastName = info.Principal.FindFirstValue(ClaimTypes.Name).Split(" ").Last();
                    var identifier = info.Principal.FindFirstValue(ClaimTypes.NameIdentifier);
                    var picture = $"https://graph.facebook.com/{identifier}/picture?type=large";
                    var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                    if (this.customUsersService.CheckForEmailsAsync(email))
                    {
                        this.ErrorMessage = $"Error : Email {email} already exists.";
                        return this.RedirectToPage("./Login", new { ReturnUrl = returnUrl });
                    }

                    user = new ApplicationUser
                    {
                        UserName = email,
                        Email = email,
                        FirstName = firstName,
                        LastName = lastName,
                        ProfilePictureUrl = picture,
                        EmailConfirmed = true,
                    };
                }
                else if (info.LoginProvider == "Google")
                {
                    var firstName = info.Principal.FindFirstValue(ClaimTypes.Name).Split(" ")[0];
                    var lastName = info.Principal.FindFirstValue(ClaimTypes.Name).Split(" ").Last();
                    var identifier = info.Principal.FindFirstValue(ClaimTypes.NameIdentifier);

                    // var picture = $"https://graph.facebook.com/{identifier}/picture?type=large";
                    var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                    if (this.customUsersService.CheckForEmailsAsync(email))
                    {
                        this.ErrorMessage = $"Error: Email {email} already exists.";
                        return this.RedirectToPage("./Login", new { ReturnUrl = returnUrl });
                    }

                    user = new ApplicationUser
                    {
                        UserName = email,
                        Email = email,
                        FirstName = firstName,
                        LastName = lastName,

                        // ProfilePictureUrl = picture,
                        EmailConfirmed = true,
                    };
                }
                else
                {
                    user = new ApplicationUser
                    {
                        UserName = this.Input.Email,
                        Email = this.Input.Email,
                    };
                }

                var loginResult = await this.userManager.CreateAsync(user);
                if (loginResult.Succeeded)
                {
                    loginResult = await this.userManager.AddLoginAsync(user, info);
                    if (loginResult.Succeeded)
                    {
                        await this.signInManager.SignInAsync(user, isPersistent: true);

                        return this.LocalRedirect(returnUrl);
                    }
                }

                foreach (var error in loginResult.Errors)
                {
                    this.ModelState.AddModelError(string.Empty, error.Description);
                }

                this.LoginProvider = info.LoginProvider;
                this.ReturnUrl = returnUrl;
                return this.Page();
            }
        }

        public async Task<IActionResult> OnPostConfirmationAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? this.Url.Content("~/");

            // Get the information about the user from the external login provider
            var info = await this.signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                this.ErrorMessage = "Error loading external login information during confirmation.";
                return this.RedirectToPage("./Login", new { ReturnUrl = returnUrl });
            }

            if (this.ModelState.IsValid)
            {
                ApplicationUser user;

                if (info.LoginProvider == "Facebook")
                {
                    var firstName = info.Principal.FindFirstValue(ClaimTypes.Name).Split(" ")[0];
                    var lastName = info.Principal.FindFirstValue(ClaimTypes.Name).Split(" ").Last();
                    var identifier = info.Principal.FindFirstValue(ClaimTypes.NameIdentifier);
                    var picture = $"https://graph.facebook.com/{identifier}/picture?type=large";

                    user = new ApplicationUser
                    {
                        UserName = this.Input.Email,
                        Email = this.Input.Email,
                        FirstName = firstName,
                        LastName = lastName,
                        ProfilePictureUrl = picture,
                        EmailConfirmed = true,
                    };
                }
                else if (info.LoginProvider == "Google")
                {
                    var firstName = info.Principal.FindFirstValue(ClaimTypes.Name).Split(" ")[0];
                    var lastName = info.Principal.FindFirstValue(ClaimTypes.Name).Split(" ").Last();
                    var identifier = info.Principal.FindFirstValue(ClaimTypes.NameIdentifier);

                    // var picture = $"https://graph.facebook.com/{identifier}/picture?type=large";
                    var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                    if (this.customUsersService.CheckForEmailsAsync(email))
                    {
                        this.ErrorMessage = $"Error: Email {email} already exists.";
                        return this.RedirectToPage("./Login", new { ReturnUrl = returnUrl });
                    }

                    user = new ApplicationUser
                    {
                        UserName = email,
                        Email = email,
                        FirstName = firstName,
                        LastName = lastName,

                        // ProfilePictureUrl = picture,
                        EmailConfirmed = true,
                    };
                }
                else
                {
                    user = new ApplicationUser
                    {
                        UserName = this.Input.Email,
                        Email = this.Input.Email,
                    };
                }

                var result = await this.userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await this.userManager.AddLoginAsync(user, info);
                    if (result.Succeeded)
                    {
                        await this.signInManager.SignInAsync(user, isPersistent: true);

                        return this.LocalRedirect(returnUrl);
                    }
                }

                foreach (var error in result.Errors)
                {
                    this.ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            this.LoginProvider = info.LoginProvider;
            this.ReturnUrl = returnUrl;
            return this.Page();
        }
    }
}
