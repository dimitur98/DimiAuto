namespace DimiAuto.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using DimiAuto.Web.ViewModels.Img;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using DimiAuto.Services.Data;
    using DimiAuto.Models.CarModel;
    using DimiAuto.Data.Common.Repositories;
    using Microsoft.EntityFrameworkCore;
    using DimiAuto.Common;
    using Microsoft.AspNetCore.Identity;
    using DimiAuto.Data.Models;
    using System.Security.Claims;
    using Microsoft.AspNetCore.Authorization;

    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ImgDeleteController : Controller
    {
        private readonly Cloudinary cloudinary;
        private readonly IDeletableEntityRepository<Car> carRepository;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IAdService adService;

        public ImgDeleteController(Cloudinary cloudinary, IDeletableEntityRepository<Car> carRepository, UserManager<ApplicationUser> userManager)
        {
            this.cloudinary = cloudinary;
            this.carRepository = carRepository;
            this.userManager = userManager;
        }

        [HttpPost]
        public async Task<ActionResult<string>> ImgDelete(ImgDeleteInputModel input)
        {
            if (input.CarId == null)
            {
                var user = await this.userManager.GetUserAsync(this.User);
                user.UserImg = user.UserImg.Replace(input.ImgToDel, string.Empty);
                if (user.UserImg == string.Empty)
                {
                    user.UserImg = GlobalConstants.DefaultImgAvatar;
                    await this.userManager.UpdateAsync(user);
                }
            }
            else
            {
                var car = await this.carRepository.All().FirstOrDefaultAsync(x => x.Id == input.CarId);
                car.ImgsPaths = car.ImgsPaths.Replace(input.ImgToDel, string.Empty);
                if (car.ImgsPaths == string.Empty)
                {
                    car.ImgsPaths = GlobalConstants.DefaultImgCar;
                }
                this.carRepository.Update(car);
                await this.carRepository.SaveChangesAsync();
            }
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
