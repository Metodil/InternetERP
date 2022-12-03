namespace InternetERP.Data.Models
{
    using InternetERP.Data.Common.Models;

    public class Payment : BaseDeletableModel<int>
    {
        [Requared]
        public string BillId { get; set; }

        public Bill Bill { get; set; }

        public decimal Amount { get; set; }

        public string Provider { get; set; }
    }
}
