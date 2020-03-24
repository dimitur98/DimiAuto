using DimiAuto.Common;
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
        private readonly IAdService adService;

        public MyAccountController(IMyAccountService myAccountService, UserManager<ApplicationUser> userManager, IAdService adService)
        {
            this.myAccountService = myAccountService;
            this.userManager = userManager;
            this.adService = adService;
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

        public async Task<IActionResult> AccountInfo()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var result = new ChangePersonalInfoInputModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Adress = user.Adress,
                City = user.City,
                NameOfCompany = user.NameOfCompany,

                PhoneNumber = user.PhoneNumber,
            };
            return this.View(result);
        }

        [HttpPost]
        public async Task<IActionResult> AccountInfo(ChangePersonalInfoInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }
            var user = await this.userManager.GetUserAsync(this.User);
            user.FirstName = input.FirstName;
            user.LastName = input.LastName;
            user.Adress = input.Adress;
            user.City = input.City;
            user.PhoneNumber = input.PhoneNumber;
            if (input.NameOfCompany != GlobalConstants.PrivatePerson)
            {
                user.NameOfCompany = input.NameOfCompany;

            }
            await this.userManager.UpdateAsync(user);
            return this.RedirectToAction("MyAccount");
        }

        
    }
}
