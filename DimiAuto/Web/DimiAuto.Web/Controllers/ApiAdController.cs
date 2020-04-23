namespace DimiAuto.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using DimiAuto.Data.Common.Repositories;
    using DimiAuto.Data.Models;
    using DimiAuto.Data.Models.CarModel;
    using DimiAuto.Models.CarModel;
    using DimiAuto.Services.Data;
    using DimiAuto.Web.ViewModels.Ad;
    using DimiAuto.Web.ViewModels.Ad.ApiController;
    using DimiAuto.Web.ViewModels.Ad.CompareAds;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ApiAdController : Controller
    {
        private readonly IMyAccountService myAccountService;
        private readonly IAdService adService;
        private readonly IDeletableEntityRepository<UserCarFavorite> favouriteRepository;
        private readonly IDeletableEntityRepository<Car> carRepository;

        public ApiAdController(IMyAccountService myAccountService, IAdService adService, IDeletableEntityRepository<UserCarFavorite> favouriteRepository, IDeletableEntityRepository<Car> carRepository)
        {
            this.myAccountService = myAccountService;
            this.adService = adService;
            this.favouriteRepository = favouriteRepository;
            this.carRepository = carRepository;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<string>> AddToFav(ApiInputModel input)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var record = await this.favouriteRepository.All().FirstOrDefaultAsync(x => x.CarId == input.CarId && x.UserId == userId);
            if (record == null)
            {
                await this.myAccountService.AddAdToFavAsync(input.CarId, userId);
                return this.Ok(new { output = "added" });
            }

            return this.Ok(new { output = "already added" });
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<string>> RemoveFavAd(ApiInputModel input)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var record = await this.favouriteRepository.All().FirstOrDefaultAsync(x => x.CarId == input.CarId && x.UserId == userId);
            if (record != null)
            {
                await this.myAccountService.RemoveFavAdAsync(input.CarId, userId);
                return this.Ok(new { output = "removed", carId = input.CarId });
            }

            return this.Ok();
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<string>> AddCarForCompare(ApiInputModel input)
        {
            var car = await this.carRepository.All().FirstOrDefaultAsync(x => x.Id == input.CarId);
            var carViewModel = new ComparedCarViewModel
            {
                Cc = car.Cc,
                Color = car.Color,
                Door = car.Door,
                EuroStandart = car.EuroStandart,
                Extras = car.Extras.Split(",", StringSplitOptions.RemoveEmptyEntries).ToList(),
                Fuel = car.Fuel,
                Gearbox = car.Gearbox,
                Horsepowers = car.Horsepowers,
                ImgPath = car.ImgsPaths.Split(",", StringSplitOptions.RemoveEmptyEntries).First().ToString(),
                Km = car.Km,
                Make = car.Make,
                Model = car.Model,
                Modification = car.Modification,
                Price = car.Price,
                Type = car.Type,
                Condition = car.Condition,
                YearOfProduction = car.YearOfProduction.ToString("MM.yyyy"),
            };
            if (this.ViewData["firstCar"] == null)
            {
                this.ViewData["firstCar"] = carViewModel;
                this.HttpContext.Session.SetString("carModel", carViewModel.Model);

                return this.Ok(new { result = "firstCar" });
            }
            else if (this.ViewData["secondCar"] == null)
            {
                this.ViewData["secondCar"] = carViewModel;
                return this.Ok(new { result = "secondCar" });
            }
            else
            {
                return this.Ok(new { result = "full" });
            }
        }

        [HttpPost]
        public ActionResult<string> LoadMakeModels(LoadModelInput input)
        {
            if (input.Make == "All")
            {
                return this.Ok(new { models = "-" });
            }

            var modelClass = typeof(Models);
            var modelEnum = modelClass.GetNestedType(input.Make.Replace(" ", string.Empty));
            var models = modelEnum.GetEnumNames().ToList();
            return this.Ok(new { models = models });
        }
    }
}
