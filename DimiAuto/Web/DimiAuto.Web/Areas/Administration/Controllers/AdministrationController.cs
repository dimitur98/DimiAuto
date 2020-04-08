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
    using DimiAuto.Data.Repositories;

    [Authorize(Roles = "Administrator")]
    [Area("Administration")]
    public class AdministrationController : Controller
    {
        private readonly IAdministrationService administrationService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ApplicationDbContext dbContext;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        private readonly RoleManager<ApplicationRole> roleManager;

        public AdministrationController(IAdministrationService administrationService, UserManager<ApplicationUser> userManager, ApplicationDbContext dbContext, IDeletableEntityRepository<ApplicationUser> userRepository, RoleManager<ApplicationRole> roleManager)
        {
            this.administrationService = administrationService;
            this.userManager = userManager;
            this.dbContext = dbContext;
            this.userRepository = userRepository;
            this.roleManager = roleManager;
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
            var users = await this.userRepository.AllWithDeleted().OrderByDescending(x => x.CreatedOn).To<UserViewModel>().ToListAsync();
            var result = new AllUsersViewModel
            {
                Users = users,
            };
            return this.View(result);
        }

        public async Task<IActionResult> UserDetails(string id)
        {
            var user = await this.userRepository.AllWithDeleted().FirstOrDefaultAsync(x => x.Id == id.Substring(3));
            var output = new UserDetailsViewModel
            {
                Id = user.Id,
                Adress = user.Adress,
                City = user.City,
                FirstName = user.FirstName,
                LastName = user.LastName,
                NameOfCompany = user.NameOfCompany,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                ImgPath = user.UserImg,
                Bulstad = user.Bulstad,
                PhoneForCustomers = user.TelephoneForCustomers,
            };
            return this.View(output);
        }

    }
}
