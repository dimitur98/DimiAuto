﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using DimiAuto.Data.Common.Repositories;
using DimiAuto.Services.Mapping;
using DimiAuto.Web.ViewModels.Ad;
using DimiAuto.Models.CarModel;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace DimiAuto.Services.Data
{
    public class AdService : IAdService
    {
        private readonly DbContext db;
        private readonly Cloudinary cloudinary;
        private readonly IDeletableEntityRepository<Car> carRepository;

        public AdService(Cloudinary cloudinary, IDeletableEntityRepository<Car> carRepository)
        {
            this.cloudinary = cloudinary;
            this.carRepository = carRepository;
        }

        public async Task<string> CreateAdAsync(CreateAdInputModel input, string userId)
        {
            if (input.Extras == null)
            {
                input.Extras = "No extras";
            }
            
            var car = new Car
            {
                Cc = input.Cc,
                Fuel = input.Fuel,
                Color = input.Color,
                Door = input.Door,
                EuroStandart = input.EuroStandart,
                Extras = input.Extras,
                Gearbox = input.GearBox,
                Horsepowers = input.Hp,
                Km = input.Km,
                Location = input.Location,
                Make = input.Make,
                Model = input.Model,
                Modification = input.Modification,
                MoreInformation = input.MoreInformation,
                Price = input.Price,
                Type = input.Type,
                Condition = input.Condition,
                YearOfProduction = input.YearOfProduction,
                UserId = userId,
            };
            await this.carRepository.AddAsync(car);
            await this.carRepository.SaveChangesAsync();
            return car.Id;
        }

       

        // Check for better code
        protected string GetFirstImgOnly(string carId)
        {
            return this.carRepository.All().FirstOrDefault(x => x.Id == carId).ImgsPaths.Split(",", StringSplitOptions.RemoveEmptyEntries).First().ToString();
        }

        public async Task<CarDetailsVIewModel> GetCurrentCarAsync(string carId)
        {
            var car = await this.carRepository.All().FirstOrDefaultAsync(x => "id=" + x.Id == carId);
            var output = new CarDetailsVIewModel
            {
                Cc = car.Cc,
                Color = car.Color,
                Door = car.Door,
                EuroStandart = car.EuroStandart,
                Extras = car.Extras.Split(",", StringSplitOptions.RemoveEmptyEntries).ToList(),
                Fuel = car.Fuel,
                Gearbox = car.Gearbox,
                Horsepowers = car.Horsepowers,
                ImgsPaths = car.ImgsPaths.Split(",",StringSplitOptions.RemoveEmptyEntries).ToList(),
                Km = car.Km,
                Location = car.Location,
                Make = car.Make,
                Model = car.Model,
                Modification = car.Modification,
                MoreInformation = car.MoreInformation,
                Price = car.Price,
                Type = car.Type,
                Condition = car.Condition,
                User = car.User,
                UserId = car.UserId,
                Views = car.Views,
                YearOfProduction = car.YearOfProduction.ToString("dd.MM.yyyy"),
            };
            return output;
        }
      
    }
}
