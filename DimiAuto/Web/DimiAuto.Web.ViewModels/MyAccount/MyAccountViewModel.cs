using System;
using System.Collections.Generic;
using System.Text;

namespace DimiAuto.Web.ViewModels.MyAccount
{
    public class MyAccountViewModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public IEnumerable<MyCarsViewModel> MyCars { get; set; }
    }
}
