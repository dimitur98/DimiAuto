namespace DimiAuto.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using DimiAuto.Common;
    using DimiAuto.Data.Common.Repositories;
    using DimiAuto.Models.CarModel;
    using DimiAuto.Services.Mapping;
    using DimiAuto.Web.ViewModels.Ad;
    using DimiAuto.Web.ViewModels.Home;
    using Microsoft.EntityFrameworkCore;

    public class HomeService : IHomeService
    {
        private readonly IDeletableEntityRepository<Car> carRepository;
        private readonly IAdService adService;
        private readonly IViewService viewService;

        public HomeService(IDeletableEntityRepository<Car> carRepository, IAdService adService, IViewService viewService)
        {
            this.carRepository = carRepository;
            this.adService = adService;
            this.viewService = viewService;
        }

        public async Task<ICollection<CarAdsViewModel>> GetAllAdsAsync()
        {
            var result = await this.carRepository.All().Where(x => x.IsApproved == true).Select(x => new CarAdsViewModel
            {
                Id = x.Id,
                Fuel = x.Fuel,
                ImgPath = GlobalConstants.CloudinaryPathDimitur98 + x.ImgsPaths
                            .Split(",", StringSplitOptions.RemoveEmptyEntries)
                            .First()
                            .ToString(),
                Km = x.Km,
                Make = x.Make,
                Model = x.Model,
                Modification = x.Modification,
                MoreInformation = x.MoreInformation.Length > 40 ? x.MoreInformation.Substring(0, 20) + "..." : x.MoreInformation,
                Price = x.Price,
                YearOfProduction = x.YearOfProduction,
                UserId = x.UserId,
                User = x.User,
                Gearbox = x.Gearbox,
                Condition = x.Condition,
                TypeOfVeichle = x.TypeOfVeichle,
                CreatedOn = x.CreatedOn,
                ModelToString = this.adService.EnumParser(x.Make.ToString(), x.Model),
            }).OrderByDescending(x => x.CreatedOn).ToListAsync();

            return result;
        }

        public async Task<ICollection<CarAdsViewModel>> GetAdsByCriteriaAsync(SearchInputModel criteria)
        {
            var result = await this.GetAllAdsAsync();
            var dic = criteria.GetType()
                        .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                        .ToDictionary(prop => prop.Name, prop => prop.GetValue(criteria, null));
            var make = dic["Make"].ToString();
            var model = dic["Model"] == null ? string.Empty : dic["Model"].ToString();
            var modelToString = this.adService.EnumParser(make, model);
            foreach (var item in dic)
            {
                if (item.Value != null && !item.Key.Contains("From") && !item.Key.Contains("To"))
                {
                    if (item.Value.ToString() != "All" && !(modelToString == "All" && item.Key.ToString() == "Model"))
                    {
                        result = result.Where(x => x.GetType().GetProperty(item.Key).GetValue(x, null).ToString() == item.Value.ToString()).ToList();
                    }
                }
                else
                {
                    if (item.Value != null && item.Key == "PriceFrom")
                    {
                        result = result.Where(x => x.Price >= criteria.PriceFrom).ToList();
                    }
                    else if (item.Value != null && item.Key == "PriceTo")
                    {
                        result = result.Where(x => x.Price <= criteria.PriceTo).ToList();
                    }

                    if (item.Value != null && item.Key == "YearFrom")
                    {
                        result = result.Where(x => x.YearOfProduction.Year >= criteria.YearFrom).ToList();
                    }
                    else if (item.Value != null && item.Key == "YearTo")
                    {
                        result = result.Where(x => x.YearOfProduction.Year <= criteria.YearTo).ToList();
                    }
                }
            }

            return result;
        }

        public async Task<ICollection<CarAdsViewModel>> GetCarsOfUserAsync(string userId)
        {
            var result = await this.carRepository.All().Where(x => x.IsApproved == true && x.UserId == userId).Select(x => new CarAdsViewModel
            {
                Id = x.Id,
                Fuel = x.Fuel,
                ImgPath = GlobalConstants.CloudinaryPathDimitur98 + x.ImgsPaths
                            .Split(",", StringSplitOptions.RemoveEmptyEntries)
                            .First()
                            .ToString(),
                Km = x.Km,
                Make = x.Make,
                Model = x.Model,
                Modification = x.Modification,
                MoreInformation = x.MoreInformation.Length > 40 ? x.MoreInformation.Substring(0, 20) + "..." : x.MoreInformation,
                Price = x.Price,
                YearOfProduction = x.YearOfProduction,
                UserId = x.UserId,
                User = x.User,
                Gearbox = x.Gearbox,
                Condition = x.Condition,
                TypeOfVeichle = x.TypeOfVeichle,
                CreatedOn = x.CreatedOn,
                ModelToString = this.adService.EnumParser(x.Make.ToString(), x.Model),
            }).OrderByDescending(x => x.CreatedOn).ToListAsync();

            return result;

        }

        public async Task<ICollection<MostWatchedUserCarViewModel>> GetTopFourMostWatchedCarsOfUserAsync(string userId)
        {
            var cars = await this.carRepository.All()
                .Where(x => x.UserId == userId && x.IsApproved == true)
                .ToListAsync();

            var result = cars.Select(x => new MostWatchedUserCarViewModel
            {
                Id = x.Id,
                ImgPath = GlobalConstants.CloudinaryPathDimitur98 + x.ImgsPaths
                           .Split(",", StringSplitOptions.RemoveEmptyEntries)
                           .First()
                           .ToString(),
                Make = x.Make,
                ModelToString = this.adService.EnumParser(x.Make.ToString(), x.Model),
                Modification = x.Modification,
                Views = this.viewService.GetViewsCount(x.Id),
                CreatedOn = x.CreatedOn,
            }).OrderByDescending(x => x.Views).ThenByDescending(x => x.CreatedOn).Take(4).ToList();

            return result;
        }

        public AllCarsModel Paging(AllCarsModel data, int? take = null, int skip = 0)
        {
            data.AllCars = data.AllCars.Skip(skip).ToList();
            if (take.HasValue)
            {
                data.AllCars = data.AllCars.Take(take.Value).ToList();
            }

            return data;
        }
    }
}
