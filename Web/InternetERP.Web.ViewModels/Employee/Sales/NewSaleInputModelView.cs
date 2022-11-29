namespace InternetERP.Web.ViewModels.Employee.Sales
{
    using System.Collections.Generic;

    using InternetERP.Web.ViewModels.Administration.Users;

    public class NewSaleInputModelView
    {
        public NewSaleInputModelView()
        {
            this.Users = new HashSet<UserListItemViewModel>();
            this.SelectedUser = new HashSet<string>();
        }

        public int Step { get; set; }

        public IEnumerable<UserListItemViewModel> Users { get; set; }

        public virtual ICollection<string> SelectedUser { get; set; }
    }
}
