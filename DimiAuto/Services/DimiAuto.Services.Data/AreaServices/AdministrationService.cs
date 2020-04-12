namespace DimiAuto.Services.Data.AreaServices
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
    using DimiAuto.Web.ViewModels.Administration;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    public class AdministrationService : IAdministrationService
    {
        private readonly IDeletableEntityRepository<Car> carRepository;
        private readonly IAdService adService;

        public AdministrationService(IDeletableEntityRepository<Car> carRepository, IAdService adService)
        {
            this.carRepository = carRepository;
            this.adService = adService;
        }

        public async Task ApproveAsync(string carId)
        {
            var car = await this.carRepository.All().FirstOrDefaultAsync(x => x.Id == carId);
            car.IsApproved = true;
            this.carRepository.Update(car);
            await this.carRepository.SaveChangesAsync();
        }

        public async Task<ICollection<AdViewModel>> GetAllAdsAsync()
        {
            var result = await this.carRepository.AllWithDeleted()
                .OrderByDescending(x => x.CreatedOn)
                .ThenBy(x => x.Model).Select(x => new AdViewModel
                {
                    Make = x.Make,
                    Model = x.Model,
                    ModelToString = this.adService.EnumParser(x.Make.ToString(), x.Model),
                    CreatedOn = x.CreatedOn,
                    Id = x.Id,
                    IsApproved = x.IsApproved,
                    IsDeleted = x.IsDeleted,
                    Modification = x.Modification,
                    UserId = x.UserId,
                    User = x.User,
                })
                .ToListAsync();
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

        // public async Task PermamentDeleteAsync(string carId)
        // {
        //    var car = await this.carRepository.AllWithDeleted().FirstOrDefaultAsync(x => x.Id == carId);
        //    if (car.Comments.Count >= 1)
        //    {
        //        foreach (var comment in car.Comments)
        //        {
        //            this.commentRepository.HardDelete(comment);
        //        await this.commentRepository.SaveChangesAsync();

        // }
        //    }
        //    this.carRepository.HardDelete(car);
        //    await this.carRepository.SaveChangesAsync();
        // }
    }
}
