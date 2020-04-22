namespace DimiAuto.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using DimiAuto.Data.Common.Models;
    using DimiAuto.Data.Common.Repositories;
    using DimiAuto.Data.Models;
    using DimiAuto.Services.Data.AreaServices;
    using DimiAuto.Web.ViewModels.Administration.AdministrationControl;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [Authorize(Roles = "Administrator")]
    [ApiController]
    [Route("api/administration/[controller]/[action]")]
    public class AdministrationControlController : ControllerBase
    {
        private readonly IAdministrationService administrationService;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;

        public AdministrationControlController(IAdministrationService administrationService, IDeletableEntityRepository<ApplicationUser> userRepository)
        {
            this.administrationService = administrationService;
            this.userRepository = userRepository;
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

        // [HttpPost]
        // public async Task PermamentDelete(AdministrationControlInputModel input)
        // {
        //    await this.administrationService.PermamentDeleteAsync(input.CarId);
        // }
        [HttpPost]
        public async Task<ActionResult<string>> DeleteUser(AdministrationControlInputModel input)
        {
            var user = await this.userRepository.AllWithDeleted().FirstOrDefaultAsync(x => x.Id == input.Id);
            user.IsDeleted = true;
            this.userRepository.Update(user);
            await this.userRepository.SaveChangesAsync();
            return this.Ok(new { output = "Deleted", action = "DeleteUser" });

        }

        [HttpPost]
        public async Task<ActionResult<string>> UndeleteUser(AdministrationControlInputModel input)
        {
            var user = await this.userRepository.AllWithDeleted().FirstOrDefaultAsync(x => x.Id == input.Id);
            user.IsDeleted = false;
            this.userRepository.Update(user);
            await this.userRepository.SaveChangesAsync();
            return this.Ok(new { output = "Undeleted", action = "UndeleteUser" });

        }
    }
}
