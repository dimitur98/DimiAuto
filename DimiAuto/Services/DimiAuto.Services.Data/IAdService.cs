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

        string EnumParser(string make, string model);

        Task UpdateCarRecordAsync(Car car);
    }
}
