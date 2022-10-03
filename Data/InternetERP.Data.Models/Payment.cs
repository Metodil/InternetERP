
using System;
using System.ComponentModel.DataAnnotations;

namespace InternetERP.Models
{
    public class Payment
    {
        public int Id { get; set; }
        [Required]
        public int  AccountId { get; set; }
        public virtual Account Account { get; set; }
        public int FailureId { get; set; }
        public virtual Failure Failure { get; set; }
        public decimal InternetPayment { get; set; }
        public decimal FailurePayment { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        public string Note { get; set; }
    }
}
