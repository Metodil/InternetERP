namespace InternetERP.Web.Areas.Employee.Controllers.Sales
{
    using System.Linq;
    using System.Threading.Tasks;

    using InternetERP.Common;
    using InternetERP.Services.Data.Contracts;
    using InternetERP.Web.ViewModels.Administration.Users;
    using InternetERP.Web.ViewModels.Employee.Manager;
    using InternetERP.Web.ViewModels.Employee.Sales;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Newtonsoft.Json;

    public class SaleGoodsController : EmployeeController
    {
        private readonly ISaleGoodsService saleGoodsService;
        private readonly IHttpContextAccessor httpContextAccessor;

        public SaleGoodsController(
            ISaleGoodsService saleGoodsService,
            IHttpContextAccessor httpContextAccessor)
        {
            this.saleGoodsService = saleGoodsService;
            this.httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public async Task<IActionResult> NewSale()
        {
            var model = new SaleGoodsViewModel
            {
                Step = 1,
                Users = await this.saleGoodsService.GetAllUsersAsync<UserListItemViewModel>(),
            };

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NewSale(SaleGoodsViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            var userId = input.SelectedUser.First();
            var saleIdObj = await this.saleGoodsService.GetCurrentSaleId<SaleSelectedUser>(userId);
            this.TempData.Put(
                "SaleIdObj",
                saleIdObj);
            var newModel = new SaleGoodsViewModel
            {
                Step = 1,
                SaleId = this.TempData.Get<SaleSelectedUser>("saleIdObj"),
            };

            return this.View(newModel);
        }
    }

    public static class TempDataExtensions
    {
        public static void Put<T>(this ITempDataDictionary tempData, string key, T value) where T : class
        {
            tempData[key] = JsonConvert.SerializeObject(value);
        }

        public static T Get<T>(this ITempDataDictionary tempData, string key) where T : class
        {
            object o;
            tempData.TryGetValue(key, out o);
            return o == null ? null : JsonConvert.DeserializeObject<T>((string)o);
        }
    }
}
