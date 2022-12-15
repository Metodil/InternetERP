// ReSharper disable VirtualMemberCallInConstructor
namespace InternetERP.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using InternetERP.Data.Common.Models;
    using InternetERP.Data.Models;
    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();
            this.Employees = new HashSet<Employee>();
        }

        [MaxLength(30)]
#nullable enable
        public string? FirstName { get; set; } = null!;

        [MaxLength(30)]
        public string? LastName { get; set; } = null!;

        public string? ProfilePictureUrl { get; set; }

        public int? TownId { get; set; }

#nullable disable
        public virtual Town Town { get; set; }
#nullable enable

        [MaxLength(30)]
        public string? District { get; set; } = null!;

        [MaxLength(50)]
        public string? Street { get; set; } = null!;

        public string? Note { get; set; } = null!;

#nullable disable

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
