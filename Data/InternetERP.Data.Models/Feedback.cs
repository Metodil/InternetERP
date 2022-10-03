
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InternetERP.Models
{
    public class Feedback
    {
        public Feedback()
        {
            this.Comments = new HashSet<Comment>();
        }
        public int Id { get; set; }
        [Required]
        public int FailureId { get; set; }
        public virtual Failure Failure { get; set; }
        [Required]
        [MaxLength(50)]
        public string Email { get; set; }
        [Required]
        [MaxLength(50)]
        public string FullName { get; set; }
        [Required]
        public string FeedbackText { get; set; }
        [Required]
        public int Stars { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
