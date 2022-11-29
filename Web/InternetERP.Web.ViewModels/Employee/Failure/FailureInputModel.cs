namespace InternetERP.Web.ViewModels.Employee.Failure
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using InternetERP.Common;
    using InternetERP.Data.Models;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class FailureInputModel
    {
        [Required]
        [Display(Name = "Short Description")]
        [StringLength(255, ErrorMessage = GlobalConstants.TextError, MinimumLength = 10)]
        public string ShortDescription { get; set; } = string.Empty;

        [Required]
        public int SelectedAccountId { get; set; }

        public string Note { get; set; } = string.Empty;

        public string FullName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public string SuccessMsg { get; set; }
    }
}
