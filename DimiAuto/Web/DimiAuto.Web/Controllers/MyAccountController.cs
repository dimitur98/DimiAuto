using DimiAuto.Data.Models;
using DimiAuto.Services.Data;
using DimiAuto.Web.ViewModels.MyAccount;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DimiAuto.Web.Controllers
{
    public class MyAccountController : Controller
    {
        private readonly IMyAccountService myAccountService;
        private readonly UserManager<ApplicationUser> userManager;

        public MyAccountController(IMyAccountService myAccountService, UserManager<ApplicationUser> userManager)
        {
            this.myAccountService = myAccountService;
            this.userManager = userManager;
        }
        public async Task<IActionResult> MyAccount()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await this.userManager.GetUserAsync(this.User);
            var myCars = await this.myAccountService.GetMyCarsAsync<MyCarsViewModel>(userId);
            var result = new MyAccountViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                MyCars = myCars,
            };
            return this.View(result);
        }
        public IActionResult AccountInfo()
        {
            return this.View();
        }
    }
}
