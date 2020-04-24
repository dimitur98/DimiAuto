namespace DimiAuto.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using DimiAuto.Common;
    using DimiAuto.Data.Common.Repositories;
    using DimiAuto.Data.Models;
    using DimiAuto.Models.CarModel;
    using Microsoft.EntityFrameworkCore;

    public class ViewService : IViewService
    {
        private readonly IDeletableEntityRepository<AdView> viewRepository;

        public ViewService(IDeletableEntityRepository<AdView> viewRepository)
        {
            this.viewRepository = viewRepository;
        }

        public async Task AddViewAsync(string user, string carId)
        {
            if (user != GlobalConstants.NotRegisterUserId)
            {
                var view = await this.viewRepository.All().FirstOrDefaultAsync(x => x.User == user && x.CarId == carId);
                if (view == null)
                {
                    await this.CreateViewAsync(user, carId);
                }
            }
            else
            {
                await this.CreateViewAsync(user, carId);
            }
        }

        public int GetViewsCount(string carId)
        {
            return this.viewRepository.All().Where(x => x.CarId == carId).Count();
        }

        private async Task CreateViewAsync(string user, string carId)
        {
            var newView = new AdView
            {
                CarId = carId,
                User = user,
            };
            await this.viewRepository.AddAsync(newView);
            await this.viewRepository.SaveChangesAsync();
        }
    }
}
