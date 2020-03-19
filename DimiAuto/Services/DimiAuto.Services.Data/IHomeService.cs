﻿namespace DimiAuto.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using DimiAuto.Models.CarModel;
    using DimiAuto.Web.ViewModels;
    using DimiAuto.Web.ViewModels.Ad;

    public interface IHomeService
    {
        IEnumerable<CarAdsViewModel> GetAllAds();

        IEnumerable<CarAdsViewModel> GetAdsByCriteria(SearchInputModel criteria);
    }
}
