namespace InternetERP.Web.ViewModels.Employee.Sales
{
    using System.Collections.Generic;

    using InternetERP.Web.ViewModels.Administration.Users;

    public class SaleGoodsViewModel
    {
        public SaleGoodsViewModel()
        {
            this.Users = new HashSet<UserListItemViewModel>();
            this.SelectedUser = new HashSet<string>();
            this.BillInfo = new BillInfo
            {
                Quantity = 0,
                Totals = 0.00m,
            };
        }

        public int Step { get; set; }

        public IEnumerable<UserListItemViewModel> Users { get; set; }

        public virtual ICollection<string> SelectedUser { get; set; }

        public SaleSelectedUser SaleId { get; set; }

        public BillInfo BillInfo { get; set; }
    }
}
