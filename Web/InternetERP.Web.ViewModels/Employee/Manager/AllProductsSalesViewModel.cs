namespace InternetERP.Web.ViewModels.Employee.Manager
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using InternetERP.Common;
    using InternetERP.Web.ViewModels.Employee.Sales;

    public class AllProductsSalesViewModel : PagingViewModel
    {
        public AllProductsSalesViewModel()
        {
            this.BillInfo = new BillInfo
            {
                Quantity = 0,
                Totals = 0.00m,
            };
        }

        public IEnumerable<ProductListModelView> Products { get; set; }

        public int Step { get; set; }

        public SaleSelectedUser SaleId { get; set; }

        public BillInfo BillInfo { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        [Range(typeof(int), "0", "999999", ErrorMessage = GlobalConstants.RangeErrorPrice)]
        public int ProductQuantity { get; set; }

        [Required]
        public string SaleUserId { get; set; }

        [Required]
        public string BillId { get; set; }

        public int PageId { get; set; }

        public string ProductFilterBy { get; set; }
    }
}
