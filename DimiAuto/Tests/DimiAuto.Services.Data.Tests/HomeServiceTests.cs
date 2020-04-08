using DimiAuto.Common;
using DimiAuto.Data;
using DimiAuto.Data.Models.CarModel;
using DimiAuto.Data.Repositories;
using DimiAuto.Models.CarModel;
using DimiAuto.Web.ViewModels.Home;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DimiAuto.Services.Data.Tests
{
    public class HomeServiceTests
    {
        [Fact]
        public async Task GetAllAdsApprovedAndNotDeletedTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var carRepository = new EfDeletableEntityRepository<Car>(new ApplicationDbContext(options.Options));

            var service = new HomeService(carRepository);

            await carRepository.AddAsync(new Car { Make = Make.Audi, Model = "A8", ImgsPaths = GlobalConstants.DefaultImgCar, });
            await carRepository.AddAsync(new Car { Make = Make.Bmw, Model = "M8", IsApproved = true, ImgsPaths = GlobalConstants.DefaultImgCar, });
            await carRepository.AddAsync(new Car { Make = Make.Bmw, Model = "M5", Modification = "Competition", IsApproved = true, ImgsPaths = GlobalConstants.DefaultImgCar, });
            await carRepository.AddAsync(new Car { Make = Make.Bmw, Model = "M3", IsApproved = true, ImgsPaths = GlobalConstants.DefaultImgCar, });
            await carRepository.AddAsync(new Car { Make = Make.Opel, IsDeleted = true, ImgsPaths = GlobalConstants.DefaultImgCar, });
            await carRepository.SaveChangesAsync();

            var result = await service.GetAllAdsAsync();
            var deletedCar = result.FirstOrDefault(x => x.Make == Make.Opel);
            var notApprovedCar = result.FirstOrDefault(x => x.Make == Make.Audi);
            Assert.Equal(3, result.Count);
            Assert.Null(deletedCar);
            Assert.Null(notApprovedCar);

        }

        [Fact]
        public async Task GetAdsByCriteriaApprovedAndNotDeleted()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var carRepository = new EfDeletableEntityRepository<Car>(new ApplicationDbContext(options.Options));

            var service = new HomeService(carRepository);

            await carRepository.AddAsync(new Car {Condition = Condition.New, Make = Make.Audi, Model = "A8", ImgsPaths = GlobalConstants.DefaultImgCar, Price = 1, YearOfProduction = DateTime.Parse("01.2020"), });
            await carRepository.AddAsync(new Car { Condition = Condition.New, Make = Make.Bmw, Model = "M8", IsApproved = true, ImgsPaths = GlobalConstants.DefaultImgCar, Price = 8, YearOfProduction = DateTime.Parse("03.2020"), });
            await carRepository.AddAsync(new Car { Condition = Condition.New, Make = Make.Bmw, Model = "M5", Modification = "Competition", IsApproved = true, ImgsPaths = GlobalConstants.DefaultImgCar, Price = 1100, YearOfProduction = DateTime.Parse("01.2019"), Fuel = Fuel.Gasoline, Gearbox = GearBox.Automatic, TypeOfVeichle = TypeOfVeichle.Car});
            await carRepository.AddAsync(new Car { Condition = Condition.New, Make = Make.Bmw, Model = "M3", IsApproved = true, ImgsPaths = GlobalConstants.DefaultImgCar, Price = 10, YearOfProduction = DateTime.Parse("01.2018"), Gearbox = GearBox.Manual, TypeOfVeichle = TypeOfVeichle.Car, Fuel = Fuel.Gasoline });
            await carRepository.AddAsync(new Car { Condition = Condition.New, Make = Make.Opel, IsDeleted = true, ImgsPaths = GlobalConstants.DefaultImgCar, Price = 13, YearOfProduction = DateTime.Parse("01.2018"), });
            await carRepository.SaveChangesAsync();

            var searchModelByPrice = new SearchInputModel {Condition = Condition.All, Fuel = Fuel.All, GearBox = GearBox.All, Make = Make.All,TypeOfVeichle = TypeOfVeichle.All, PriceFrom = 1, PriceTo = 1200 };
            var searchModelByYear = new SearchInputModel { Condition = Condition.All, Fuel = Fuel.All, GearBox = GearBox.All, Make = Make.All, TypeOfVeichle = TypeOfVeichle.Car, YearFrom = 2018, YearTo = 2019 };
            var searchModelByMultipleCriteria = new SearchInputModel { Condition = Condition.All, Fuel = Fuel.Gasoline, GearBox = GearBox.Manual, Make = Make.All, TypeOfVeichle = TypeOfVeichle.Car };
            var searchModelByMultipleCriteriaNoMatch = new SearchInputModel { Condition = Condition.ForParts, Fuel = Fuel.Gasoline, GearBox = GearBox.Manual, Make = Make.All, TypeOfVeichle = TypeOfVeichle.Car };


            var resultWithPrice = await service.GetAdsByCriteriaAsync(searchModelByPrice);
            var resultWithYear = await service.GetAdsByCriteriaAsync(searchModelByYear);
            var resultWithMultipleCriteria = await service.GetAdsByCriteriaAsync(searchModelByMultipleCriteria);
            var resultWithMultipleCriteriaNoMatch = await service.GetAdsByCriteriaAsync(searchModelByMultipleCriteriaNoMatch);


            Assert.Equal(3, resultWithPrice.Count);
            Assert.Equal(2, resultWithYear.Count);
            Assert.Equal(1, resultWithMultipleCriteria.Count);
            Assert.Equal(0, resultWithMultipleCriteriaNoMatch.Count);




        }
    }
}
