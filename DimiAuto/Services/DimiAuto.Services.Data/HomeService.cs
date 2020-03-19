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
            }).ToList();
            return result;
        }

        public IEnumerable<CarAdsViewModel> GetAdsByCriteria(SearchInputModel criteria)
        {

            var result = this.GetAllAds();
            var dic = criteria.GetType()
                        .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                        .ToDictionary(prop => prop.Name, prop => prop.GetValue(criteria, null));
            foreach (var item in dic)
            {
                if (item.Value == null)
                {
                    dic.Remove(item.Key);
                }
            }

           
            return result.Where(x => x.Model == criteria.Model);
        }
}
}
