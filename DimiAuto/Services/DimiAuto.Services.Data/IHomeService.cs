namespace DimiAuto.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using DimiAuto.Models.CarModel;
    using DimiAuto.Web.ViewModels.Home;
    using DimiAuto.Web.ViewModels.Ad;

    public interface IHomeService
    {
        Task<IEnumerable<CarAdsViewModel>> GetAllAdsAsync();

        Task<IEnumerable<CarAdsViewModel>> GetAdsByCriteriaAsync(SearchInputModel criteria);
    }
}
