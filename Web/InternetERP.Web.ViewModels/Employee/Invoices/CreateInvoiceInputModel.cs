namespace InternetERP.Web.ViewModels.Employee.Invoices
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using InternetERP.Common;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class CreateInvoiceInputModel
    {
        public string BillId { get; set; }

        [Required]
        [Display(Name = "Customer")]
        [Range(1, int.MaxValue, ErrorMessage = GlobalConstants.PositiveNumberError)]
        public int SelectedCustomerId { get; set; }

        public List<SelectListItem> Customers { get; set; }

        [Required]
        [Display(Name = "Bill")]
        #pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        public string? SelectedBillId { get; set; }
        #pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.

        public List<SelectListItem> Bills { get; set; }

        [Required]
        [Display(Name = "Payment Type")]
        [Range(1, int.MaxValue, ErrorMessage = GlobalConstants.PositiveNumberError)]
        public int SelectedPaymentTypeId { get; set; }

        public List<SelectListItem> PaymentTypes { get; set; }

        public string SuccessMsg { get; set; }

        public int InvoiceId { get; set; }
    }
}
