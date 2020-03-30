
namespace DimiAuto.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using DimiAuto.Data.Common.Repositories;
    using DimiAuto.Data.Models;
    using DimiAuto.Services.Data;
    using DimiAuto.Web.ViewModels.Ad.ApiController;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ApiAdController : Controller
    {
        private readonly IAdService adService;
        private readonly IDeletableEntityRepository<UserCarFavorite> favouriteRepository;

        public ApiAdController(IAdService adService, IDeletableEntityRepository<UserCarFavorite> favouriteRepository)
        {
            this.adService = adService;
            this.favouriteRepository = favouriteRepository;
        }

        [HttpPost]
        public async Task<ActionResult<string>> AddToFav(ApiInputModel input)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var record = await this.favouriteRepository.All().FirstOrDefaultAsync(x => x.CarId == input.CarId && x.UserId == userId);
            if (record == null)
            {
                await this.adService.AddAdToFavAsync(input.CarId, userId);
                return this.Ok(new { output = "added" });
            }
            return this.Ok(new { output = "already added" });
        }

        [HttpPost]
        public async Task<ActionResult<string>> RemoveFavAd(ApiInputModel input)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var record = await this.favouriteRepository.All().FirstOrDefaultAsync(x => x.CarId == input.CarId && x.UserId == userId);
            if (record != null)
            {
                await this.adService.RemoveFavAdAsync(input.CarId, userId);
                return this.Ok(new { output = "removed" , carId = input.CarId});
            }
            return this.Ok();
        }

    }
}
