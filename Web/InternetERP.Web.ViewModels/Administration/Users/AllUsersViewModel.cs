namespace InternetERP.Web.ViewModels.Administration.Users
{
    using System.Collections.Generic;

    public class AllUsersViewModel : PagingViewModel
    {
        public IEnumerable<UserListItemViewModel> Users { get; set; }
    }
}
