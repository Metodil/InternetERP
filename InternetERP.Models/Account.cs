using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InternetERP.Models
{
    public class Account
    {
        public Account()
        {
			this.Failures = new HashSet<Failure>();
			this.Payments = new HashSet<Payment>();
			this.Phones = new HashSet<Phone>();
        }
        public int Id { get; set; }
		[Required]
		public int TypeAccountId { get; set; }
        public virtual TypeAccount TypeAccount { get; set; }
		[Required]
        public DateTime ExparedDate { get; set; }
		[Required]
		[MaxLength(30)]
		public string UserName { get; set; }
		//[Required]
		//[MaxLength(30)]
		//public string LastName { get; set; }
		[Required]
		[MaxLength(35)]
		public string Password { get; set; }
		[Required]
		[MaxLength(50)]
		public string FirstName { get; set; }
		[Required]
		[MaxLength(50)]
		public string LastName { get; set; }
		[Required]
        public int AddressId { get; set; }
        public virtual Address Address { get; set; }
		//[Required]
		public decimal? МonthlyPayment { get; set; }
		[Required]
		public DateTime CreateDate  { get; set; }
        public virtual ICollection<Failure> Failures { get; set; }
		public virtual ICollection<Payment> Payments { get; set; }
		public virtual ICollection<Phone> Phones { get; set; }
	}
}
