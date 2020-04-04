namespace DimiAuto.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using DimiAuto.Common;
    using DimiAuto.Data.Models;
    using DimiAuto.Services.Data;
    using DimiAuto.Web.ViewModels.Ad;
    using DimiAuto.Web.ViewModels.FavoriteAds;
    using DimiAuto.Web.ViewModels.Img;
    using DimiAuto.Web.ViewModels.MyAccount;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class MyAccountController : Controller
    {
        private readonly IMyAccountService myAccountService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IAdService adService;
        private readonly IImgService imgService;

        public MyAccountController(IMyAccountService myAccountService, UserManager<ApplicationUser> userManager, IAdService adService, IImgService imgService)
        {
            this.myAccountService = myAccountService;
            this.userManager = userManager;
            this.adService = adService;
            this.imgService = imgService;
        }

        public async Task<IActionResult> MyAccount()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await this.userManager.GetUserAsync(this.User);
            var myCars = await this.myAccountService.GetMyCarsAsync<MyCarsViewModel>(userId);
            var result = new MyAccountViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                MyCars = myCars,
            };
            return this.View(result);
        }

        public async Task<IActionResult> AccountInfo()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var result = new ChangePersonalInfoInputModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Adress = user.Adress,
                City = user.City,
                NameOfCompany = user.NameOfCompany,

                PhoneNumber = user.PhoneNumber,
            };
            return this.View(result);
        }

        [HttpPost]
        public async Task<IActionResult> AccountInfo(ChangePersonalInfoInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);
            user.FirstName = input.FirstName;
            user.LastName = input.LastName;
            user.Adress = input.Adress;
            user.City = input.City;
            user.PhoneNumber = input.PhoneNumber;
            if (input.NameOfCompany != GlobalConstants.PrivatePerson)
            {
                user.NameOfCompany = input.NameOfCompany;
            }

            await this.userManager.UpdateAsync(user);
            return this.RedirectToAction("MyAccount");
        }

        public async Task<IActionResult> ChangeAvatar()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var output = new ChangeAvatarModel
            {
                ChangeAvatarViewModel = new ChangeAvatarViewModel
                {
                    ImgPath = user.UserImg,
                },
            };
            return this.View(output);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeAvatar(ImgUploadInputModel input)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            user.UserImg = string.Empty;
            var result = await this.imgService.UploadImgsAsync(input);
            user.UserImg = result.First();
            await this.userManager.UpdateAsync(user);
            return this.Redirect("MyAccount");
        }

        public async Task<IActionResult> Favorites()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var favorites = await this.adService.GetAllFavAdsOnCurrentUserAsync<UserAdViewModel>(userId);
            var cars = new List<CarAdsViewModel>();
            foreach (var favorite in favorites)
            {
                var car = new CarAdsViewModel
                {
                    Id = favorite.Car.Id,
                    Fuel = favorite.Car.Fuel,
                    ImgPath = favorite.Car.ImgsPaths.Split(",", StringSplitOptions.RemoveEmptyEntries).First().ToString(),
                    Km = favorite.Car.Km,
                    Make = favorite.Car.Make,
                    Model = favorite.Car.Model,
                    Modification = favorite.Car.Modification,
                    MoreInformation = favorite.Car.MoreInformation.Length > 40 ? favorite.Car.MoreInformation.Substring(0, 20) + "..." : favorite.Car.MoreInformation,
                    Price = favorite.Car.Price,
                    YearOfProduction = favorite.Car.YearOfProduction,
                    UserId = favorite.UserId,
                    User = favorite.User,
                    GearBox = favorite.Car.Gearbox,
                    Condition = favorite.Car.Condition,
                    TypeOfVeichle = favorite.Car.TypeOfVeichle,
                };
                cars.Add(car);
            }

            var output = new AllCarsModel
            {
                AllCars = cars,
            };
            return this.View(output);
        }
    }
}
