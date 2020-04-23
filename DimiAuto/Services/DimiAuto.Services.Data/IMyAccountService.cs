namespace DimiAuto.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using DimiAuto.Models.CarModel;
    using DimiAuto.Web.ViewModels.MyAccount;

    public interface IMyAccountService
    {
        Task<ICollection<Car>> GetMyCarsAsync(string userId);

        Task DeleteAccountDataAsync(string userId);

        Task AddAdToFavAsync(string carId, string userId);

        Task RemoveFavAdAsync(string carId, string userId);

        Task<ICollection<TModel>> GetAllFavAdsOnCurrentUserAsync<TModel>(string userId);
    }
}
