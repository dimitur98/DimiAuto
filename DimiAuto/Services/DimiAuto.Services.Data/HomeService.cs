namespace DimiAuto.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using DimiAuto.Data.Common.Repositories;
    using DimiAuto.Models.CarModel;
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
            }).ToList();
            return result;
        }

        public IEnumerable<FourMostViewAdCarsViewModel> GetTopFourViewsAd()
        {
            return this.carRepository.All().Take(4).Select(x => new FourMostViewAdCarsViewModel
            {
                Id = x.Id,
                ImgPad = this.carRepository
                    .All()
                    .FirstOrDefault(a => a.Id == x.Id)
                    .ImgsPaths.Split(",", StringSplitOptions.RemoveEmptyEntries)
                    .First()
                    .ToString(),
                Make = x.Make,
                Model = x.Model,
                Modification = x.Modification,
                Price = x.Price,
                UserId = x.UserId,
                Year = x.YearOfProduction.ToString("dd.mm.yyyy"),
                Fuel = x.Fuel,
            }).ToList();
        }
    }
}
