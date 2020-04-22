namespace DimiAuto.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using DimiAuto.Data.Models;
    using DimiAuto.Models.CarModel;
    using DimiAuto.Web.ViewModels.Ad;
    using Microsoft.AspNetCore.Http;

    public interface IAdService
    {
        Task<string> CreateAdAsync(CreateAdInputModel input, string userId);

        Task<Car> GetCurrentCarAsync(string carId);

        Task<Car> EditAd(EditAddInputModel input);

        Task AddAdToFavAsync(string carId, string userId);

        Task RemoveFavAdAsync(string carId, string userId);

        Task<ICollection<TModel>> GetAllFavAdsOnCurrentUserAsync<TModel>(string userId);

        string EnumParser(string make, string model);

    }
}
