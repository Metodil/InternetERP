#nullable disable

namespace InternetERP.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using InternetERP.Data.Common.Models;
    using Microsoft.EntityFrameworkCore.Metadata.Internal;

    public class Failure : BaseDeletableModel<int>
    {
        public Failure()
        {
            this.FailurePhases = new HashSet<FailurePhase>();
        }

        [Required]
        public string ShortDescription { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public bool IsPaid { get; set; } = false;

        [Required]
        public int AccountId { get; set; }

        public virtual InternetAccount Account { get; set; }

        [Required]
        public string CreateUserId { get; set; }

        public virtual ApplicationUser CreateUser { get; set; }

        [Required]
        public int StatusFailureId { get; set; }

        public StatusFailure StatusFailure { get; set; }

        public DateTime? FinishDate { get; set; }

        public string Note { get; set; }

        public virtual int FailurePhaseId { get; set; }

        public virtual ICollection<FailurePhase> FailurePhases { get; set; }
    }
}
