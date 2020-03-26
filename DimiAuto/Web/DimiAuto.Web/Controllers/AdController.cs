namespace DimiAuto.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection.Metadata;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using DimiAuto.Services.Data;
    using DimiAuto.Services.Mapping;
    using DimiAuto.Web.ViewModels.Ad;
    using DimiAuto.Web.ViewModels.Ad.Comment;
    using FinalProject.Models.CarModel;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    public class AdController : Controller
    {
        private readonly IAdService adService;
        private readonly ICommentService commentService;

        public AdController(IAdService adService, ICommentService commentService)
        {
            this.adService = adService;
            this.commentService = commentService;
        }

        public IActionResult CreateAd()
        {
            return this.View();
        }

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
                car.ImgsPaths = "'~/images/default-image-png-18-original.png'";
            }

            var output = new CarDetailsModel
            {
                CarDetailsVIewModel = new CarDetailsVIewModel
                {
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
                    User = car.User,
                    UserId = car.UserId,
                    Views = car.Views,
                    YearOfProduction = car.YearOfProduction.ToString("dd.MM.yyyy"),
                    Comments = await this.commentService.GetComments<CarCommentViewModel>(car.Id),
                },
            };
            
            return this.View(output);
        }

        [HttpPost]
        public async Task<IActionResult> Details(string id, CarDetailsModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }
            input.CarCommentsInputModel.UserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            input.CarCommentsInputModel.CarId = id.Substring(3);
            await this.commentService.Create(input.CarCommentsInputModel);
            return this.RedirectToAction("Details", new { id });
        }

        public async Task<IActionResult> Edit(string id)
        {

            var car = await this.adService.GetCurrentCarAsync(id);
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
    }
}
