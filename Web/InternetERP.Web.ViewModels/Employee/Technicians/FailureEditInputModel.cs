namespace InternetERP.Web.ViewModels.Employee.Technicians
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using InternetERP.Common;
    using InternetERP.Data.Models;
    using InternetERP.Services.Mapping;
    using Microsoft.EntityFrameworkCore.Metadata.Internal;

    public class FailureEditInputModel : IMapFrom<Failure>
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Short Description")]
        [StringLength(255, ErrorMessage = GlobalConstants.TextError, MinimumLength = 10)]
        public string ShortDescription { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Range(typeof(decimal), "0.00", "999999.00", ErrorMessage = GlobalConstants.RangeErrorPrice)]
        public decimal Price { get; set; }

        public string Note { get; set; }

        public int StatusFailureId { get; set; }

        public StatusFailure StatusFailure { get; set; }

        public int SelectedStatus { get; set; }

        public string NoteNewStatus { get; set; }

        public string SuccessMsg { get; set; }
    }
}
