using DimiAuto.Data.Common.Repositories;
using DimiAuto.Data.Models;
using DimiAuto.Models.CarModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DimiAuto.Services.Data
{
    public class ViewService : IViewService
    {
        private readonly IDeletableEntityRepository<AdView> viewRepository;

        public ViewService(IDeletableEntityRepository<AdView> viewRepository)
        {
            this.viewRepository = viewRepository;
        }

        public async Task AddViewAsync(string userId, string carId)
        {

            var newView = new AdView
            {
                CarId = carId,
                UserId = userId,
            };
            await this.viewRepository.AddAsync(newView);
            await this.viewRepository.SaveChangesAsync();

        }

        public int GetViewsCount(string carId)
        {
            return this.viewRepository.All().Where(x => x.CarId == carId).Count();
        }

        
    }
}
