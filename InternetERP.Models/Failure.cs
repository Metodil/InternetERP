using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InternetERP.Models
{
    public class Failure
    {
        public Failure()
        {
            this.FailuresParts = new HashSet<FailurePart>();
            this.Payments = new HashSet<Payment>();
        }
        public int Id { get; set; }
        [Required]
        public string ShortDescription { get; set; }
        [Required]
        public int AccountId { get; set; }
        public virtual Account Account { get; set; }
        [Required]
        public int StatusFailureId { get; set; }
        public StatusFailure StatusFailure { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        public DateTime FinishDate { get; set; }
        public virtual ICollection<FailurePart> FailuresParts { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }

    }
}
