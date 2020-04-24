namespace DimiAuto.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using DimiAuto.Common;
    using DimiAuto.Data;
    using DimiAuto.Data.Models;
    using DimiAuto.Data.Models.CarModel;
    using DimiAuto.Data.Repositories;
    using DimiAuto.Models.CarModel;
    using DimiAuto.Web.ViewModels.Ad;
    using DimiAuto.Web.ViewModels.Home;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Xunit;

    public class HomeServiceTests
    {
        [Fact]
        public async Task GetAllAdsApprovedAndNotDeletedAsyncTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString());
            var carRepository = new EfDeletableEntityRepository<Car>(new ApplicationDbContext(options.Options));
            var adService = new Mock<IAdService>();
            var viewService = new Mock<IViewService>();

            var homeService = new HomeService(carRepository, adService.Object, viewService.Object);
            await SeedData(carRepository);

            var result = await homeService.GetAllAdsAsync();
            var deletedCar = result.FirstOrDefault(x => x.Make == Make.Opel);
            var notApprovedCar = result.FirstOrDefault(x => x.Make == Make.Audi);
            Assert.Equal(5, result.Count);
            Assert.Null(deletedCar);
            Assert.Null(notApprovedCar);
        }

        [Fact]
        public async Task GetAdsByCriteriaApprovedAndNotDeletedAsyncTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString());
            var carRepository = new EfDeletableEntityRepository<Car>(new ApplicationDbContext(options.Options));
            var adService = new Mock<IAdService>();
            var viewService = new Mock<IViewService>();

            var homeService = new HomeService(carRepository, adService.Object, viewService.Object);
            await SeedData(carRepository);

            var searchModelByPrice = new SearchInputModel { Location = Location.All, Condition = Condition.All, Fuel = Fuel.All, Gearbox = Gearbox.All, Make = Make.All, TypeOfVeichle = TypeOfVeichle.All, PriceFrom = 1, PriceTo = 1200 };
            var searchModelByYear = new SearchInputModel { Location = Location.All, Condition = Condition.All, Fuel = Fuel.All, Gearbox = Gearbox.All, Make = Make.All, TypeOfVeichle = TypeOfVeichle.Car, YearFrom = 2018, YearTo = 2019 };
            var searchModelByMultipleCriteria = new SearchInputModel { Location = Location.All, Condition = Condition.All, Fuel = Fuel.Gasoline, Gearbox = Gearbox.Manual, Make = Make.All, TypeOfVeichle = TypeOfVeichle.Car };
            var searchModelByMultipleCriteriaNoMatch = new SearchInputModel { Location = Location.All, Condition = Condition.ForParts, Fuel = Fuel.Gasoline, Gearbox = Gearbox.Manual, Make = Make.All, TypeOfVeichle = TypeOfVeichle.Car };

            var resultWithPrice = await homeService.GetAdsByCriteriaAsync(searchModelByPrice);
            var resultWithYear = await homeService.GetAdsByCriteriaAsync(searchModelByYear);
            var resultWithMultipleCriteria = await homeService.GetAdsByCriteriaAsync(searchModelByMultipleCriteria);
            var resultWithMultipleCriteriaNoMatch = await homeService.GetAdsByCriteriaAsync(searchModelByMultipleCriteriaNoMatch);

            Assert.Equal(5, resultWithPrice.Count);
            Assert.Equal(4, resultWithYear.Count);
            Assert.Equal(3, resultWithMultipleCriteria.Count);
            Assert.Equal(0, resultWithMultipleCriteriaNoMatch.Count);
        }

        [Fact]
        public async Task GetApprovedAndNotDeletedCarsOfUserAsyncTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString());
            var carRepository = new EfDeletableEntityRepository<Car>(new ApplicationDbContext(options.Options));
            var adService = new Mock<IAdService>();
            var viewService = new Mock<IViewService>();

            var homeService = new HomeService(carRepository, adService.Object, viewService.Object);

            await SeedData(carRepository);

            var result = await homeService.GetCarsOfUserAsync("1");

            Assert.Equal(5, result.Count);
        }

        [Fact]
        public async Task GetTopFourMostWatchedCarsOfUserAsyncTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString());
            var carRepository = new EfDeletableEntityRepository<Car>(new ApplicationDbContext(options.Options));
            var viewRepository = new EfDeletableEntityRepository<AdView>(new ApplicationDbContext(options.Options));
            var adService = new Mock<IAdService>();
            var viewService = new ViewService(viewRepository);

            var homeService = new HomeService(carRepository, adService.Object, viewService);

            await SeedData(carRepository);

            // id=car2-first, id=car4-second(created later than id=car3), id=car3 - third, id=car5-fourth id=car1-not approved
            await viewRepository.AddAsync(new AdView { CarId = "car1", User = GlobalConstants.NotRegisterUserId });
            await viewRepository.AddAsync(new AdView { CarId = "car2", User = "id" });
            await viewRepository.AddAsync(new AdView { CarId = "car2", User = GlobalConstants.NotRegisterUserId });
            await viewRepository.AddAsync(new AdView { CarId = "car2", User = GlobalConstants.NotRegisterUserId });
            await viewRepository.AddAsync(new AdView { CarId = "car3", User = GlobalConstants.NotRegisterUserId });
            await viewRepository.AddAsync(new AdView { CarId = "car3", User = GlobalConstants.NotRegisterUserId });
            await viewRepository.AddAsync(new AdView { CarId = "car4", User = GlobalConstants.NotRegisterUserId });
            await viewRepository.AddAsync(new AdView { CarId = "car4", User = GlobalConstants.NotRegisterUserId });
            await viewRepository.AddAsync(new AdView { CarId = "car5", User = GlobalConstants.NotRegisterUserId });
            await viewRepository.SaveChangesAsync();

            var result = await homeService.GetTopFourMostWatchedCarsOfUserAsync("1");

            Assert.Equal("car2", result.ToArray()[0].Id);
            Assert.Equal("car4", result.ToArray()[1].Id);
            Assert.Equal("car3", result.ToArray()[2].Id);
            Assert.Equal("car5", result.ToArray()[3].Id);
        }

        [Fact]
        public async Task PagingTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString());
            var carRepository = new EfDeletableEntityRepository<Car>(new ApplicationDbContext(options.Options));
            var adService = new Mock<IAdService>();
            var viewService = new Mock<IViewService>();

            var homeService = new HomeService(carRepository, adService.Object, viewService.Object);

            await SeedData(carRepository);
            var cars = await carRepository.All().Where(x => x.IsApproved).ToListAsync();

            // order of cars: car2, car3, car4, car5, car6
            var output = new AllCarsModel
            {
                AllCars = await homeService.GetAllAdsAsync(),
            };
            output.AllCars = output.AllCars.OrderBy(x => x.Id).ToArray();
            int carsPerPage = 2;
            int page = 1;
            var result = homeService.Paging(output, carsPerPage, (page - 1) * carsPerPage);
            Assert.Equal("car2", result.AllCars.ToArray()[0].Id);
            Assert.Equal("car3", result.AllCars.ToArray()[1].Id);

            output = new AllCarsModel
            {
                AllCars = await homeService.GetAllAdsAsync(),
            };
            output.AllCars = output.AllCars.OrderBy(x => x.Id).ToArray();
            page = 2;
            result = homeService.Paging(output, carsPerPage, (page - 1) * carsPerPage);
            Assert.Equal("car4", result.AllCars.ToArray()[0].Id);
            Assert.Equal("car5", result.AllCars.ToArray()[1].Id);

            output = new AllCarsModel
            {
                AllCars = await homeService.GetAllAdsAsync(),
            };
            output.AllCars = output.AllCars.OrderBy(x => x.Id).ToArray();
            page = 3;
            result = homeService.Paging(output, carsPerPage, (page - 1) * carsPerPage);
            Assert.Equal("car6", result.AllCars.ToArray()[0].Id);
        }

        private static async Task SeedData(EfDeletableEntityRepository<Car> carRepository)
        {
            await carRepository.AddAsync(new Car { Id = "car1", UserId = "1", Condition = Condition.New, Make = Make.Audi, Model = "8", ImgsPaths = GlobalConstants.DefaultImgCar, Price = 1, YearOfProduction = DateTime.Parse("01.2020"), });
            await carRepository.AddAsync(new Car { Id = "car2", UserId = "1", Condition = Condition.New, Make = Make.Bmw, Model = "8", IsApproved = true, ImgsPaths = GlobalConstants.DefaultImgCar, Price = 8, YearOfProduction = DateTime.Parse("03.2020"), MoreInformation = "test", });
            await carRepository.AddAsync(new Car { Id = "car3", UserId = "1", Condition = Condition.New, Make = Make.Bmw, Model = "5", IsApproved = true, ImgsPaths = GlobalConstants.DefaultImgCar, Price = 1100, YearOfProduction = DateTime.Parse("01.2019"), Fuel = Fuel.Gasoline, Gearbox = Gearbox.Automatic, TypeOfVeichle = TypeOfVeichle.Car, MoreInformation = "test", });
            await carRepository.AddAsync(new Car { Id = "car4", UserId = "1", Condition = Condition.New, Make = Make.Bmw, Model = "3", IsApproved = true, ImgsPaths = GlobalConstants.DefaultImgCar, Price = 10, YearOfProduction = DateTime.Parse("01.2018"), Gearbox = Gearbox.Manual, TypeOfVeichle = TypeOfVeichle.Car, Fuel = Fuel.Gasoline, MoreInformation = "test", });
            await carRepository.AddAsync(new Car { Id = "car5", UserId = "1", Condition = Condition.New, Make = Make.Bmw, Model = "10", IsApproved = true, ImgsPaths = GlobalConstants.DefaultImgCar, Price = 10, YearOfProduction = DateTime.Parse("01.2018"), Gearbox = Gearbox.Manual, TypeOfVeichle = TypeOfVeichle.Car, Fuel = Fuel.Gasoline, MoreInformation = "test", });
            await carRepository.AddAsync(new Car { Id = "car6", UserId = "1", Condition = Condition.New, Make = Make.Bmw, Model = "30", IsApproved = true, ImgsPaths = GlobalConstants.DefaultImgCar, Price = 10, YearOfProduction = DateTime.Parse("01.2018"), Gearbox = Gearbox.Manual, TypeOfVeichle = TypeOfVeichle.Car, Fuel = Fuel.Gasoline, MoreInformation = "test", });
            await carRepository.AddAsync(new Car { Condition = Condition.New, Make = Make.Opel, IsDeleted = true, ImgsPaths = GlobalConstants.DefaultImgCar, Price = 13, YearOfProduction = DateTime.Parse("01.2018"), });
            await carRepository.SaveChangesAsync();
        }
    }
}
