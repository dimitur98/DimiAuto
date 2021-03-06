﻿namespace DimiAuto.Web.Controllers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using System.Web;

    using AutoMapper;
    using DimiAuto.Common;
    using DimiAuto.Data;
    using DimiAuto.Data.Models;
    using DimiAuto.Data.Models.CarModel;
    using DimiAuto.Services.Data;
    using DimiAuto.Services.Mapping;
    using DimiAuto.Web.ViewModels;
    using DimiAuto.Web.ViewModels.Ad;
    using DimiAuto.Web.ViewModels.Home;
    using DimiAuto.Web.ViewModels.Sort;
    using HtmlAgilityPack;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    public class HomeController : BaseController
    {
        private readonly IHomeService homeService;
        private readonly ISearchService searchService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ApplicationDbContext db;

        public HomeController(
            IHomeService homeService,
            ISearchService searchService,
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext db)
        {
            this.homeService = homeService;
            this.searchService = searchService;
            this.userManager = userManager;
            this.db = db;
        }

        public IActionResult Index()
        {

            return this.View();
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        public IActionResult Error(int statusCode)
        {
            if (statusCode == 404)
            {
                return this.View("404Error");
            }

            return this.View("Error");
        }

        public async Task<IActionResult> All(int page = 1)
        {
            var searchModel = new SearchInputModel
            {
                Condition = Condition.All,
                Location = Location.All,
                Fuel = Fuel.All,
                TypeOfVeichle = TypeOfVeichle.All,
                Gearbox = Gearbox.All,
                Make = Make.All,
                Model = null,
                YearTo = null,
                YearFrom = null,
                PriceFrom = null,
                PriceTo = null,
            };
            var result = new AllCarsModel
            {
                AllCars = await this.homeService.GetAllAdsAsync(),
                SortInputModel = new SortInputModel { SearchInputModel = searchModel, },
                CurrentPage = page,
                Action = "All",
            };
            var count = result.AllCars.Count;
            result.PagesCount = (int)Math.Ceiling((double)count / GlobalConstants.ItemsPerPage);
            if (result.PagesCount == 0)
            {
                result.PagesCount = 1;
            }

            var output = this.homeService.Paging(result, GlobalConstants.ItemsPerPage, (page - 1) * GlobalConstants.ItemsPerPage);
            this.ViewData["searchModel"] = searchModel as SearchInputModel;

            return this.View(output);
        }

        public async Task<IActionResult> AllByCriteria(string id, SearchInputModel input, int page = 1)
        {
            // if (input.Make == 0)
            // {
            //    var searchModel = await this.searchService.GetDefaultSearchModel();
            //    input = AutoMapperConfig.MapperInstance.Map<SearchInputModel>(searchModel);
            // }
            var user = await this.userManager.GetUserAsync(this.User);
            if (id != null)
            {
                var searchModel = await this.searchService.GetSearchModelByIdAsync(id);
                input = AutoMapperConfig.MapperInstance.Map<SearchInputModel>(searchModel);
            }
            else
            {
                if (user != null)
                {
                    await this.searchService.SaveSearchModelAsync(user.Id, input);
                }
            }

            this.ViewData["searchModel"] = input as SearchInputModel;

            var ads = await this.homeService.GetAdsByCriteriaAsync(input);

            var result = new AllCarsModel
            {
                AllCars = ads,
                SortInputModel = new SortInputModel { SearchInputModel = new SearchInputModel(), },
                CurrentPage = page,
                Action = "AllByCriteria",
            };

            var count = result.AllCars.Count;
            result.PagesCount = (int)Math.Ceiling((double)count / GlobalConstants.ItemsPerPage);
            if (result.PagesCount == 0)
            {
                result.PagesCount = 1;
            }

            var output = this.homeService.Paging(result, GlobalConstants.ItemsPerPage, (page - 1) * GlobalConstants.ItemsPerPage);

            return this.View("All", output);
        }

        public async Task<IActionResult> Sort(SortInputModel sortModel, AllCarsModel input, SearchInputModel searchModle, int page = 1)
        {
            ICollection<CarAdsViewModel> ads;

            // this is true only when you go through pages
            if (searchModle.Make != 0)
            {
                input.SortInputModel = new SortInputModel { OrderByYear = sortModel.OrderByYear, OrderByPrice = sortModel.OrderByPrice, SearchInputModel = searchModle };
            }

            var sortInputModel = new SortInputModel();
            if (input.SortInputModel.SearchInputModel == null)
            {
                ads = await this.homeService.GetAllAdsAsync();
            }
            else
            {
                ads = await this.homeService.GetAdsByCriteriaAsync(input.SortInputModel.SearchInputModel);
                this.ViewData["searchModel"] = input.SortInputModel.SearchInputModel as SearchInputModel;
                this.ViewData["orderByYear"] = input.SortInputModel.OrderByYear;
                this.ViewData["orderByPrice"] = input.SortInputModel.OrderByPrice;
                sortInputModel.SearchInputModel = new SearchInputModel();
            }

            if (input.SortInputModel.OrderByYear == "2")
            {
                ads = ads.OrderBy(x => x.YearOfProduction).ToList();
            }
            else if (input.SortInputModel.OrderByYear == "1")
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

            var result = new AllCarsModel
            {
                AllCars = ads,
                SortInputModel = sortInputModel,
                CurrentPage = page,
                Action = "Sort",
            };

            var count = result.AllCars.Count;
            result.PagesCount = (int)Math.Ceiling((double)count / GlobalConstants.ItemsPerPage);
            if (result.PagesCount == 0)
            {
                result.PagesCount = 1;
            }

            var output = this.homeService.Paging(result, GlobalConstants.ItemsPerPage, (page - 1) * GlobalConstants.ItemsPerPage);
            return this.View("All", output);
        }

        public async Task<IActionResult> UserCars(string id)
        {
            var user = await this.userManager.FindByIdAsync(id);
            var output = new UserCarsViewModel
            {
                AllCars = await this.homeService.GetCarsOfUserAsync(id),
                User = user,
                TopFourCars = await this.homeService.GetTopFourMostWatchedCarsOfUserAsync(user.Id),
            };

            return this.View(output);
        }

        public async Task<IActionResult> Users()
        {
            var users = await this.db.Users.Where(x => x.IsDeleted == false && x.EmailConfirmed == true).ToListAsync();
            var output = new AllUsersModel
            {
                Users = users.Select(x => new UserViewModel
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Id = x.Id,
                    ImgPath =GlobalConstants.CloudinaryPathDimitur98 + x.UserImg,
                    NameOfCompany = x.NameOfCompany,
                    NameOfThePage = x.NameOfThePage,
                    CreatedOn = x.CreatedOn,
                }).OrderByDescending(x => x.CreatedOn).ToList(),
            };

            return this.View(output);
        }

        [HttpPost]
        public async Task<IActionResult> Users(AllUsersModel input)
        {
            var searchedText = input.SearchedText.SearchedText.ToLower().Replace(" ", string.Empty);
            var users = await this.db.Users.Where(x => x.IsDeleted == false && x.EmailConfirmed == true && x.NameOfThePage.ToLower().Replace(" ", string.Empty).Contains(searchedText)).ToListAsync();
            var output = new AllUsersModel
            {
                Users = users.Select(x => new UserViewModel
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Id = x.Id,
                    ImgPath = GlobalConstants.CloudinaryPathDimitur98 + x.UserImg,
                    NameOfCompany = x.NameOfCompany,
                    NameOfThePage = x.NameOfThePage,
                    CreatedOn = x.CreatedOn,
                }).OrderByDescending(x => x.CreatedOn).ToList(),
            };

            return this.View(output);
        }
    }
}
