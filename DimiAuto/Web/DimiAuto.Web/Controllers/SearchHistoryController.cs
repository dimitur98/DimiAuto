namespace DimiAuto.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using DimiAuto.Data.Models;
    using DimiAuto.Services.Data;
    using DimiAuto.Web.ViewModels.SearchHistory;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

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
                Searches = await this.searchService.GetSearchModelsAsync(user.Id),
            };
            return this.View(output);
        }

        public async Task<IActionResult> DeleteSearchHistory(string id)
        {
            if (id == null)
            {
                return this.View("Error");
            }

            await this.searchService.DeleteSearchModelByIdAsync(id);
            return this.Redirect("SearchHistoryIndex");
        }
    }
}
