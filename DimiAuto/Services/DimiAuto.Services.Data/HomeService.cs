namespace DimiAuto.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using DimiAuto.Data.Common.Repositories;
    using DimiAuto.Models.CarModel;
    using DimiAuto.Web.ViewModels;
    using DimiAuto.Web.ViewModels.Ad;

    public class HomeService : IHomeService
    {
        private readonly IDeletableEntityRepository<Car> carRepository;

        public HomeService(IDeletableEntityRepository<Car> carRepository)
        {
            this.carRepository = carRepository;
        }

        public IEnumerable<CarAdsViewModel> GetAllAds()
        {
            var result = this.carRepository.All().Select(x => new CarAdsViewModel
            {
                Id = x.Id,
                Fuel = x.Fuel,
                ImgPath = this.carRepository
                    .All()
                    .FirstOrDefault(a => a.Id == x.Id)
                    .ImgsPaths.Split(",", StringSplitOptions.RemoveEmptyEntries)
                    .First()
                    .ToString(),
                Km = x.Km,
                Make = x.Make,
                Model = x.Model,
                Modification = x.Modification,
                MoreInformation = x.MoreInformation.Length > 40 ? x.MoreInformation.Substring(0, 20) + "..." : x.MoreInformation,
                Price = x.Price,
                YearOfProduction = x.YearOfProduction.ToString("dd.mm.yyyy"),
                UserId = x.UserId,
                User = x.User,
                GearBox = x.Gearbox,
                Condition = x.Condition,
                TypeOfVeichle = x.TypeOfVeichle,
            }).ToList();

            return result;
        }

        public IEnumerable<CarAdsViewModel> GetAdsByCriteria(SearchInputModel criteria)
        {

            var result = this.GetAllAds().ToList();
            var dic = criteria.GetType()
                        .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                        .ToDictionary(prop => prop.Name, prop => prop.GetValue(criteria, null));
            foreach (var item in dic)
            {
                if (item.Value != null && !item.Key.Contains("From") && !item.Key.Contains("To"))
                {
                    result = result.Where(x => x.GetType().GetProperty(item.Key).GetValue(x, null).ToString() == item.Value.ToString()).ToList();
                }


                if (item.Value != null && item.Key == "PriceFrom")
                {
                    result = result.Where(x => x.Price >= criteria.PriceFrom).ToList();
                }
                else if (item.Value != null && item.Key == "PriceTo")
                {
                    result = result.Where(x => x.Price < criteria.PriceTo).ToList();
                }

                if (item.Value != null && item.Key == "YearFrom")
                {
                    result = result.Where(x => DateTime.Parse(x.YearOfProduction).Year >= DateTime.Parse(criteria.YearFrom).Year).ToList();
                }
                else if (item.Value != null && item.Key == "YearTo")
                {
                    result = result.Where(x => DateTime.Parse(x.YearOfProduction).Year < DateTime.Parse(criteria.YearTo).Year).ToList();
                }

            }

            result = result.Select(x => new CarAdsViewModel
            {
                Id = x.Id,
                Fuel = x.Fuel,
                ImgPath = this.carRepository
                    .All()
                    .FirstOrDefault(a => a.Id == x.Id)
                    .ImgsPaths.Split(",", StringSplitOptions.RemoveEmptyEntries)
                    .First()
                    .ToString(),
                Km = x.Km,
                Make = x.Make,
                Model = x.Model,
                Modification = x.Modification,
                MoreInformation = x.MoreInformation.Length > 40 ? x.MoreInformation.Substring(0, 20) + "..." : x.MoreInformation,
                Price = x.Price,
                YearOfProduction = x.YearOfProduction.ToString(),
                UserId = x.UserId,
                User = x.User,
                GearBox = x.GearBox,
                TypeOfVeichle = x.TypeOfVeichle,
                Condition = x.Condition,
            }).ToList();
            return result;
        }
    }

}
