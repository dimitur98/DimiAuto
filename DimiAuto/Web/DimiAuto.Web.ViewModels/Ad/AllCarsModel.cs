namespace DimiAuto.Web.ViewModels.Ad
{
    using System.Collections.Generic;

    using DimiAuto.Web.ViewModels.Ad;
    using DimiAuto.Web.ViewModels.Ad.CompareAds;
    using DimiAuto.Web.ViewModels.Sort;

    public class AllCarsModel
    {
        public CompareCarsInputModel CompareCarsInputModel { get; set; }

        public ICollection<CarAdsViewModel> AllCars { get; set; }

        public SortInputModel SortInputModel { get; set; }

        public string Action { get; set; }

        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }

    }
}
