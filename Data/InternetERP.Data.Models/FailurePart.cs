
using System.Collections.Generic;

namespace InternetERP.Models
{
    public class FailurePart
    {
        public int PartId { get; set; }
        public virtual Part Part { get; set; }
        public int FailureId { get; set; }
        public virtual Failure Failure { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public bool Paid { get; set; }
    }
}
