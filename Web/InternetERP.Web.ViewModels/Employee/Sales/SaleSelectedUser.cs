namespace InternetERP.Web.ViewModels.Employee.Sales
{
    using InternetERP.Data.Models;
    using InternetERP.Services.Mapping;

    public class SaleSelectedUser : IMapFrom<Bill>
    {
        public SaleSelectedUser()
        {
            this.BillInfo = new BillInfo
            {
                Quantity = 0,
                Totals = 0.00m,
            };
        }

        public string Id { get; set; }

        public string SaleUserId { get; set; }

        public string SelectedUserId { get; set; }

        public string UserFullName { get; set; }

        public string UserAddress { get; set; }

        public string CustomerId { get; set; }

        public string StatusId { get; set; }

        public BillInfo BillInfo { get; set; }
    }
}
