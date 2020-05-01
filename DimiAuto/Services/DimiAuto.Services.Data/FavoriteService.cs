namespace DimiAuto.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using DimiAuto.Data.Common.Repositories;
    using DimiAuto.Data.Models;
    using Microsoft.EntityFrameworkCore;

    public class FavoriteService : IFavoriteService
    {
        private readonly IDeletableEntityRepository<UserCarFavorite> favoriteRepository;

        public FavoriteService(IDeletableEntityRepository<UserCarFavorite> favoriteRepository)
        {
            this.favoriteRepository = favoriteRepository;
        }

        public async Task<UserCarFavorite> GetFavoriteCarByUserAndCarIdAsync(string carId, string userId) =>
            await this.favoriteRepository.All().FirstOrDefaultAsync(x => x.CarId == carId && x.UserId == userId);

    }
}
