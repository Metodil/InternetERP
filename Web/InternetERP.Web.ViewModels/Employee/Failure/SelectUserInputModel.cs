namespace InternetERP.Web.ViewModels.Employee.Failure
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Mvc.Rendering;

    public class SelectUserInputModel
    {
        [Required]
        public int SelectedAccountId { get; set; }

        public List<SelectListItem> InternetAccounts { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }
    }
}
