namespace InternetERP.Web.Areas.Employee.Controllers.Sales
{
    using InternetERP.Web.ViewModels.Employee.Sales;
    using Microsoft.AspNetCore.Mvc;

    public class SaleGoodsController : EmployeeController
    {
        public IActionResult NewSale()
        {
            var model = new SaleGoodsViewModel
            {
                Step = 1,
            };

            return this.View(model);
        }
    }
}
