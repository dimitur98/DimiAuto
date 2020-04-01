using DimiAuto.Data.Models;
using DimiAuto.Services.Data;
using DimiAuto.Web.ViewModels.SearchHistory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DimiAuto.Web.Controllers
{
    [Authorize]
    public class SearchHistoryController : Controller
    {
        private readonly ISearchService searchService;
        private readonly UserManager<ApplicationUser> userManager;

        public SearchHistoryController(ISearchService searchService, UserManager<ApplicationUser> userManager)
        {
            this.searchService = searchService;
            this.userManager = userManager;
        }
        public async Task<IActionResult> SearchHistoryIndex()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var output = new SearchModelsViewModel
            {
                Searches = await this.searchService.GetSearchModelsAsync<SearchViewModel>(user.Id),
            };
            return this.View(output);
        }

        public async Task<IActionResult> DeleteSearchHistory(string id)
        {
            await this.searchService.DeleteSearchModelByIdAsync(id.Substring(3));
            return this.Redirect("SearchHistoryIndex");
        }
    }
}
