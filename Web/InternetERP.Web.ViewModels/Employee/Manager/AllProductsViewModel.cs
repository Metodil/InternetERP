namespace InternetERP.Web.ViewModels.Employee.Manager
{
    using System.Collections.Generic;

    using InternetERP.Web.ViewModels.Administration.Roles;

    public class AllProductsViewModel : PagingViewModel
    {
        public IEnumerable<ProductListModelView> Products { get; set; }
    }
}
