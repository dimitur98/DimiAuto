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

        public MyAccountService(IDeletableEntityRepository<Car> carRepository, IAdService adService)
        {
            this.carRepository = carRepository;
            this.adService = adService;
        }

        public async Task<ICollection<MyCarsViewModel>> GetMyCarsAsync(string userId)
        {
            return await this.carRepository.All()
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.CreatedOn)
                .Select(x => new MyCarsViewModel 
                { 
                    Id = x.Id,
                    Model = x.Model,
                    Make = x.Make,
                    CreatedOn = x.CreatedOn,
                    IsApproved = x.IsApproved,
                    ModelToString = this.adService.EnumParser(x.Make.ToString(), x.Model),
                    Modification = x.Modification,
                    Price = x.Price,
                    
                })
                .ToListAsync();
        }
    }
}
