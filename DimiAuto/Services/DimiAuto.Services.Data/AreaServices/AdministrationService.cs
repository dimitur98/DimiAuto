using DimiAuto.Data.Common.Repositories;
using DimiAuto.Models.CarModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DimiAuto.Services.Mapping;
using DimiAuto.Data.Models;

namespace DimiAuto.Services.Data.AreaServices
{
    public class AdministrationService : IAdministrationService
    {
        private readonly IDeletableEntityRepository<Car> carRepository;
        private readonly IDeletableEntityRepository<Comment> commentRepository;

        public AdministrationService(IDeletableEntityRepository<Car> carRepository, IDeletableEntityRepository<Comment> commentRepository)
        {
            this.carRepository = carRepository;
            this.commentRepository = commentRepository;
        }

        public async Task ApproveAsync(string carId)
        {
            var car = await this.carRepository.All().FirstOrDefaultAsync(x => x.Id == carId);
            car.IsApproved = true;
            this.carRepository.Update(car);
            await this.carRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<TModel>> GetAllAdsAsync<TModel>()
        {
            var result = await this.carRepository.AllWithDeleted().OrderByDescending(x => x.CreatedOn).ThenBy(x => x.Model ).To<TModel>().ToListAsync();
            return result;
        }

        public async Task DeleteAsync(string carId)
        {
            var car = await this.carRepository.All().FirstOrDefaultAsync(x => x.Id == carId);
            car.IsDeleted = true;
            this.carRepository.Update(car);
            await this.carRepository.SaveChangesAsync();

        }

        public async Task UnDeleteAsync(string carId)
        {
            var car = await this.carRepository.AllWithDeleted().FirstOrDefaultAsync(x => x.Id == carId);
            car.IsDeleted = false;
            this.carRepository.Update(car);
            await this.carRepository.SaveChangesAsync();

        }

        //public async Task PermamentDeleteAsync(string carId)
        //{
        //    var car = await this.carRepository.AllWithDeleted().FirstOrDefaultAsync(x => x.Id == carId);
        //    if (car.Comments.Count >= 1)
        //    {
        //        foreach (var comment in car.Comments)
        //        {
        //            this.commentRepository.HardDelete(comment);
        //        await this.commentRepository.SaveChangesAsync();

        //        }
        //    }
        //    this.carRepository.HardDelete(car);
        //    await this.carRepository.SaveChangesAsync();
        //}
    }
}
