namespace DimiAuto.Web.ViewModels.Home
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class UserViewModel
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string NameOfCompany { get; set; }

        public string NameOfThePage { get; set; }

        public string ImgPath { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
