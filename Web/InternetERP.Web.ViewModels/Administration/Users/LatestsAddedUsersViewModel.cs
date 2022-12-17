namespace InternetERP.Web.ViewModels.Administration.Users
{
    using System.Collections.Generic;

    using InternetERP.Data.Models;

    public class LatestsAddedUsersViewModel
    {
        public ICollection<ApplicationUser> LatestAddedUsers { get; set; }
    }
}
