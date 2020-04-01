// ReSharper disable VirtualMemberCallInConstructor
namespace DimiAuto.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using DimiAuto.Common;
    using DimiAuto.Data.Common.Models;
    using DimiAuto.Models.CarModel;
    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();
            this.NameOfCompany = "Private person";
            this.UserImg = GlobalConstants.DefaultImgAvatar;
        }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public string UserImg { get; set; }

        [Required]
        [StringLength(GlobalConstants.AdressMaxLenght)]
        public string Adress { get; set; }

        [Required]
        [StringLength(GlobalConstants.NameMaxLenght)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(GlobalConstants.NameMaxLenght)]
        public string LastName { get; set; }

        [Required]
        [StringLength(GlobalConstants.CityLenght)]
        public string City { get; set; }

        [StringLength(GlobalConstants.NameOfCompanyLenght)]
        public string? NameOfCompany { get; set; }

        public string? Bulstad { get; set; }

        public string? TelephoneForCustomers { get; set; }

        [StringLength(GlobalConstants.NameOfPageLenght)]
        public string? NameOfThePage { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }
    }
}
