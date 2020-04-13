namespace DimiAuto.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using DimiAuto.Common;
    using DimiAuto.Data.Common.Models;
    using DimiAuto.Data.Common.Repositories;
    using DimiAuto.Data.Models;
    using DimiAuto.Services.Mapping;
    using DimiAuto.Web.ViewModels.Home;
    using DimiAuto.Web.ViewModels.SearchHistory;
    using Microsoft.EntityFrameworkCore;

    public class SearchService : ISearchService
    {
        private readonly IDeletableEntityRepository<SearchModel> searchModelRepository;
        private readonly IAdService adService;

        public SearchService(IDeletableEntityRepository<SearchModel> searchModelRepository, IAdService adService)
        {
            this.searchModelRepository = searchModelRepository;
            this.adService = adService;
        }

        public async Task SaveSearchModelAsync(string userId, SearchInputModel search)
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
            var searchModelExist = await this.searchModelRepository.All().Where(x => x.UserId == userId).FirstOrDefaultAsync(x =>
            x.Condition == searchModel.Condition && x.Fuel == searchModel.Fuel && x.GearBox == searchModel.GearBox &&
            x.Make == searchModel.Make && x.Model == searchModel.Model && x.PriceFrom == searchModel.PriceFrom && x.PriceTo == searchModel.PriceTo &&
            x.YearFrom == searchModel.YearFrom && x.YearTo == searchModel.YearTo && x.TypeOfVeichle == searchModel.TypeOfVeichle);
            if (searchModelExist == null)
            {
                await this.searchModelRepository.AddAsync(searchModel);
                await this.searchModelRepository.SaveChangesAsync();
            }

        }

        public async Task<ICollection<SearchViewModel>> GetSearchModelsAsync(string userId)
        {
            var result = await this.searchModelRepository.All().Where(x => x.UserId == userId).OrderBy(x => x.CreatedOn).Select(x => new SearchViewModel 
            {
                Id = x.Id,
                Model = x.Model,
                Make = x.Make,
                Condition = x.Condition,
                Fuel = x.Fuel,
                GearBox = x.GearBox,
                ModelToString = this.adService.EnumParser(x.Make.ToString(), x.Model),
                PriceFrom = x.PriceFrom,
                PriceTo = x.PriceTo,
                TypeOfVeichle = x.TypeOfVeichle,
                YearFrom = x.YearFrom,
                YearTo = x.YearTo,
            }).ToListAsync();
            return result;
        }

        public async Task DeleteSearchModelByIdAsync(string id)
        {
            var searchModel = await this.searchModelRepository.All().FirstOrDefaultAsync(x => x.Id == id);
            if (searchModel == null)
            {
                throw new NullReferenceException();
            }
            searchModel.IsDeleted = true;
            this.searchModelRepository.Update(searchModel);
            await this.searchModelRepository.SaveChangesAsync();

        }

        public async Task<SearchModel> GetSearchModelByIdAsync(string id)
        {
            var result = await this.searchModelRepository.All().FirstOrDefaultAsync(x => x.Id == id);
            if (result == null)
            {
                throw new NullReferenceException();
            }

            return result;
        }

        public async Task<SearchModel> GetDefaultSearchModel()
        {
            return await this.searchModelRepository.All().FirstOrDefaultAsync(x => x.UserId == GlobalConstants.DefaultSearchModelUserId);
        }
    }
}
