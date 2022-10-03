using System.ComponentModel.DataAnnotations;

namespace InternetERP.Models
{
    public class JobTitle
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
