namespace DimiAuto.Web.Areas.Administration.Controllers
{
    using DimiAuto.Data.Common.Repositories;
    using DimiAuto.Models.CarModel;
    using DimiAuto.Services.Data.AreaServices;
    using DimiAuto.Web.ViewModels.Administration;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;

    [Authorize(Roles = "Administrator")]
    [Area("Administration")]
    public class AdministrationController : Controller
    {
        private readonly IAdministrationService administrationService;
        

        public AdministrationController(IAdministrationService administrationService)
        {
            this.administrationService = administrationService;
        }
        public async Task<IActionResult> AllAds()
        {
            var result = new AllAdsViewModel
            {
                Cars = await this.administrationService.GetAllAdsAsync<AdViewModel>(),
            };
            return this.View(result);
        }


        
    }
}
