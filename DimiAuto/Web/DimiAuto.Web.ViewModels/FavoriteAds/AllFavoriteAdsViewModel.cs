using DimiAuto.Web.ViewModels.Ad;
using System;
using System.Collections.Generic;
using System.Text;

namespace DimiAuto.Web.ViewModels.FavoriteAds
{
    public class AllFavoriteAdsViewModel
    {
        public ICollection<CarAdsViewModel> FavotitesAds { get; set; }
    }
}
