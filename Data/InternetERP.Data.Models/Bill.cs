namespace InternetERP.Data.Models
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    using InternetERP.Data.Common.Models;

    public class Bill : BaseDeletableModel<string>
    {
        public Bill()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string SaleUserId { get; set; }

        public string SelectedUserId { get; set; }

        public ApplicationUser SelectedUser { get; set; }

        public string UserFullName { get; set; }

        public string UserAddress { get; set; }

        public int? CustomerId { get; set; }

        public Customer Customer { get; set; }

        public int StatusId { get; set; }

        public StatusBill Status { get; set; }

        public virtual ICollection<Sale> Sales { get; set; }
    }
}
