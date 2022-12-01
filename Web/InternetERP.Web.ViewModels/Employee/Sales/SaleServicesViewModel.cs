namespace InternetERP.Web.ViewModels.Employee.Sales
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using InternetERP.Common;
    using InternetERP.Data.Models;
    using InternetERP.Web.Infrastructure.CustomValidate;

    public class SaleServicesViewModel
    {
        public int Step { get; set; }

        public virtual ICollection<InternetAccountType> Services { get; set; }

        [Required]
        [Display(Name = "Montly Payment")]
        [Range(typeof(decimal), "0.00", "999999.00", ErrorMessage = GlobalConstants.RangeErrorPrice)]
        public decimal MontlyPayment { get; set; }

        [Required]
        [Display(Name = "Expare dDate")]
        [DataType(DataType.Date)]
        [CheckDateRangeAttribute]
        public DateTime ExparedDate { get; set; }

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
