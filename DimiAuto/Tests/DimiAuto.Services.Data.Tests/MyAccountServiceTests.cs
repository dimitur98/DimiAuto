using DimiAuto.Common;
using DimiAuto.Data;
using DimiAuto.Data.Common.Repositories;
using DimiAuto.Data.Models;
using DimiAuto.Data.Models.CarModel;
using DimiAuto.Data.Repositories;
using DimiAuto.Models.CarModel;
using DimiAuto.Services.Mapping;
using DimiAuto.Web.ViewModels.Ad;
using DimiAuto.Web.ViewModels.MyAccount;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DimiAuto.Services.Data.Tests
{
    public class MyAccountServiceTests
    {
        [Fact]
        public async Task GetMyCarsTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var carRepository = new EfDeletableEntityRepository<Car>(new ApplicationDbContext(options.Options));
            var viewsRepository = new EfDeletableEntityRepository<AdView>(new ApplicationDbContext(options.Options));
            var commentRepository = new EfDeletableEntityRepository<Comment> (new ApplicationDbContext(options.Options));
            var searchModelRepository = new EfDeletableEntityRepository<SearchModel> (new ApplicationDbContext(options.Options));
            var favoriteRepository = new EfDeletableEntityRepository<UserCarFavorite>(new ApplicationDbContext(options.Options));
            var adService = new Mock<IAdService>();

            var service = new MyAccountService(carRepository, adService.Object, viewsRepository, commentRepository, searchModelRepository, favoriteRepository);

            var firstCar = new Car
            {
                Cc = 1,
                Color = 0,
                Condition = Condition.New,
                Door = Doors.Three,
                EuroStandart = EuroStandart.Euro1,
                Extras = "4x4",
                Fuel = Fuel.Diesel,
                Gearbox = GearBox.Automatic,
                Horsepowers = 1,
                ImgsPaths = GlobalConstants.DefaultImgCar,
                Km = 100,
                Location = "Sofia",
                Make = Make.Audi,
                Model = "test",
                Modification = "test",
                MoreInformation = "test test",
                Price = 100,
                Type = Types.Convertible,
                TypeOfVeichle = TypeOfVeichle.Car,
                YearOfProduction = DateTime.Parse("01.01.1999"),
                UserId = "1",

            };

            var secondCar = new Car
            {
                Cc = 1,
                Color = 0,
                Condition = Condition.New,
                Door = Doors.Three,
                EuroStandart = EuroStandart.Euro1,
                Extras = "4x4",
                Fuel = Fuel.Gasoline,
                Gearbox = GearBox.Manual,
                Horsepowers = 1,
                ImgsPaths = GlobalConstants.DefaultImgCar,
                Km = 100,
                Location = "Sofia",
                Make = Make.Bmw,
                Model = "test",
                Modification = "test",
                MoreInformation = "test test",
                Price = 100,
                Type = Types.Convertible,
                TypeOfVeichle = TypeOfVeichle.Car,
                YearOfProduction = DateTime.Parse("01.01.1999"),
                UserId = "2",
            };

            await carRepository.AddAsync(firstCar);
            await carRepository.AddAsync(secondCar);
            await carRepository.SaveChangesAsync();

            AutoMapperConfig.RegisterMappings(typeof(MyCarsViewModel).Assembly);
            var result = await service.GetMyCarsAsync("1");
            Assert.Equal(1, result.Count);
        }
    }
}
