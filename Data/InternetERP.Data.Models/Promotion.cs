namespace InternetERP.Data.Seeding
{
    using System.ComponentModel.DataAnnotations;

    using InternetERP.Data.Common.Models;

    public class Promotion : BaseDeletableModel<int>
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
