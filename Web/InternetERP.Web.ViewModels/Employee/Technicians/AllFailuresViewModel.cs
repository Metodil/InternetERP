namespace InternetERP.Web.ViewModels.Employee.Technicians
{
    using System.Collections.Generic;

    public class AllFailuresViewModel : PagingViewModel
    {
        public ICollection<FailureListViewModel> Failures { get; set; }
    }
}
