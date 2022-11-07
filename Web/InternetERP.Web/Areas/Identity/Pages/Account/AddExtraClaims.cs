namespace InternetERP.Web.Areas.Identity.Pages.Account
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using InternetERP.Data.Models;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;

    public class AddExtraClaims : IClaimsTransformation
    {
        private readonly UserManager<ApplicationUser> userManager;

        public AddExtraClaims(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            ClaimsIdentity claimsIdentity = new ClaimsIdentity();

            var user = await this.userManager.FindByNameAsync(principal.Identity.Name);
            if (user == null)
            {
                return principal;
            }

            claimsIdentity.AddClaim(new Claim("FirstName", user.FirstName));
            claimsIdentity.AddClaim(new Claim("LastName", user.LastName));

            principal.AddIdentity(claimsIdentity);
            return await Task.FromResult(principal);
        }
    }
}
