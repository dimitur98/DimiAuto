namespace DimiAuto.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using DimiAuto.Common;
    using DimiAuto.Data;
    using DimiAuto.Data.Common.Repositories;
    using DimiAuto.Data.Models;
    using DimiAuto.Data.Models.CarModel;
    using DimiAuto.Models.CarModel;
    using DimiAuto.Services.Mapping;
    using DimiAuto.Web.ViewModels.Ad;
    using DimiAuto.Web.ViewModels.Ad.Comment;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;

    public class AdService : IAdService
    {
        private readonly IDeletableEntityRepository<Car> carRepository;
        private readonly IDeletableEntityRepository<UserCarFavorite> favouriteRepository;

        public AdService(IDeletableEntityRepository<Car> carRepository)
        {
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
                Gearbox = input.Gearbox,
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
                YearOfProduction = DateTime.ParseExact(input.YearOfProduction, "mm.yyyy", CultureInfo.InvariantCulture),
                UserId = userId,
                ImgsPaths = GlobalConstants.DefaultImgCar,
            };
            await this.carRepository.AddAsync(car);
            await this.carRepository.SaveChangesAsync();
            return car.Id;
        }

        public async Task<Car> GetCurrentCarAsync(string carId)
        {
            var car = await this.carRepository.AllWithDeleted().FirstOrDefaultAsync(x => x.Id == carId);
            if (car == null)
            {
                throw new NullReferenceException();
            }

            return car;
        }

        public async Task<Car> EditAd(EditAddInputModel input)
        {
            var car = await this.carRepository.All().FirstOrDefaultAsync(x => x.Id == input.Id);
            if (car == null)
            {
                throw new NullReferenceException();
            }

            car.Horsepowers = input.Hp;
            car.Cc = input.Cc;
            car.Color = input.Color;
            car.Condition = input.Condition;
            car.Door = input.Door;
            car.EuroStandart = input.EuroStandart;
            car.Extras = input.Extras;
            car.Fuel = input.Fuel;
            car.Gearbox = input.Gearbox;
            car.Km = input.Km;
            car.Location = input.Location;
            car.Make = input.Make;
            car.Model = input.Model;
            car.Modification = input.Modification;
            car.MoreInformation = input.MoreInformation;
            car.Price = input.Price;
            car.Type = input.Type;
            car.TypeOfVeichle = input.TypeOfVeichle;
            car.YearOfProduction = DateTime.ParseExact(input.YearOfProduction, "mm.yyyy", CultureInfo.InvariantCulture);
            this.carRepository.Update(car);
            await this.carRepository.SaveChangesAsync();
            return car;
        }

        public string EnumParser(string make, string model)
        {
            if (make != "All" && model != string.Empty && model != "-")
            {
                var modelNum = int.Parse(model) - 1;
                var modelClass = typeof(Models);
                var modelEnum = modelClass.GetNestedType(make);
                var models = modelEnum.GetEnumNames();
                var modelName = models[modelNum];
                if (modelName[0] == '_')
                {
                    modelName = modelName.Substring(1);
                }

                return modelName;
            }

            return "-";
        }
    }
}
