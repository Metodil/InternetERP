namespace InternetERP.Web.ViewModels.Employee.Technicians
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AllFailuresViewModel : PagingViewModel
    {
        public ICollection<FailureListViewModel> Failures { get; set; }

        public int Page { get; set; }

        public int SelectedStatus { get; set; }
    }
}
