using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DimiAuto.Data.Models;
using DimiAuto.Services.Data.AreaServices;
using DimiAuto.Web.ViewModels.Administration.AdministrationControl;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DimiAuto.Web.Areas.Administration.Controllers
{
    [ApiController]
    [Route("api/administration/[controller]/[action]")]
    public class AdministrationControlController : ControllerBase
    {
        private readonly IAdministrationService administrationService;
        private readonly UserManager<ApplicationUser> userManager;

        public AdministrationControlController(IAdministrationService administrationService, UserManager<ApplicationUser> userManager)
        {
            this.administrationService = administrationService;
            this.userManager = userManager;
        }

        [HttpPost]
        public async Task<ActionResult<string>> Approve(AdministrationControlInputModel input)
        {
            await this.administrationService.ApproveAsync(input.Id);
            return this.Ok(new { output = "Approve", action = "Approve" });

        }

        [HttpPost]
        public async Task<ActionResult<string>> DeleteAd(AdministrationControlInputModel input)
        {
            await this.administrationService.DeleteAsync(input.Id);
            return this.Ok(new { output = "Deleted", action = "Delete" });
        }

        [HttpPost]
        public async Task<ActionResult<string>> Undelete(AdministrationControlInputModel input)
        {
            await this.administrationService.UnDeleteAsync(input.Id);
            return this.Ok(new { output = "Not deleted", action = "Undelete" });

        }

        //[HttpPost]
        //public async Task PermamentDelete(AdministrationControlInputModel input)
        //{
        //    await this.administrationService.PermamentDeleteAsync(input.CarId);


        //}

        [HttpPost]
        public async Task<ActionResult<string>> DeleteUser(AdministrationControlInputModel input)
        {
            var user = await this.userManager.FindByIdAsync(input.Id);
            user.IsDeleted = true;
            await this.userManager.UpdateAsync(user);
            return this.Ok(new { output = "Deleted", action = "DeleteUser" });

        }

        [HttpPost]
        public async Task<ActionResult<string>> UndeleteUser(AdministrationControlInputModel input)
        {
            var user = await this.userManager.FindByIdAsync(input.Id);
            user.IsDeleted = false;
            await this.userManager.UpdateAsync(user);
            return this.Ok(new { output = "Undeleted", action = "UndeleteUser" });

        }
    }
}
