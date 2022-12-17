namespace InternetERP.Web.ViewModels.InternetAccount.Dashboard
{
    using System.Collections.Generic;

    using InternetERP.Data.Models;

    public class LatestsPaymentsForInternetAccountsViewModel
    {
        public ICollection<Sale> LatestPaymentsForInternetAccounts { get; set; }
    }
}
