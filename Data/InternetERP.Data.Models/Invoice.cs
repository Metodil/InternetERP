#nullable disable

namespace InternetERP.Data.Models
{
    using System.Collections.Generic;

    using InternetERP.Data.Common.Models;

    public class Invoice : BaseDeletableModel<int>
    {
        public int CustomerId { get; set; }

        public Customer Customer { get; set; }

        public string BillId { get; set; }

        public Bill Bill { get; set; }

        public int PaymentTypeId { get; set; }

        public PaymentType PaymentType { get; set; }

        public virtual ICollection<Sale> Sales { get; set; }
    }
}
