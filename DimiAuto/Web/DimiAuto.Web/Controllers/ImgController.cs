﻿namespace DimiAuto.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using DimiAuto.Common;
    using DimiAuto.Data.Models;
    using DimiAuto.Services.Data;
    using DimiAuto.Web.ViewModels.Img;
    using DimiAuto.Web.ViewModels.MyAccount;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class ImgController : Controller
    {
        private readonly IImgService imgService;
        private readonly IAdService adService;
        private readonly Cloudinary cloudinary;
        private readonly UserManager<ApplicationUser> userManager;

        public ImgController(IImgService imgService, IAdService adService, Cloudinary cloudinary, UserManager<ApplicationUser> userManager)
        {
            this.imgService = imgService;
            this.adService = adService;
            this.cloudinary = cloudinary;
            this.userManager = userManager;
        }

        public IActionResult Upload(string id)
        {
            return this.View(new ImgUploadViewModel { CarId = id });
        }

        [HttpPost]
        public async Task<IActionResult> Upload(string id, ImgUploadInputModel input)
        {
            if (id == null)
            {
                return this.View("Error");
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var result = new List<string>();
            for (int i = 1; i <= 10; i++)
            {
                var file = (IFormFile)input.GetType().GetProperty("File" + i).GetValue(input, null);
                result.Add(await this.imgService.UploadImgAsync(file));
            }

            //this.ViewBag.ImgsPaths = result.ToString();
            await this.imgService.AddImgToCurrentAdAsync(string.Join(",", result), id);
            return this.Redirect("/");
        }

        public async Task<IActionResult> EditAdImgs(string id)
        {
            var car = await this.adService.GetCurrentCarAsync(id);
            if (car == null)
            {
                throw new NullReferenceException();
            }

            var uploadedImgs = car.ImgsPaths.Split(",", StringSplitOptions.RemoveEmptyEntries).ToArray();
            var imgsPaths = new string[10];
            for (int i = 0; i < uploadedImgs.Length; i++)
            {
                imgsPaths[i] = uploadedImgs[i];
            }

            var output = new ImgEditModel
            {
                ImgEditViewModel = new ImgEditViewModel
                {
                    CarId = id,
                    File1 = GlobalConstants.CloudinaryPathDimitur98 + imgsPaths[0],
                    File2 = GlobalConstants.CloudinaryPathDimitur98 + imgsPaths[1],
                    File3 = GlobalConstants.CloudinaryPathDimitur98 + imgsPaths[2],
                    File4 = GlobalConstants.CloudinaryPathDimitur98 + imgsPaths[3],
                    File5 = GlobalConstants.CloudinaryPathDimitur98 + imgsPaths[4],
                    File6 = GlobalConstants.CloudinaryPathDimitur98 + imgsPaths[5],
                    File7 = GlobalConstants.CloudinaryPathDimitur98 + imgsPaths[6],
                    File8 = GlobalConstants.CloudinaryPathDimitur98 + imgsPaths[7],
                    File9 = GlobalConstants.CloudinaryPathDimitur98 + imgsPaths[8],
                    File10 = GlobalConstants.CloudinaryPathDimitur98 + imgsPaths[9],
                },
            };
            return this.View(output);
        }

        [HttpPost]
        public async Task<IActionResult> EditAdImgs(string id, ImgUploadInputModel input)
        {
            if (id == null)
            {
                return this.View("Error");
            }

            var result = new List<string>();
            for (int i = 1; i <= 10; i++)
            {
                var file = (IFormFile)input.GetType().GetProperty("File" + i).GetValue(input, null);
                result.Add(await this.imgService.UploadImgAsync(file));
            }

            await this.imgService.AddImgToCurrentAdAsync(string.Join(",", result), id);
            return this.Redirect("/MyAccount/MyAccount");
        }
    }
}
