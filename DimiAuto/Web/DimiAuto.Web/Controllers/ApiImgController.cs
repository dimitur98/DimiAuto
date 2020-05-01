namespace DimiAuto.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using DimiAuto.Common;
    using DimiAuto.Data.Common.Repositories;
    using DimiAuto.Data.Models;
    using DimiAuto.Models.CarModel;
    using DimiAuto.Services.Data;
    using DimiAuto.Web.ViewModels.Img;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ApiImgController : Controller
    {
        private readonly Cloudinary cloudinary;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IImgService imgService;
        private readonly IAdService adService;

        public ApiImgController(Cloudinary cloudinary, UserManager<ApplicationUser> userManager, IImgService imgService, IAdService adService)
        {
            this.cloudinary = cloudinary;
            this.userManager = userManager;
            this.imgService = imgService;
            this.adService = adService;
        }

        [HttpPost]
        public bool ImgValidation(ImgValidationInputModel input)
        {
            return this.imgService.IsValidImg("." + input.Format.ToLower(), input.Type, input.Size);
        }

        [HttpPost]
        public async Task<bool> DeleteAvatarImg(ImgDeleteInputModel input)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var imgParts = input.ImgToDel.Split("/", StringSplitOptions.RemoveEmptyEntries).ToList();
            var img = imgParts[imgParts.Count - 2] + "/" + imgParts[imgParts.Count - 1];
            user.UserImg = user.UserImg.Replace(img, GlobalConstants.DefaultImgAvatar);

            await this.userManager.UpdateAsync(user);

            return await this.DeleteImgFromCloud(input);
        }

        [HttpPost]
        public async Task<bool> DeleteCarImg(ImgDeleteInputModel input)
        {
            var car = await this.adService.GetCurrentCarAsync(input.CarId);
            var imgParts = input.ImgToDel.Split("/", StringSplitOptions.RemoveEmptyEntries).ToList();
            var img = imgParts[imgParts.Count - 2] + "/" + imgParts[imgParts.Count - 1];
            if (car.ImgsPaths.Contains(img))
            {
                //var newImgsPaths = car.ImgsPaths.Replace(img, string.Empty);
                var imgList = car.ImgsPaths.Split(",", StringSplitOptions.RemoveEmptyEntries).ToList();
                imgList.Remove(img);
                car.ImgsPaths = string.Join(",", imgList);
            }

            if (car.ImgsPaths.Length == 0)
            {
                car.ImgsPaths = GlobalConstants.DefaultImgCar;
            }

            await this.adService.UpdateCarRecordAsync(car);

            return await this.DeleteImgFromCloud(input);
        }

        private async Task<bool> DeleteImgFromCloud(ImgDeleteInputModel input)
        {
            if (input.ImgToDel != GlobalConstants.CloudinaryPathDimitur98 + GlobalConstants.DefaultImgCar ||
                input.ImgToDel != GlobalConstants.CloudinaryPathDimitur98 + GlobalConstants.DefaultImgAvatar)
            {
                var img = Regex.Match(input.ImgToDel, @"[a-zA-Z0-9.]+$").ToString();
                img = img.Substring(0, img.Length - 4);
                DeletionParams deletionParams = new DeletionParams(img)
                {
                    PublicId = img.ToString(),
                };
                await this.cloudinary.DestroyAsync(deletionParams);
                return true;
            }

            return false;
        }
    }
}
