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

        public MyAccountService(IDeletableEntityRepository<Car> carRepository)
        {
            this.carRepository = carRepository;
        }

        public async Task<ICollection<TModel>> GetMyCarsAsync<TModel>(string userId)
        {
            return await this.carRepository.All().Where(x => x.UserId == userId).To<TModel>().ToListAsync();
        }
    }
}
