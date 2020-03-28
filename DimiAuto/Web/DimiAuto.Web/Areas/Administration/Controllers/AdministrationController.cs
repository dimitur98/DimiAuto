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
    using DimiAuto.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using DimiAuto.Services.Mapping;
    using DimiAuto.Common;
    using DimiAuto.Data;

    [Authorize(Roles = "Administrator")]
    [Area("Administration")]
    public class AdministrationController : Controller
    {
        private readonly IAdministrationService administrationService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ApplicationDbContext dbContext;

        public AdministrationController(IAdministrationService administrationService, UserManager<ApplicationUser> userManager, ApplicationDbContext dbContext)
        {
            this.administrationService = administrationService;
            this.userManager = userManager;
            this.dbContext = dbContext;
        }
        public async Task<IActionResult> AllAds()
        {
            var result = new AllAdsViewModel
            {
                Cars = await this.administrationService.GetAllAdsAsync<AdViewModel>(),
            };
            return this.View(result);
        }

        public async Task<IActionResult> AllUsers()
        {
            var users = await this.userManager.Users.Where(x => x.Roles.Count == 0).To<UserViewModel>().ToListAsync();

            var result = new AllUsersViewModel
            {
                Users = users,
            };
            return this.View(result);
        }

        public async Task<IActionResult> UserDetails(string id)
        {
            var user = await this.userManager.FindByIdAsync(id.Substring(3));
            var output = new UserDetailsViewModel
            {
                Id = user.Id,
                Adress = user.Adress,
                City = user.City,
                FirstName = user.FirstName,
                LastName = user.LastName,
                NameOfCompany = user.LastName,
                PhoneNumber = user.PhoneNumber,
            };
            return this.View(output);
        }

    }
}
