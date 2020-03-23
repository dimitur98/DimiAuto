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
            var output = await this.adService.GetCurrentCarAsync(id);

            return this.View(output);
        }

        [HttpPost]
        public async Task<IActionResult> Details(string id,CarDetailsModel input)
        {
            input.CarCommentsInputModel.UserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            input.CarCommentsInputModel.CarId = id.Substring(3);
            await this.commentService.Create(input.CarCommentsInputModel);
            return this.RedirectToAction("Details", new { id });
        }
    }
}
