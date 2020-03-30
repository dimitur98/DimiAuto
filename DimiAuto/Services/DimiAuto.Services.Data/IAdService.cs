using CloudinaryDotNet;
using DimiAuto.Data.Models;
using DimiAuto.Models.CarModel;
using DimiAuto.Web.ViewModels.Ad;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DimiAuto.Services.Data
{
    public interface IAdService
    {
        Task<string> CreateAdAsync(CreateAdInputModel input, string userId);

        Task<Car> GetCurrentCarAsync(string carId);

        Task<Car> EditAd(EditAddInputModel input);

        Task AddAdToFavAsync(string carId, string userId);

        Task RemoveFavAdAsync(string carId, string userId);

        Task<IEnumerable<TModel>> GetAllFavAdsOnCurrentUserAsync<TModel>(string userId);

    }
}
