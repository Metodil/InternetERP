namespace InternetERP.Web.ViewModels.Administration.Roles
{
    using System.Collections.Generic;

    public class AllRolesViewModel : PagingViewModel
    {
        public IEnumerable<RoleListViewModel> Roles { get; set; }
    }
}
