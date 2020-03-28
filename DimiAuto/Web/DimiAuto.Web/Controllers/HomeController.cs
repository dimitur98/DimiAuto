namespace DimiAuto.Web.Controllers
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using DimiAuto.Data.Models;
    using DimiAuto.Services.Data;
    using DimiAuto.Web.ViewModels;
    using DimiAuto.Web.ViewModels.Ad;
    using DimiAuto.Web.ViewModels.Home;
    using HtmlAgilityPack;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly IHomeService homeService;
        private readonly ISearchService searchService;
        private readonly UserManager<ApplicationUser> userManager;

        public HomeController(IHomeService homeService, ISearchService searchService, UserManager<ApplicationUser> userManager)
        {
            this.homeService = homeService;
            this.searchService = searchService;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> All()
        {
            var result = new AllCarsViewModel
            {
                AllCars = await this.homeService.GetAllAdsAsync(),
            };
            

            return this.View(result);
        }

        public async Task<IActionResult> AllByCriteria(string id, SearchInputModel input)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            if (id != null)
            {
                var searchModel = await this.searchService.GetSearchModelByIdAsync(id.Substring(3));
                input = new SearchInputModel
                {
                    Condition = searchModel.Condition,
                    Fuel = searchModel.Fuel,
                    GearBox = searchModel.GearBox,
                    Make = searchModel.Make,
                    Model = searchModel.Model,
                    PriceFrom = searchModel.PriceFrom,
                    PriceTo = searchModel.PriceTo,
                    TypeOfVeichle = searchModel.TypeOfVeichle,
                    YearFrom = searchModel.YearFrom,
                    YearTo = searchModel.YearTo,
                };
            }
            else
            {
                if (user != null)
                {
                    await this.searchService.SaveSearchModel(user.Id, input);
                }
            }
            var ads = await this.homeService.GetAdsByCriteriaAsync(input);
            
            var result = new AllCarsViewModel
            {
                AllCars = ads,
            };

            return this.View("All", result);
        }
    }
}
