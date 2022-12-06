namespace InternetERP.Web.ViewModels.Home
{
    using System.Collections.Generic;

    using InternetERP.Data.Models;

    public class HomeViewModel
    {
        public HomeViewModel()
        {
            this.PromoProducts = new HashSet<Product>();
        }

        public ICollection<Product> PromoProducts { get; set; }
    }
}
