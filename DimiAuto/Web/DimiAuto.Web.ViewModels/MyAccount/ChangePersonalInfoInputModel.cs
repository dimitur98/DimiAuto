using DimiAuto.Common;
using DimiAuto.Data.Models;
using DimiAuto.Services.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DimiAuto.Web.ViewModels.MyAccount
{
    public class ChangePersonalInfoInputModel : IMapFrom<ApplicationUser>
    {
        [Required]
        [StringLength(GlobalConstants.NameMaxLenght)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(GlobalConstants.NameMaxLenght)]
        public string LastName { get; set; }

        [Required]
        [StringLength(GlobalConstants.CityLenght)]
        public string City { get; set; }

        [Required]
        [StringLength(GlobalConstants.AdressMaxLenght)]
        public string Adress { get; set; }

        [Required(ErrorMessage = "Phone number field is required.")]
        public string PhoneNumber { get; set; }

        [StringLength(GlobalConstants.NameOfCompanyLenght)]
        public string NameOfCompany { get; set; }

        public string Bulstad { get; set; }

        public string NameOfThePage { get; set; }

        public string TelephoneForCustomers { get; set; }

    }
}
