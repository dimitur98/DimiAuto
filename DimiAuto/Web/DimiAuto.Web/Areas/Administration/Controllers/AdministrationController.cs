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
        public async Task<IActionResult> Approve()
        {
            var result = new AdsForApprovingViewModel
            {
                Cars = await this.administrationService.GetAllUnApprovedAdsAsync<ApproveViewModel>(),
            };
            return this.View(result);
        }
        [HttpPost]
        public async Task<IActionResult> Approve(string carId)
        {
            await this.administrationService.ApproveAsync(carId);
            return this.Redirect("/Administration/Administration/Approve");
        }

        public async Task<IActionResult> Delete()
        {
            var result = new AllAdsViewModel
            {
                Cars = await this.administrationService.GetAllAdsAsync<DeleteOrUnDeleteViewModel>(),
            };
            return this.View(result);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(string carId)
        {
            await this.administrationService.DeleteAsync(carId);
            return this.Redirect("/Administration/Administration/Delete");
        }

        public async Task<IActionResult> Deleted()
        {
            var result = new AllAdsViewModel
            {
                Cars = await this.administrationService.GetAllDeletedAdsAsync<DeleteOrUnDeleteViewModel>(),
            };
            return this.View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Deleted(string carId)
        {
            await this.administrationService.UnDeleteAsync(carId);
            return this.Redirect("/Administration/Administration/Deleted");
        }
        public async Task<IActionResult> PermamentDelete()
        {
            var result = new AllAdsViewModel
            {
                Cars = await this.administrationService.GetAllDeletedAdsAsync<DeleteOrUnDeleteViewModel>(),
            };
            return this.View(result);
        }

        [HttpPost]
        public async Task<IActionResult> PermamentDelete(string carId)
        {
            await this.administrationService.PermamentDeleteAsync(carId);
            return this.Redirect("/Administration/Administration/PermamentDelete");
        }
    }
}
