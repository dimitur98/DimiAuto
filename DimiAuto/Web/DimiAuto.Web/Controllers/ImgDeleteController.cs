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
    public class ImgDeleteController : Controller
    {
        private readonly Cloudinary cloudinary;
        private readonly IDeletableEntityRepository<Car> carRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public ImgDeleteController(Cloudinary cloudinary, IDeletableEntityRepository<Car> carRepository, UserManager<ApplicationUser> userManager)
        {
            this.cloudinary = cloudinary;
            this.carRepository = carRepository;
            this.userManager = userManager;
        }

        [HttpPost]
        public async Task<ActionResult<string>> DeleteAvatarImg(ImgDeleteInputModel input)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            user.UserImg = user.UserImg.Replace(input.ImgToDel, string.Empty);

            user.UserImg = GlobalConstants.DefaultImgAvatar;
            await this.userManager.UpdateAsync(user);

            return await this.DeleteImgFromCloud(input);
        }

        [HttpPost]
        public async Task<ActionResult<string>> DeleteCarImg(ImgDeleteInputModel input)
        {
            var car = await this.carRepository.AllWithDeleted().FirstOrDefaultAsync(x => x.Id == input.CarId);
            car.ImgsPaths = car.ImgsPaths.Replace(input.ImgToDel, string.Empty);
            if (car.ImgsPaths == string.Empty)
            {
                car.ImgsPaths = GlobalConstants.DefaultImgCar;
            }

            this.carRepository.Update(car);
            await this.carRepository.SaveChangesAsync();

            return await this.DeleteImgFromCloud(input);
        }

        private async Task<ActionResult<string>> DeleteImgFromCloud(ImgDeleteInputModel input)
        {
            var img = Regex.Match(input.ImgToDel, @"[a-zA-Z0-9.]+$").ToString();
            img = img.Substring(0, img.Length - 4);
            DeletionParams deletionParams = new DeletionParams(img)
            {
                PublicId = img.ToString(),
            };
            var deletionResult = await this.cloudinary.DestroyAsync(deletionParams);
            return deletionResult.ToString();
        }
    }
}
