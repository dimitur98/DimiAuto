using DimiAuto.Data.Models;
using DimiAuto.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace DimiAuto.Web.ViewModels.MyAccount
{
    public class ChangePersonalInfoInputModel : IMapFrom<ApplicationUser>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string City { get; set; }

        public string Adress { get; set; }

        public string NameOfCompany { get; set; }

        public string PhoneNumber { get; set; }

    }
}
