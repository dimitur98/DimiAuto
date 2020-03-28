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
    }
}
