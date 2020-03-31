namespace DimiAuto.Web.ViewModels.Ad
{
    using System.Collections.Generic;

    using DimiAuto.Web.ViewModels.Ad;
    using DimiAuto.Web.ViewModels.Ad.CompareAds;
    using DimiAuto.Web.ViewModels.Sort;

    public class AllCarsModel
    {
        public CompareCarsInputModel CompareCarsInputModel { get; set; }

        public IEnumerable<CarAdsViewModel> AllCars { get; set; }

        public SortInputModel SortInputModel { get; set; }

       
    }
}
