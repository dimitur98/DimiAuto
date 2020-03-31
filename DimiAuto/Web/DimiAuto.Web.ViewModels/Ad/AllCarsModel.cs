namespace DimiAuto.Web.ViewModels.Ad
{
    using System.Collections.Generic;

    using DimiAuto.Web.ViewModels.Ad;
    using DimiAuto.Web.ViewModels.Ad.CompareAds;

    public class AllCarsModel
    {
        public CompareCarsInputModel CompareCarsInputModel { get; set; }

        public IEnumerable<CarAdsViewModel> AllCars { get; set; }
    }
}
