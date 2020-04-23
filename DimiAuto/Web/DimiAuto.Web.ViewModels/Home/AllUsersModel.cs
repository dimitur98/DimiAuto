namespace DimiAuto.Web.ViewModels.Home
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AllUsersModel
    {
        public ICollection<UserViewModel> Users { get; set; }

        public AllUserInputModel SearchedText { get; set; }
    }
}
