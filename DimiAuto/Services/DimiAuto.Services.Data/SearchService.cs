namespace DimiAuto.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using DimiAuto.Data.Common.Models;
    using DimiAuto.Data.Common.Repositories;
    using DimiAuto.Data.Models;
    using DimiAuto.Services.Mapping;
    using DimiAuto.Web.ViewModels.Home;
    using Microsoft.EntityFrameworkCore;

    public class SearchService : ISearchService
    {
        private readonly IDeletableEntityRepository<SearchModel> searchModelRepository;

        public SearchService(IDeletableEntityRepository<SearchModel> searchModelRepository)
        {
            this.searchModelRepository = searchModelRepository;
        }
        public async Task SaveSearchModel(string userId, SearchInputModel search)
        {
            var searchModel = new SearchModel
            {
                Condition = search.Condition,
                Fuel = search.Fuel,
                GearBox = search.GearBox,
                Make = search.Make,
                Model = search.Model,
                PriceFrom = search.PriceFrom,
                PriceTo = search.PriceTo,
                TypeOfVeichle = search.TypeOfVeichle,
                YearFrom = search.YearFrom,
                YearTo = search.YearTo,
                UserId = userId,
            };

            await this.searchModelRepository.AddAsync(searchModel);
            await this.searchModelRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<TModel>> GetSearchModels<TModel>(string userId)
        {
            var result = await this.searchModelRepository.All().Where(x => x.UserId == userId).OrderBy(x => x.CreatedOn).To<TModel>().ToListAsync();
            return result;
        }

        public async Task DeleteSearchModelByIdAsync(string id)
        {
            var searchModel = await this.searchModelRepository.All().FirstOrDefaultAsync(x => x.Id == id);
            searchModel.IsDeleted = true;
            this.searchModelRepository.Update(searchModel);
            await this.searchModelRepository.SaveChangesAsync();

        }

        public async Task<SearchModel> GetSearchModelByIdAsync(string id)
        {
            var result = await this.searchModelRepository.All().FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }
    }
}
