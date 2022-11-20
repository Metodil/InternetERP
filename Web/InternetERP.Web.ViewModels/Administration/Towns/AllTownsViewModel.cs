namespace InternetERP.Web.ViewModels.Administration.Towns
{
    using System.Collections.Generic;

    public class AllTownsViewModel : PagingViewModel
    {
        public IEnumerable<TownListViewModel> Towns { get; set; }
}
}
