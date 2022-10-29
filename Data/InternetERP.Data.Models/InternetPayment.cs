#nullable disable

namespace InternetERP.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using InternetERP.Data.Common.Models;

    public class InternetPayment : BaseDeletableModel<int>
    {
        [Required]
        public int AccountId { get; set; }

        public virtual InternetAccount Account { get; set; }

        public int? SaleId { get; set; }

        public virtual Sale Sale { get; set; }
    }
}
