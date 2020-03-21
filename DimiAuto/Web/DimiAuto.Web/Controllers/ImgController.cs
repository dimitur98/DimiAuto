namespace DimiAuto.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using DimiAuto.Services.Data;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    public class ImgController : Controller
    {
        private readonly IImgService imgService;

        public ImgController(IImgService imgService)
        {
            this.imgService = imgService;
        }
        
        public IActionResult Upload()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Upload(string id, ICollection<IFormFile> files)
        {
            var result = await this.imgService.UploadImgsAsync(files);
            this.ViewBag.ImgsPaths = result.ToString();
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            await this.imgService.AddImgToCurrentAdAsync(string.Join(",", result), id);
            return this.Redirect("/");
        }
    }
}
