namespace DimiAuto.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection.Metadata;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using DimiAuto.Common;
    using DimiAuto.Data.Common.Repositories;
    using DimiAuto.Data.Models;
    using DimiAuto.Services.Data;
    using DimiAuto.Services.Mapping;
    using DimiAuto.Web.ViewModels.Ad;
    using DimiAuto.Web.ViewModels.Ad.Comment;
    using DimiAuto.Web.ViewModels.Ad.CompareAds;
    using FinalProject.Models.CarModel;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    public class AdController : Controller
    {
        private readonly IAdService adService;
        private readonly ICommentService commentService;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;

        public AdController(IAdService adService, ICommentService commentService, IDeletableEntityRepository<ApplicationUser> userRepository)
        {
            this.adService = adService;
            this.commentService = commentService;
            this.userRepository = userRepository;
        }

        [Authorize]
        public IActionResult CreateAd()
        {
            return this.View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateAd(CreateAdInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var id = await this.adService.CreateAdAsync(input, userId);

            return this.Redirect($"/Img/Upload/id={id}");
        }

        public async Task<IActionResult> Details(string id)
        {
           // var a = Assembly("All.cshtml");
            var car = await this.adService.GetCurrentCarAsync(id.Substring(3));
            if (car.ImgsPaths == string.Empty)
            {
                car.ImgsPaths = GlobalConstants.DefaultImgCar;
            }

            var user = await this.userRepository.AllWithDeleted().FirstOrDefaultAsync(x => x.Id == car.UserId);
            var output = new CarDetailsModel
            {
                CarDetailsVIewModel = new CarDetailsVIewModel
                {
                    Id = car.Id,
                    Cc = car.Cc,
                    Color = car.Color,
                    Door = car.Door,
                    EuroStandart = car.EuroStandart,
                    Extras = car.Extras.Split(",", StringSplitOptions.RemoveEmptyEntries).ToList(),
                    Fuel = car.Fuel,
                    Gearbox = car.Gearbox,
                    Horsepowers = car.Horsepowers,
                    ImgsPaths = car.ImgsPaths.Split(",", StringSplitOptions.RemoveEmptyEntries).ToList(),
                    Km = car.Km,
                    Location = car.Location,
                    Make = car.Make,
                    Model = car.Model,
                    Modification = car.Modification,
                    MoreInformation = car.MoreInformation,
                    Price = car.Price,
                    Type = car.Type,
                    Condition = car.Condition,
                    User = user,
                    UserId = car.UserId,
                    YearOfProduction = car.YearOfProduction.ToString("MM.yyyy"),
                    Comments = await this.commentService.GetComments<CarCommentViewModel>(car.Id),
                    IsApproved = car.IsApproved,
                    IsDeleted = car.IsDeleted,
                },
            };

            return this.View(output);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Details(string id, CarDetailsModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }
            
            input.CarCommentsInputModel.UserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            input.CarCommentsInputModel.CarId = id.Substring(3);
            await this.commentService.CreateAsync(input.CarCommentsInputModel);
            return this.RedirectToAction("Details", new { id });
        }

        [Authorize]
        public async Task<IActionResult> Edit(string id)
        {
            var car = await this.adService.GetCurrentCarAsync(id.Substring(3));
            var output = new EditAddInputModel
            {
                Cc = car.Cc,
                Color = car.Color,
                Door = car.Door,
                EuroStandart = car.EuroStandart,
                Extras = car.Extras,
                Fuel = car.Fuel,
                GearBox = car.Gearbox,
                Hp = car.Horsepowers,
                Km = car.Km,
                Location = car.Location,
                Make = car.Make,
                Model = car.Model,
                Modification = car.Modification,
                MoreInformation = car.MoreInformation,
                Price = car.Price,
                Type = car.Type,
                Condition = car.Condition,
                YearOfProduction = car.YearOfProduction,
            };
            return this.View(output);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(EditAddInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var car = await this.adService.EditAd(input);
            var output = new EditAddInputModel
            {
                Cc = car.Cc,
                Color = car.Color,
                Door = car.Door,
                EuroStandart = car.EuroStandart,
                Extras = car.Extras,
                Fuel = car.Fuel,
                GearBox = car.Gearbox,
                Hp = car.Horsepowers,
                Km = car.Km,
                Location = car.Location,
                Make = car.Make,
                Model = car.Model,
                Modification = car.Modification,
                MoreInformation = car.MoreInformation,
                Price = car.Price,
                Type = car.Type,
                Condition = car.Condition,
                YearOfProduction = car.YearOfProduction,
            };
            return this.Redirect("/MyAccount/MyAccount");
        }

        [Authorize]
        public async Task<IActionResult> Compare(AllCarsModel input)
        {
            var firstCar = await this.adService.GetCurrentCarAsync(input.CompareCarsInputModel.FirstCarId);
            var secondCar = await this.adService.GetCurrentCarAsync(input.CompareCarsInputModel.SecondCarId);

            var output = new ChoosenCarsForCompareViewModel
            {
                FirstCar = new ComparedCarViewModel
                {
                    Cc = firstCar.Cc,
                    Color = firstCar.Color,
                    Door = firstCar.Door,
                    EuroStandart = firstCar.EuroStandart,
                    Extras = firstCar.Extras.Split(",", StringSplitOptions.RemoveEmptyEntries).ToList(),
                    Fuel = firstCar.Fuel,
                    Gearbox = firstCar.Gearbox,
                    Horsepowers = firstCar.Horsepowers,
                    ImgPath = firstCar.ImgsPaths.Split(",", StringSplitOptions.RemoveEmptyEntries).First().ToString(),
                    Km = firstCar.Km,
                    Make = firstCar.Make,
                    Model = firstCar.Model,
                    Modification = firstCar.Modification,
                    Price = firstCar.Price,
                    Type = firstCar.Type,
                    Condition = firstCar.Condition,
                    YearOfProduction = firstCar.YearOfProduction.ToString("MM.yyyy"),
                },
                SecondCar = new ComparedCarViewModel
                {
                    Cc = secondCar.Cc,
                    Color = secondCar.Color,
                    Door = secondCar.Door,
                    EuroStandart = secondCar.EuroStandart,
                    Extras = secondCar.Extras.Split(",", StringSplitOptions.RemoveEmptyEntries).ToList(),
                    Fuel = secondCar.Fuel,
                    Gearbox = secondCar.Gearbox,
                    Horsepowers = secondCar.Horsepowers,
                    ImgPath = secondCar.ImgsPaths.Split(",", StringSplitOptions.RemoveEmptyEntries).First().ToString(),
                    Km = secondCar.Km,
                    Make = secondCar.Make,
                    Model = secondCar.Model,
                    Modification = secondCar.Modification,
                    Price = secondCar.Price,
                    Type = secondCar.Type,
                    Condition = secondCar.Condition,
                    YearOfProduction = secondCar.YearOfProduction.ToString("MM.yyyy"),
                },
            };
            return this.View(output);
        }
    }
}
