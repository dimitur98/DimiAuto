using DimiAuto.Data.Models;
using DimiAuto.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace DimiAuto.Web.ViewModels.Administration
{
    public class UserDetailsViewModel 
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string City { get; set; }

        public string Adress { get; set; }

        public string PhoneNumber { get; set; }

        public string NameOfCompany { get; set; }

        public bool IsDeleted { get; set; }

        public string Statuse => this.IsDeleted ? "Deleted" : "Not deleted";

        public string Email { get; set; }

        public string ImgPath { get; set; }

        public string PhoneForCustomers { get; set; }

        public string Bulstad { get; set; }
    }
}
