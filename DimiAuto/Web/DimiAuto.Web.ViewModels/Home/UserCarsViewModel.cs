namespace DimiAuto.Web.ViewModels.Home
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using DimiAuto.Data.Models;
    using DimiAuto.Web.ViewModels.Ad;

    public class UserCarsViewModel
    {
        public ICollection<CarAdsViewModel> AllCars { get; set; }

        public ApplicationUser User { get; set; }

        public ICollection<MostWatchedUserCarViewModel> TopFourCars { get; set; }
    }
}
