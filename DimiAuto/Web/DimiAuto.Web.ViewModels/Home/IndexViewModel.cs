using DimiAuto.Models.CarModel;
using DimiAuto.Web.ViewModels.Ad;
using System;
using System.Collections.Generic;
using System.Text;

namespace DimiAuto.Web.ViewModels.Home
{
   public class IndexViewModel
    {
        public IEnumerable<CarAdsViewModel> Ads { get; set; }
    }
}
