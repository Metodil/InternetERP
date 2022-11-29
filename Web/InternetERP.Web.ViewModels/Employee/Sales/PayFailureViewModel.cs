namespace InternetERP.Web.ViewModels.Employee.Sales
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using InternetERP.Common;
    using InternetERP.Data.Models;
    using InternetERP.Web.Infrastructure;

    public class PayFailureViewModel
    {
        public int Step { get; set; }

        public virtual ICollection<InternetAccountType> Services { get; set; }

        public SaleSelectedUser SaleId { get; set; }

        public BillInfo BillInfo { get; set; }

        public InternetAccount InternetAccountInfo { get; set; }

        public string SuccessMsg { get; set; }
    }
}
