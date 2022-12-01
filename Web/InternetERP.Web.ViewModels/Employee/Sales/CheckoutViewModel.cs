namespace InternetERP.Web.ViewModels.Employee.Sales
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using InternetERP.Common;
    using InternetERP.Data.Models;
    using InternetERP.Web.Infrastructure.CustomValidate;

    public class CheckoutViewModel
    {
        public int Step { get; set; }

        public virtual ICollection<Sale> Sales { get; set; }

        [Required]
        [Display(Name = "Quantity")]
        [ArrayIsInt]

        // [Range(typeof(int), "0", "999999", ErrorMessage = GlobalConstants.RangeErrorPrice)]
        public int[] Quantities { get; set; }

        [Required]
        public int[] StockIds { get; set; }

        [Required]
        public string BillId { get; set; }

        [Required]
        public string SaleInternetAccountId { get; set; }

        public SaleSelectedUser SaleId { get; set; }

        public BillInfo BillInfo { get; set; }

        public InternetAccount InternetAccountInfo { get; set; }

        public string SuccessMsg { get; set; }
    }
}
