namespace InternetERP.Web.ViewModels.Administration.Users
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using InternetERP.Data.Models;
    using InternetERP.Services.Mapping;
    using Microsoft.AspNetCore.Identity;

    public class UserListItemViewModel : IMapFrom<ApplicationUser>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ProfilePictureUrl { get; set; }

        public string Email { get; set; }

        public string TownName { get; set; }

        public DateTime CreatedOn { get; set; }

        public string PhoneNumber { get; set; }

        public ICollection<IdentityUserRole<string>> Roles { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
             configuration.CreateMap<ApplicationUser, UserListItemViewModel>().ForMember(
                m => m.TownName,
                opt => opt.MapFrom(x => x.Town.Name));
        }
    }
}
