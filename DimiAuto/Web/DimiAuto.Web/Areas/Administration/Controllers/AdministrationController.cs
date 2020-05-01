namespace DimiAuto.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using DimiAuto.Common;
    using DimiAuto.Data;
    using DimiAuto.Data.Common.Repositories;
    using DimiAuto.Data.Models;
    using DimiAuto.Data.Repositories;
    using DimiAuto.Models.CarModel;
    using DimiAuto.Services.Data;
    using DimiAuto.Services.Data.AreaServices;
    using DimiAuto.Services.Mapping;
    using DimiAuto.Web.ViewModels.Administration;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [Authorize(Roles = "Administrator")]
    [Area("Administration")]
    public class AdministrationController : Controller
    {
        private readonly IAdministrationService administrationService;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;

        public AdministrationController(
            IAdministrationService administrationService,
            IDeletableEntityRepository<ApplicationUser> userRepository)
        {
            this.administrationService = administrationService;
            this.userRepository = userRepository ?? throw new System.ArgumentNullException(nameof(userRepository));
        }

        public async Task<IActionResult> AllAds()
        {
            var result = new AllAdsViewModel
            {
                Cars = await this.administrationService.GetAllAdsAsync(),
            };
            return this.View(result);
        }

        public async Task<IActionResult> AllUsers()
        {
            var users = await this.userRepository.AllWithDeleted().OrderByDescending(x => x.CreatedOn).To<UserViewModel>().ToListAsync();
            var result = new AllUsersViewModel
            {
                Users = users.SkipLast(1),
            };
            return this.View(result);
        }

        public async Task<IActionResult> UserDetails(string id)
        {
            var user = await this.userRepository.AllWithDeleted().FirstOrDefaultAsync(x => x.Id == id);
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
                ImgPath = GlobalConstants.CloudinaryPathDimitur98 + user.UserImg,
                Bulstad = user.Bulstad,
                PhoneForCustomers = user.TelephoneForCustomers,
            };
            return this.View(output);
        }
    }
}
