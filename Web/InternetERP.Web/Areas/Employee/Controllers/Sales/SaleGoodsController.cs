namespace InternetERP.Web.Areas.Employee.Controllers.Sales
{
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using InternetERP.Common;
    using InternetERP.Data.Models;
    using InternetERP.Services.Data.Contracts;
    using InternetERP.Web.ViewModels.Administration.Users;
    using InternetERP.Web.ViewModels.Employee.Manager;
    using InternetERP.Web.ViewModels.Employee.Sales;
    using Microsoft.AspNetCore.Mvc;
    using PayPal.Api;

    public class SaleGoodsController : EmployeeController
    {
        private readonly ISaleGoodsService saleGoodsService;

        public SaleGoodsController(
            ISaleGoodsService saleGoodsService)
        {
            this.saleGoodsService = saleGoodsService;
        }

        [HttpGet]
        public async Task<IActionResult> NewSale()
        {
            var saleUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var saleId = await this.saleGoodsService.GetCurrentSaleId<SaleSelectedUser>(saleUserId);
            if (saleId != null)
            {
                // there is allready began bill/sale
                var newModel = new SaleGoodsViewModel
                {
                    Step = 1,
                    SaleId = await this.saleGoodsService.GetCurrentSaleId<SaleSelectedUser>(saleUserId),
                    BillInfo = await this.saleGoodsService.GetBillInfo(saleId.Id),
                };

                return this.View(newModel);
            }

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

            var saleUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var selectedUserId = input.SelectedUser.First();
            await this.saleGoodsService.NewSaleId(saleUserId, selectedUserId);
            var saleId = await this.saleGoodsService.GetCurrentSaleId<SaleSelectedUser>(saleUserId);
            var newModel = new SaleGoodsViewModel
            {
                Step = 1,
                SaleId = saleId,
                BillInfo = await this.saleGoodsService.GetBillInfo(saleId.Id),
            };

            return this.View(newModel);
        }

        [HttpGet]
        #pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        public async Task<IActionResult> SaleProducts(int id = 1, string? filterBy = null)
        #pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        {
            var saleUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var saleId = await this.saleGoodsService.GetCurrentSaleId<SaleSelectedUser>(saleUserId);
            var model = new AllProductsSalesViewModel
            {
                Products = await this.saleGoodsService.GetFilteredProductsPagingAsync<ProductListModelView>(
                    id, GlobalConstants.ItemsPerPageGrid, filterBy),
                ItemsPerPage = GlobalConstants.ItemsPerPageGrid,
                PageNumber = id,
                AspAction = nameof(this.SaleProducts),
                ItemsCount = await this.saleGoodsService.CountAsync(filterBy),
                FilterBy = filterBy,
                Step = 2,
                SaleId = saleId,
                BillInfo = await this.saleGoodsService.GetBillInfo(saleId.Id),
            };

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaleProducts(AllProductsSalesViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var result = await this.saleGoodsService.SellProduct(input);

            var saleUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var model = new AllProductsSalesViewModel
            {
                Products = await this.saleGoodsService.GetFilteredProductsPagingAsync<ProductListModelView>(
                    input.PageNumber, GlobalConstants.ItemsPerPageGrid, input.ProductFilterBy),
                ItemsPerPage = GlobalConstants.ItemsPerPageGrid,
                PageNumber = input.PageNumber,
                AspAction = nameof(this.SaleProducts),
                ItemsCount = await this.saleGoodsService.CountAsync(input.ProductFilterBy),
                FilterBy = input.ProductFilterBy,
                Step = 2,
                SaleId = await this.saleGoodsService.GetCurrentSaleId<SaleSelectedUser>(saleUserId),
                BillInfo = await this.saleGoodsService.GetBillInfo(input.BillId),
            };

            return this.View(model);
        }

        [HttpGet]
        public async Task<IActionResult> SaleServices()
        {
            var saleUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var saleId = await this.saleGoodsService.GetCurrentSaleId<SaleSelectedUser>(saleUserId);
            var model = new SaleServicesViewModel
            {
                Step = 3,
                SaleId = saleId,
                BillInfo = await this.saleGoodsService.GetBillInfo(saleId.Id),
                Services = await this.saleGoodsService.GetServices(),
                InternetAccountInfo = await this.saleGoodsService.GetInternetAccountInfo(saleId.SelectedUserId),
            };

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaleServices(SaleServicesViewModel input)
        {
            var saleUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var saleId = await this.saleGoodsService.GetCurrentSaleId<SaleSelectedUser>(saleUserId);
            if (!this.ModelState.IsValid)
            {
                input.Step = 3;
                input.SaleId = saleId;
                input.BillInfo = await this.saleGoodsService.GetBillInfo(saleId.Id);
                input.Services = await this.saleGoodsService.GetServices();
                input.InternetAccountInfo = await this.saleGoodsService.GetInternetAccountInfo(saleId.SelectedUserId);
                return this.View(input);
            }

            input.InternetAccountInfo = await this.saleGoodsService.GetInternetAccountInfo(saleId.SelectedUserId);
            var result = await this.saleGoodsService.SellService(input);

            var saleUserIdN = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var saleIdN = await this.saleGoodsService.GetCurrentSaleId<SaleSelectedUser>(saleUserIdN);
            var model = new SaleServicesViewModel
            {
                Step = 3,
                SaleId = saleIdN,
                BillInfo = await this.saleGoodsService.GetBillInfo(saleIdN.Id),
                Services = await this.saleGoodsService.GetServices(),
                InternetAccountInfo = await this.saleGoodsService.GetInternetAccountInfo(saleId.SelectedUserId),
                SuccessMsg = "Successfuly pay montrly payment to this internet account!",
            };

            return this.View(model);
        }

        [HttpGet]
        public async Task<IActionResult> PayFailure()
        {
            var saleUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var saleId = await this.saleGoodsService.GetCurrentSaleId<SaleSelectedUser>(saleUserId);
            var internetAccountInfo = await this.saleGoodsService.GetInternetAccountInfo(saleId.SelectedUserId);
            var model = new PayFailureViewModel
            {
                Step = 4,
                SaleId = saleId,
                BillInfo = await this.saleGoodsService.GetBillInfo(saleId.Id),
                Failures = await this.saleGoodsService.GetFailures(internetAccountInfo.Id),
                InternetAccountInfo = internetAccountInfo,
            };

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PayFailure(PayFailureViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                var saleUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var saleId = await this.saleGoodsService.GetCurrentSaleId<SaleSelectedUser>(saleUserId);
                var internetAccountInfo = await this.saleGoodsService.GetInternetAccountInfo(saleId.SelectedUserId);
                input.Step = 4;
                input.SaleId = saleId;
                input.BillInfo = await this.saleGoodsService.GetBillInfo(saleId.Id);
                input.Failures = await this.saleGoodsService.GetFailures(internetAccountInfo.Id);
                input.InternetAccountInfo = internetAccountInfo;

                return this.View(input);
            }

            var result = await this.saleGoodsService.SaleFailureAmount(input);

            var saleUserIdN = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var saleIdN = await this.saleGoodsService.GetCurrentSaleId<SaleSelectedUser>(saleUserIdN);
            var internetAccountInfoN = await this.saleGoodsService.GetInternetAccountInfo(saleIdN.SelectedUserId);
            var model = new PayFailureViewModel
            {
                Step = 4,
                SaleId = saleIdN,
                BillInfo = await this.saleGoodsService.GetBillInfo(saleIdN.Id),
                Failures = await this.saleGoodsService.GetFailures(internetAccountInfoN.Id),
                InternetAccountInfo = internetAccountInfoN,
                SuccessMsg = "Successfuly pay failure payments to this internet account!",
            };

            return this.View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Checkout()
        {
            var saleUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var saleId = await this.saleGoodsService.GetCurrentSaleId<SaleSelectedUser>(saleUserId);
            var internetAccountInfo = await this.saleGoodsService.GetInternetAccountInfo(saleId.SelectedUserId);
            var model = new CheckoutViewModel
            {
                Step = 5,
                SaleId = saleId,
                BillInfo = await this.saleGoodsService.GetBillInfo(saleId.Id),
                Sales = await this.saleGoodsService.GetSales(saleId.Id),
                InternetAccountInfo = internetAccountInfo,
            };

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(CheckoutViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                var saleUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var saleId = await this.saleGoodsService.GetCurrentSaleId<SaleSelectedUser>(saleUserId);
                var internetAccountInfo = await this.saleGoodsService.GetInternetAccountInfo(saleId.SelectedUserId);
                input.Step = 5;
                input.SaleId = saleId;
                input.BillInfo = await this.saleGoodsService.GetBillInfo(saleId.Id);
                input.Sales = await this.saleGoodsService.GetSales(saleId.Id);
                input.InternetAccountInfo = internetAccountInfo;

                return this.View(input);
            }

            await this.saleGoodsService.UpdateCheckout(input);

            var saleUserIdN = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var saleIdN = await this.saleGoodsService.GetCurrentSaleId<SaleSelectedUser>(saleUserIdN);
            var internetAccountInfoN = await this.saleGoodsService.GetInternetAccountInfo(saleIdN.SelectedUserId);
            var model = new CheckoutViewModel
            {
                Step = 5,
                SaleId = saleIdN,
                BillInfo = await this.saleGoodsService.GetBillInfo(saleIdN.Id),
                Sales = await this.saleGoodsService.GetSales(saleIdN.Id),
                InternetAccountInfo = internetAccountInfoN,
                SuccessMsg = "Successfuly update quantities!",
            };

            return this.View(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetFailureInfo(int id)
        {
            var model = new FailureInfoViewModel
            {
                Failure = await this.saleGoodsService.GetFailureById(id),
            };

            return this.PartialView("./Partial/_FailureInfo", model);
        }
    }
}
