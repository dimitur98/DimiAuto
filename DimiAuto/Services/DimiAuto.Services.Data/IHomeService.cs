namespace DimiAuto.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using DimiAuto.Models.CarModel;
    using DimiAuto.Web.ViewModels.Ad;
    using DimiAuto.Web.ViewModels.Home;

    public interface IHomeService
    {
        Task<ICollection<CarAdsViewModel>> GetAllAdsAsync();

        Task<ICollection<CarAdsViewModel>> GetAdsByCriteriaAsync(SearchInputModel criteria);

        AllCarsModel Paging(AllCarsModel data, int? take = null, int skip = 0);

        Task<ICollection<CarAdsViewModel>> GetCarsOfUserAsync(string userId);

        Task<ICollection<MostWatchedUserCarViewModel>> GetTopFourMostWatchedCarsOfUserAsync(string userId);
    }
}
