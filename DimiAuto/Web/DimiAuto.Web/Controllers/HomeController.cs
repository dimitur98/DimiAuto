namespace DimiAuto.Web.Controllers
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;

    using DimiAuto.Services.Data;
    using DimiAuto.Web.ViewModels;
    using DimiAuto.Web.ViewModels.Ad;
    using DimiAuto.Web.ViewModels.Home;
    using HtmlAgilityPack;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly IHomeService homeService;

        public HomeController(IHomeService homeService)
        {
            this.homeService = homeService;
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

        public IActionResult All()
        {
            var result = new AllCarsViewModel
            {
                AllCars = this.homeService.GetAllAds(),
            };
            return this.View(result);
        }

        public IActionResult AllByCriteria(SearchInputModel input, string orderByPrice, string orderByYear)
        {
            var ads = this.homeService.GetAdsByCriteria(input);

            var result = new AllCarsViewModel
            {
                AllCars = ads,
            };

            return this.View("All", result);
        }
        
        
    }
}
