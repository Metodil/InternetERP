
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InternetERP.Models
{
    public class Comment
    {
        public Comment()
        {
            this.Feedbacks = new HashSet<Feedback>();
        }
        public int Id { get; set; }
        [Required]
        public string CommentText { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
    }
 
}
