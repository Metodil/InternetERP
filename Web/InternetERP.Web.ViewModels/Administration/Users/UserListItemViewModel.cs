namespace InternetERP.Web.ViewModels.Administration.Users
{
    using InternetERP.Data.Models;
    using InternetERP.Services.Mapping;

    public class UserListItemViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ProfilePictureUrl { get; set; }

        public string CardId { get; set; }
    }
}
