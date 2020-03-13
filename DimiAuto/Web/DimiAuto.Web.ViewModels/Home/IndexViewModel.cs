namespace DimiAuto.Web.ViewModels.Home
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using DimiAuto.Models.CarModel;
    using DimiAuto.Web.ViewModels.Ad;

    public class IndexViewModel
    {
        public IEnumerable<FourMostViewAdCarsViewModel> Ads { get; set; }
    }
}
