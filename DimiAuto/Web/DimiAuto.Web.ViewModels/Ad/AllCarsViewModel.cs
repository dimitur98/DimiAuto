namespace DimiAuto.Web.ViewModels.Ad
{
    using System.Collections.Generic;

    using DimiAuto.Web.ViewModels.Ad;

    public class AllCarsViewModel
    {
        public string OrderByPrice { get; set; }

        public string OrderByYear { get; set; }

        public IEnumerable<CarAdsViewModel> AllCars { get; set; }
    }
}
