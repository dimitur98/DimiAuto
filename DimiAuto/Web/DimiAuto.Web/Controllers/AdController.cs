﻿namespace DimiAuto.Web.Controllers
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
    using FinalProject.Models.CarModel;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    public class AdController : Controller
    {
        private readonly IAdService adService;

        public AdController(IAdService adService)
        {
            this.adService = adService;
        }

        public IActionResult Upload()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Upload(string id, ICollection<IFormFile> files)
        {
            var result = await this.adService.UploadImgsAsync(files);
            this.ViewBag.ImgsPaths = result.ToString();
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            await this.adService.AddImgToCurrentAdAsync(string.Join(",", result), id);
            return this.Redirect("/");
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

            return this.Redirect($"/Ad/Upload/id={id}");
        }

        public async Task<IActionResult> Details(string id)
        {
           // var a = Assembly("All.cshtml");
            var output = await this.adService.GetCurrentCar(id);
            
            return this.View(output);
        }
    }
}
