namespace InternetERP.Web.ViewModels.Employee.Sales
{
    using System.ComponentModel.DataAnnotations;

    using InternetERP.Common;

    public class SaleProductInputModelView
    {
        [Required]
        public string ProductId { get; set; }

        [Required]
        [Range(typeof(int), "0", "999999", ErrorMessage = GlobalConstants.RangeErrorPrice)]
        public int ProductQauntity { get; set; }

        [Required]
        public string SaleId { get; set; }

        [Required]
        public string SaleUserId { get; set; }
    }
}
