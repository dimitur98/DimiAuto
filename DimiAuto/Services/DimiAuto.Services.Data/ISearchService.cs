namespace DimiAuto.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using DimiAuto.Data.Models;
    using DimiAuto.Web.ViewModels.Home;
    using DimiAuto.Web.ViewModels.SearchHistory;

    public interface ISearchService
    {
        Task SaveSearchModelAsync(string userId, SearchInputModel search);

        Task<ICollection<SearchViewModel>> GetSearchModelsAsync(string userId);

        Task DeleteSearchModelByIdAsync(string id);

        Task<SearchModel> GetSearchModelByIdAsync(string id);

        Task<SearchModel> GetDefaultSearchModel();
    }
}
