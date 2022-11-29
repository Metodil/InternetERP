namespace InternetERP.Web.ViewModels.Employee.Technicians
{
    using System.ComponentModel.DataAnnotations;

    using InternetERP.Common;
    using InternetERP.Data.Models;
    using InternetERP.Services.Mapping;

    public class FailureEditInputModel : IMapFrom<Failure>
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Short Description")]
        [StringLength(255, ErrorMessage = GlobalConstants.TextError, MinimumLength = 10)]
        public string ShortDescription { get; set; } = string.Empty;

        public string Note { get; set; }

        public int StatusFailureId { get; set; }

        public StatusFailure StatusFailure { get; set; }

        public int SelectedStatus { get; set; }

        public string NoteNewStatus { get; set; }
    }
}
