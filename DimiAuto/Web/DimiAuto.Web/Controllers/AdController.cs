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
    using DimiAuto.Data.Models.CarModel;
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
    using Microsoft.Extensions.Caching.Distributed;

    public class AdController : Controller
    {
        private readonly IAdService adService;
        private readonly ICommentService commentService;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        private readonly IViewService viewService;
        private readonly IDistributedCache distributedCache;

        public AdController(IAdService adService, ICommentService commentService, IDeletableEntityRepository<ApplicationUser> userRepository, IViewService viewService, IDistributedCache distributedCache)
        {
            this.adService = adService;
            this.commentService = commentService;
            this.userRepository = userRepository;
            this.viewService = viewService;
            this.distributedCache = distributedCache;
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

            return this.Redirect($"/Img/Upload?id={id}");
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return this.View("Error");
            }

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var ip = this.HttpContext.Connection.ToString();
            var a = this.HttpContext.Connection.RemoteIpAddress.ToString();
            this.ViewData["ip"] = this.HttpContext.Request.Cookies["myId"].ToString();

           // var a = Assembly("All.cshtml");
            var car = await this.adService.GetCurrentCarAsync(id);
            if (userId == null)
            {
                var uniqCarId = car.Make + car.Model + car.UserId + car.CreatedOn.ToString();
                if (this.HttpContext.Request.Cookies["myId"] == null || !this.HttpContext.Request.Cookies["myId"].Contains(uniqCarId))
                {

                    var myId = Guid.NewGuid().ToString() + uniqCarId;
                    this.HttpContext.Response.Cookies.Append("myId", myId);
                    var a = this.HttpContext.Response.Cookies("myId")
                    await this.viewService.AddViewAsync("notRegisterUser", car.Id);
                }
            }
            else
            {
                await this.viewService.AddViewAsync(userId, car.Id);
            }

            if (car.ImgsPaths == string.Empty)
            {
                car.ImgsPaths = GlobalConstants.DefaultImgCar;
            }
            var user = await this.userRepository.AllWithDeleted().FirstOrDefaultAsync(x => x.Id == car.UserId);

            //var a = new CarDetailsModel { CarDetailsVIewModel = new CarDetailsVIewModel { Id = car.Id, }, };
            //var b = new CarDetailsModel{ CarDetailsVIewModel = new CarDetailsVIewModel{ Cc = car.Cc, }, };
            //var c = new CarDetailsModel{ CarDetailsVIewModel = new CarDetailsVIewModel{ Color = car.Color, }, };
            //var d = new CarDetailsModel{ CarDetailsVIewModel = new CarDetailsVIewModel{ Door = car.Door, }, };
            //var e = new CarDetailsModel{ CarDetailsVIewModel = new CarDetailsVIewModel{ EuroStandart = car.EuroStandart, }, };
            //var ad = new CarDetailsModel{ CarDetailsVIewModel = new CarDetailsVIewModel{ Extras = car.Extras.Split(",", StringSplitOptions.RemoveEmptyEntries).ToList(), }, };
            //var ae = new CarDetailsModel{ CarDetailsVIewModel = new CarDetailsVIewModel{ Fuel = car.Fuel, }, };
            //var aw = new CarDetailsModel{ CarDetailsVIewModel = new CarDetailsVIewModel{ Gearbox = car.Gearbox, }, };
            //var ar = new CarDetailsModel{ CarDetailsVIewModel = new CarDetailsVIewModel{ Horsepowers = car.Horsepowers, }, };
            //var av = new CarDetailsModel{ CarDetailsVIewModel = new CarDetailsVIewModel{ ImgsPaths = car.ImgsPaths.Split(",", StringSplitOptions.RemoveEmptyEntries).ToList(), }, };
            //var ac = new CarDetailsModel{ CarDetailsVIewModel = new CarDetailsVIewModel{ Km = car.Km, }, };
            //var ax = new CarDetailsModel{ CarDetailsVIewModel = new CarDetailsVIewModel{ Location = car.Location, }, };
            //var az = new CarDetailsModel{ CarDetailsVIewModel = new CarDetailsVIewModel{ Make = car.Make, }, };
            //var ag = new CarDetailsModel{ CarDetailsVIewModel = new CarDetailsVIewModel{ Model = car.Model, }, };
            //var ah = new CarDetailsModel{ CarDetailsVIewModel = new CarDetailsVIewModel{ Modification = car.Modification, }, };
            //var aj = new CarDetailsModel{ CarDetailsVIewModel = new CarDetailsVIewModel{ MoreInformation = car.MoreInformation, }, };
            //var ak = new CarDetailsModel{ CarDetailsVIewModel = new CarDetailsVIewModel{ Price = car.Price, }, };
            //var al = new CarDetailsModel{ CarDetailsVIewModel = new CarDetailsVIewModel{ Type = car.Type, }, };
            //var ao = new CarDetailsModel{ CarDetailsVIewModel = new CarDetailsVIewModel{ Condition = car.Condition, }, };
            //var ba = new CarDetailsModel{ CarDetailsVIewModel = new CarDetailsVIewModel{ User = user, }, };
            //var bd = new CarDetailsModel{ CarDetailsVIewModel = new CarDetailsVIewModel{ UserId = car.UserId, }, };
            //var be = new CarDetailsModel{ CarDetailsVIewModel = new CarDetailsVIewModel{ YearOfProduction = car.YearOfProduction.ToString("MM.yyyy"), }, };
            //var bw = new CarDetailsModel{ CarDetailsVIewModel = new CarDetailsVIewModel{ Comments = await this.commentService.GetComments<CarCommentViewModel>(car.Id), }, };
            //var bq = new CarDetailsModel{ CarDetailsVIewModel = new CarDetailsVIewModel{ Views = this.viewService.GetViewsCount(id), }, };
            //var bc = new CarDetailsModel{ CarDetailsVIewModel = new CarDetailsVIewModel{ IsApproved = car.IsApproved, }, };
            //var bx = new CarDetailsModel{ CarDetailsVIewModel = new CarDetailsVIewModel{ IsDeleted = car.IsDeleted, }, };
            //var bz = new CarDetailsModel{ CarDetailsVIewModel = new CarDetailsVIewModel{ CurrentUserId = userId, }, };
            //var bv = new CarDetailsModel{ CarDetailsVIewModel = new CarDetailsVIewModel{ ModelToString = this.adService.EnumParser(car.Make.ToString(), car.Model), }, };
            var output = new CarDetailsModel
            {
                CarDetailsVIewModel = new CarDetailsVIewModel
                {
                    Id = car.Id,
                    Cc = car.Cc,
                    Color = car.Color,
                    Door = car.Door,
                    EuroStandart = car.EuroStandart,
                    Extras = car.Extras == null ? new List<string>() : car.Extras.Split(",", StringSplitOptions.RemoveEmptyEntries).ToList(),
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
                    Views = this.viewService.GetViewsCount(id),
                    IsApproved = car.IsApproved,
                    IsDeleted = car.IsDeleted,
                    CurrentUserId = userId,
                    ModelToString = this.adService.EnumParser(car.Make.ToString(), car.Model),
                },
            };

            return this.View(output);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Details(CarDetailsModel input)
        {
            input.CarCommentsInputModel.UserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            await this.commentService.CreateAsync(input.CarCommentsInputModel);
            return this.RedirectToAction("Details", new { id = input.CarCommentsInputModel.CarId });
        }

        [Authorize]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return this.View("Error");
            }

            var car = await this.adService.GetCurrentCarAsync(id);
            var output = new EditAddInputModel
            {
                Cc = car.Cc,
                Color = car.Color,
                Door = car.Door,
                EuroStandart = car.EuroStandart,
                Extras = car.Extras,
                Fuel = car.Fuel,
                Gearbox = car.Gearbox,
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
                YearOfProduction = car.YearOfProduction.ToString("mm.yyyy"),
                Id = car.Id,
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

            // var output = new EditAddInputModel
            // {
            //    Cc = car.Cc,
            //    Color = car.Color,
            //    Door = car.Door,
            //    EuroStandart = car.EuroStandart,
            //    Extras = car.Extras,
            //    Fuel = car.Fuel,
            //    Gearbox = car.Gearbox,
            //    Hp = car.Horsepowers,
            //    Km = car.Km,
            //    Location = car.Location,
            //    Make = car.Make,
            //    Model = car.Model,
            //    Modification = car.Modification,
            //    MoreInformation = car.MoreInformation,
            //    Price = car.Price,
            //    Type = car.Type,
            //    Condition = car.Condition,
            //    YearOfProduction = car.YearOfProduction,
            // };
            return this.Redirect("/MyAccount/MyAccount");
        }

        [Authorize]
        public async Task<IActionResult> Compare(AllCarsModel input)
        {
            var firstCar = await this.adService.GetCurrentCarAsync(input.CompareCarsInputModel.FirstCarId);
            var secondCar = await this.adService.GetCurrentCarAsync(input.CompareCarsInputModel.SecondCarId);

            if (firstCar == null || secondCar == null)
            {
                return this.View("Error");
            }

            var output = new ChoosenCarsForCompareViewModel
            {
                FirstCar = new ComparedCarViewModel
                {
                    Cc = firstCar.Cc,
                    Color = firstCar.Color,
                    Door = firstCar.Door,
                    EuroStandart = firstCar.EuroStandart,
                    Extras = firstCar.Extras == null ? new List<string>() : firstCar.Extras.Split(",", StringSplitOptions.RemoveEmptyEntries).ToList(),
                    Fuel = firstCar.Fuel,
                    Gearbox = firstCar.Gearbox,
                    Horsepowers = firstCar.Horsepowers,
                    ImgPath = GlobalConstants.CloudinaryPathDimitur98 + firstCar.ImgsPaths.Split(",", StringSplitOptions.RemoveEmptyEntries).First().ToString(),
                    Km = firstCar.Km,
                    Make = firstCar.Make,
                    Model = firstCar.Model,
                    Modification = firstCar.Modification,
                    Price = firstCar.Price,
                    Type = firstCar.Type,
                    Condition = firstCar.Condition,
                    YearOfProduction = firstCar.YearOfProduction.ToString("MM.yyyy"),
                    ModelToString = this.adService.EnumParser(firstCar.Make.ToString(), firstCar.Model),
                },
                SecondCar = new ComparedCarViewModel
                {
                    Cc = secondCar.Cc,
                    Color = secondCar.Color,
                    Door = secondCar.Door,
                    EuroStandart = secondCar.EuroStandart,
                    Extras = secondCar.Extras == null ? new List<string>() : secondCar.Extras.Split(",", StringSplitOptions.RemoveEmptyEntries).ToList(),
                    Fuel = secondCar.Fuel,
                    Gearbox = secondCar.Gearbox,
                    Horsepowers = secondCar.Horsepowers,
                    ImgPath = GlobalConstants.CloudinaryPathDimitur98 + secondCar.ImgsPaths.Split(",", StringSplitOptions.RemoveEmptyEntries).First().ToString(),
                    Km = secondCar.Km,
                    Make = secondCar.Make,
                    Model = secondCar.Model,
                    Modification = secondCar.Modification,
                    Price = secondCar.Price,
                    Type = secondCar.Type,
                    Condition = secondCar.Condition,
                    YearOfProduction = secondCar.YearOfProduction.ToString("MM.yyyy"),
                    ModelToString = this.adService.EnumParser(secondCar.Make.ToString(), secondCar.Model),
                },
            };
            return this.View(output);
        }

        [Authorize]
        public async Task<IActionResult> DeleteComment(string commentId, string carId)
        {
            await this.commentService.DeleteCommentAsync(commentId);
            return this.RedirectToAction("Details", new { id = carId });
        }
    }
}
