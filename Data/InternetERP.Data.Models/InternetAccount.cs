#nullable disable

namespace InternetERP.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using InternetERP.Data.Common.Models;

    public class InternetAccount : BaseDeletableModel<int>
    {
        public InternetAccount()
        {
            this.Failures = new HashSet<Failure>();
            this.Payments = new HashSet<InternetPayment>();
        }

        public string InternetUserId { get; set; }

        public virtual ApplicationUser InternetUser { get; set; }

        [Required]
        public int AccountTypeId { get; set; }

        public virtual InternetAccountType AccountType { get; set; }

        [Required]
        public DateTime ExparedDate { get; set; }

        [Required]
        [MaxLength(30)]
        public string InternetName { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal? МonthlyPayment { get; set; }

        public virtual ICollection<Failure> Failures { get; set; }

        public virtual ICollection<InternetPayment> Payments { get; set; }

    }
}
