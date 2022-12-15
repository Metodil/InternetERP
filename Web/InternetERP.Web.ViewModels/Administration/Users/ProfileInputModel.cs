namespace InternetERP.Web.ViewModels.Administration.Users
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using InternetERP.Data.Models;
    using InternetERP.Services.Mapping;
    using Microsoft.AspNetCore.Identity;

    public class ProfileInputModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        [Display(Name = "User Name")]
        public string UserName { get; set; }

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

        public ICollection<IdentityUserRole<string>> Roles { get; set; }

        public string ReturnMessage { get; set; }

        public string ProfilePictureUrl { get; set; }
    }
}
