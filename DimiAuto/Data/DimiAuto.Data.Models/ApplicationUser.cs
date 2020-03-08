﻿// ReSharper disable VirtualMemberCallInConstructor
namespace DimiAuto.Data.Models
{
    using System;
    using System.Collections.Generic;

    using DimiAuto.Data.Common.Models;
    using FinalProject.Models.CarModel;
    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();
            this.Role = Role.User;
            this.Ads = new HashSet<Car>();
        }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public string Adress { get; set; }

        public string City { get; set; }

        public string NameOfCompany { get; set; }

        public string Bulstad { get; set; }

        public string TelephoneForCustomers { get; set; }

        public string NameOfThePage { get; set; }

        public Role Role { get; set; }

        public virtual ICollection<Car> Ads { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }
    }
}
