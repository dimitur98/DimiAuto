namespace DimiAuto.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using DimiAuto.Data.Models;
    using DimiAuto.Web.ViewModels.Home;

    public interface ISearchService
    {
        Task SaveSearchModelAsync(string userId, SearchInputModel search);

        Task<ICollection<TModel>> GetSearchModelsAsync<TModel>(string userId);

        Task DeleteSearchModelByIdAsync(string id);

        Task<SearchModel> GetSearchModelByIdAsync(string id);
    }
}
