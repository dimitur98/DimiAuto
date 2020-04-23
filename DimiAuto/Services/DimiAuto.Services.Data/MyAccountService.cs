namespace DimiAuto.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using DimiAuto.Data.Common.Repositories;
    using DimiAuto.Data.Models;
    using DimiAuto.Models.CarModel;
    using DimiAuto.Services.Mapping;
    using DimiAuto.Web.ViewModels.MyAccount;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json.Linq;

    public class MyAccountService : IMyAccountService
    {
        private readonly IDeletableEntityRepository<Car> carRepository;
        private readonly IAdService adService;
        private readonly IDeletableEntityRepository<AdView> viewsRepository;
        private readonly IDeletableEntityRepository<Comment> commentRepository;
        private readonly IDeletableEntityRepository<SearchModel> searchModelRepository;
        private readonly IDeletableEntityRepository<UserCarFavorite> favoriteRepository;

        public MyAccountService(IDeletableEntityRepository<Car> carRepository, IAdService adService,
            IDeletableEntityRepository<AdView> viewsRepository, IDeletableEntityRepository<Comment> commentRepository,
            IDeletableEntityRepository<SearchModel> searchModelRepository, IDeletableEntityRepository<UserCarFavorite> favoriteRepository
            )
        {
            this.carRepository = carRepository;
            this.adService = adService;
            this.viewsRepository = viewsRepository;
            this.commentRepository = commentRepository;
            this.searchModelRepository = searchModelRepository;
            this.favoriteRepository = favoriteRepository;
        }

        public async Task<ICollection<Car>> GetMyCarsAsync(string userId)
        => await this.carRepository.All()
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.CreatedOn)
                .ToListAsync();

        public async Task AddAdToFavAsync(string carId, string userId)
        {
            var car = await this.carRepository.All().FirstOrDefaultAsync(x => x.Id == carId);
            if (car == null)
            {
                throw new NullReferenceException();
            }

            var newRecord = new UserCarFavorite
            {
                CarId = carId,
                UserId = userId,
            };

            await this.favoriteRepository.AddAsync(newRecord);
            await this.favoriteRepository.SaveChangesAsync();
        }

        public async Task RemoveFavAdAsync(string carId, string userId)
        {
            var recordForRemove = await this.favoriteRepository.All().FirstOrDefaultAsync(x => x.UserId == userId && x.CarId == carId);
            recordForRemove.IsDeleted = true;
            this.favoriteRepository.Update(recordForRemove);
            await this.favoriteRepository.SaveChangesAsync();
        }

        public async Task<ICollection<TModel>> GetAllFavAdsOnCurrentUserAsync<TModel>(string userId)
        {
            var result = await this.favoriteRepository.All().Where(x => x.UserId == userId).OrderByDescending(x => x.CreatedOn).To<TModel>().ToListAsync();
            return result;
        }

        public async Task DeleteAccountDataAsync(string userId)
        {
            var cars = await this.carRepository.AllWithDeleted().Where(x => x.UserId == userId).ToListAsync();
            var views = await this.viewsRepository.AllWithDeleted().Where(x => x.User == userId).ToListAsync();
            var favorites = await this.favoriteRepository.AllWithDeleted().Where(x => x.UserId == userId).ToListAsync();
            var comments = await this.commentRepository.AllWithDeleted().Where(x => x.UserId == userId).ToListAsync();
            var searchHistory = await this.searchModelRepository.AllWithDeleted().Where(x => x.UserId == userId).ToListAsync();

            foreach (var view in views)
            {
                this.viewsRepository.HardDelete(view);
                await this.viewsRepository.SaveChangesAsync();
            }

            foreach (var favorite in favorites)
            {
                this.favoriteRepository.HardDelete(favorite);
                await this.favoriteRepository.SaveChangesAsync();
            }

            foreach (var comment in comments)
            {
                this.commentRepository.HardDelete(comment);
                await this.commentRepository.SaveChangesAsync();
            }

            foreach (var model in searchHistory)
            {
                this.searchModelRepository.HardDelete(model);
                await this.searchModelRepository.SaveChangesAsync();
            }

            foreach (var car in cars)
            {
                this.carRepository.HardDelete(car);
                await this.carRepository.SaveChangesAsync();
            }
        }
    }
}
