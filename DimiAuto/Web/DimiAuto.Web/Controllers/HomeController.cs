namespace DimiAuto.Web.Controllers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;

    using AutoMapper;
    using DimiAuto.Data.Models;
    using DimiAuto.Services.Data;
    using DimiAuto.Services.Mapping;
    using DimiAuto.Web.ViewModels;
    using DimiAuto.Web.ViewModels.Ad;
    using DimiAuto.Web.ViewModels.Home;
    using DimiAuto.Web.ViewModels.Sort;
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
            var result = new AllCarsModel
            {
                AllCars = await this.homeService.GetAllAdsAsync(),
                SortInputModel = new SortInputModel(),
            };
            return this.View(result);
        }

        public async Task<IActionResult> AllByCriteria(string id, SearchInputModel input)
        {
            this.ViewData["searchModel"] = input as SearchInputModel;
            var user = await this.userManager.GetUserAsync(this.User);
            if (id != null)
            {
                var searchModel = await this.searchService.GetSearchModelByIdAsync(id.Substring(3));                
                input = AutoMapperConfig.MapperInstance.Map<SearchInputModel>(searchModel);
            }
            else
            {
                if (user != null)
                {
                    await this.searchService.SaveSearchModelAsync(user.Id, input);
                }
            }

            var ads = await this.homeService.GetAdsByCriteriaAsync(input);

            var result = new AllCarsModel
            {
                AllCars = ads,
                SortInputModel = new SortInputModel { SearchInputModel = new SearchInputModel(), },
            };

            return this.View("All", result);
        }

        public async Task<IActionResult> Sort(AllCarsModel input)
        {
            IEnumerable<CarAdsViewModel> ads;
            var sortInputModel = new SortInputModel();
            if (input.SortInputModel.SearchInputModel == null)
            {
                ads = await this.homeService.GetAllAdsAsync();
            }
            else
            {
                ads = await this.homeService.GetAdsByCriteriaAsync(input.SortInputModel.SearchInputModel);
                this.ViewData["searchModel"] = input.SortInputModel.SearchInputModel as SearchInputModel;
                sortInputModel.SearchInputModel = new SearchInputModel();
                
            }

            if (input.SortInputModel.OrderByYear == "1")
            {
                ads = ads.OrderBy(x => x.YearOfProduction).ToList();
            }
            else if (input.SortInputModel.OrderByPrice == "2")
            {
                ads = ads.OrderByDescending(x => x.YearOfProduction).ToList();
            }
            else if (input.SortInputModel.OrderByPrice == "2")
            {
                ads = ads.OrderBy(x => x.Price).ToList();
            }
            else if (input.SortInputModel.OrderByPrice == "1")
            {
                ads = ads.OrderByDescending(x => x.Price).ToList();
            }

            var output = new AllCarsModel
            {
                AllCars = ads,
                SortInputModel = sortInputModel,

            };
            return this.View("All", output);
        }
    }
}
