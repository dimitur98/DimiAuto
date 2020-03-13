namespace DimiAuto.Web.ViewModels.All
{
    using System.Collections.Generic;

    using DimiAuto.Web.ViewModels.Ad;

    public class AllCarsViewModel
    {
        public IEnumerable<CarAdsViewModel> AllCars { get; set; }
    }
}
