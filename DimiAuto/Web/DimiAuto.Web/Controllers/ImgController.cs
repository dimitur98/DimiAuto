namespace DimiAuto.Web.Controllers
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

        public IActionResult Upload()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Upload(string id, ImgUploadInputModel input)
        {
            var result = await this.imgService.UploadImgsAsync(input);
            this.ViewBag.ImgsPaths = result.ToString();
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            await this.imgService.AddImgToCurrentAdAsync(string.Join(",", result), id);
            return this.Redirect("/");
        }

        public async Task<IActionResult> EditAdImgs(string id)
        {
            var car = await this.adService.GetCurrentCarAsync(id.Substring(3));
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
                    CarId = car.Id,
                    File1 = imgsPaths[0],
                    File2 = imgsPaths[1],
                    File3 = imgsPaths[2],
                    File4 = imgsPaths[3],
                    File5 = imgsPaths[4],
                    File6 = imgsPaths[5],
                    File7 = imgsPaths[6],
                    File8 = imgsPaths[7],
                    File9 = imgsPaths[8],
                    File10 = imgsPaths[9],
                },
            };
            return this.View(output);
        }

        [HttpPost]
        public async Task<IActionResult> EditAdImgs(string id, ImgUploadInputModel input)
        {
            var result = await this.imgService.UploadImgsAsync(input);
            await this.imgService.AddImgToCurrentAdAsync(string.Join(",", result), id);
            return this.Redirect("/MyAccount/MyAccount");
        }
    }
}
