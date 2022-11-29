namespace InternetERP.Web.ViewModels.Employee.Technicians
{
    using System;
    using System.Collections.Generic;

    using InternetERP.Data.Models;
    using InternetERP.Services.Mapping;

    public class FailureListViewModel : IMapFrom<Failure>
    {
        public FailureListViewModel()
        {
            this.FailurePhases = new HashSet<FailurePhase>();
        }

        public int Id { get; set; }

        public string ShortDescription { get; set; }

        public int AccountId { get; set; }

        public virtual InternetAccount Account { get; set; }

        public string CreateUserId { get; set; }

        public virtual ApplicationUser CreateUser { get; set; }

        public int StatusFailureId { get; set; }

        public StatusFailure StatusFailure { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? FinishDate { get; set; }

        public string Note { get; set; }

        public virtual int FailurePhaseId { get; set; }

        public virtual ICollection<FailurePhase> FailurePhases { get; set; }
    }
}
