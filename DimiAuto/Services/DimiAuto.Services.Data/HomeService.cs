namespace DimiAuto.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using DimiAuto.Data.Common.Repositories;
    using DimiAuto.Models.CarModel;
    using DimiAuto.Services.Mapping;
    using DimiAuto.Web.ViewModels.Home;
    using DimiAuto.Web.ViewModels.Ad;
    using Microsoft.EntityFrameworkCore;
    using DimiAuto.Common;

    public class HomeService : IHomeService
    {
        private readonly IDeletableEntityRepository<Car> carRepository;
        private readonly IAdService adService;

        public HomeService(IDeletableEntityRepository<Car> carRepository, IAdService adService)
        {
            this.carRepository = carRepository;
            this.adService = adService;
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
                GearBox = x.Gearbox,
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
            foreach (var item in dic)
            {
                if (item.Value != null && !item.Key.Contains("From") && !item.Key.Contains("To"))
                {
                    if (item.Value.ToString() != "All")
                    {
                        result = result.Where(x => x.GetType().GetProperty(item.Key).GetValue(x, null).ToString() == item.Value.ToString()).ToList();
                    }
                }

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
