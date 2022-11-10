#nullable disable

namespace InternetERP.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using InternetERP.Data.Common.Models;

    public class Town : BaseDeletableModel<int>
    {
        public Town()
        {
            this.Users = new HashSet<ApplicationUser>();
        }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        public ICollection<ApplicationUser> Users { get; set; }
    }
}
