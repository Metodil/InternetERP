﻿namespace InternetERP.Web.ViewModels.Employee.Sales
{
    using System.Collections.Generic;

    using InternetERP.Web.ViewModels.Administration.Users;

    public class SaleGoodsViewModel 
    {
        public SaleGoodsViewModel()
        {
            this.Users = new HashSet<UserListItemViewModel>();
            this.SelectedUser = new HashSet<string>();
        }

        public int Step { get; set; }

        public IEnumerable<UserListItemViewModel> Users { get; set; }

        public virtual ICollection<string> SelectedUser { get; set; }

        public SaleSelectedUser SaleId { get; set; }
    }
}
