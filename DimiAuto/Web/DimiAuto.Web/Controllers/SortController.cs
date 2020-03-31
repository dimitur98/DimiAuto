namespace DimiAuto.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using DimiAuto.Data.Common.Repositories;
    using DimiAuto.Models.CarModel;
    using DimiAuto.Services.Data;
    using DimiAuto.Web.ViewModels.Ad;
    using DimiAuto.Web.ViewModels.Sort;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [ApiController]
    [Route("api/[controller]")]
    public class SortController : ControllerBase
    {
        private readonly IHomeService homeService;
        private readonly IDeletableEntityRepository<Car> carRepository;

        public SortController(IHomeService homeService, IDeletableEntityRepository<Car> carRepository)
        {
            this.homeService = homeService;
            this.carRepository = carRepository;
        }

        [HttpPost]
        public async Task<ActionResult<AllCarsModel>> Sort(SortInputModel input)
        {
            
            var ads = await this.homeService.GetAllAdsAsync();
            if (input.OrderByYear == "1")
            {
                ads = ads.OrderBy(x => x.YearOfProduction).ToList();
            }
            else if (input.OrderByPrice == "2")
            {
                ads = ads.OrderByDescending(x => x.YearOfProduction).ToList();
            }

            if (input.OrderByPrice == "2")
            {
                ads = ads.OrderBy(x => x.Price).ToList();
            }
            else if (input.OrderByPrice == "1")
            {
                ads = ads.OrderByDescending(x => x.Price).ToList();
            }
            //var a = new List<CarAdsViewModel>();
            //foreach (var x in ads)
            //{
            //    var b = new CarAdsViewModel
            //    {
            //        Id = x.Id,
            //        Fuel = x.Fuel,
            //        ImgPath = this.carRepository
            //        .All()
            //        .FirstOrDefault(a => a.Id == x.Id)
            //        .ImgsPaths.Split(",", StringSplitOptions.RemoveEmptyEntries)
            //        .First()
            //        .ToString(),
            //        Km = x.Km,
            //        Make = x.Make,
            //        Model = x.Model,
            //        Modification = x.Modification,
            //        MoreInformation = x.MoreInformation.Length > 40 ? x.MoreInformation.Substring(0, 20) + "..." : x.MoreInformation,
            //        Price = x.Price,
            //        YearOfProduction = x.YearOfProduction.ToString(),
            //        UserId = x.UserId,
            //        User = x.User,
            //        GearBox = x.Gearbox,
            //        TypeOfVeichle = x.TypeOfVeichle,
            //        Condition = x.Condition,
            //    };
            //    a.Add(b);
            //}
            //AllCarsModel result = new AllCarsModel
            //{
            //    AllCars = ads,
            //};
            return new AllCarsModel { AllCars = ads };
        }
    }
}
