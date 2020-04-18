namespace DimiAuto.Services.Data.Tests
{
    using DimiAuto.Common;
    using DimiAuto.Data;
    using DimiAuto.Data.Models.CarModel;
    using DimiAuto.Data.Repositories;
    using DimiAuto.Models.CarModel;
    using DimiAuto.Services.Data.AreaServices;
    using DimiAuto.Web.ViewModels.Ad;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text;
    using System.Threading.Tasks;
    using Xunit;

    public class AdministrationServiceTests
    {
        [Fact]
        public async Task ApproveTest()
        {
            EfDeletableEntityRepository<Car> carRepository;
            AdministrationService administrationService;
            Car car, car2;
            string firstCarId;
            AdministrationServiceBuildWithCars(out carRepository, out administrationService, out car, out car2, out firstCarId);
            await carRepository.AddAsync(car);
            await carRepository.AddAsync(car2);
            await carRepository.SaveChangesAsync();

            await administrationService.ApproveAsync(firstCarId);
            var approvedCar = await carRepository.All().FirstOrDefaultAsync(x => x.Id == firstCarId);
            Assert.True(approvedCar.IsApproved);
        }

        [Fact]
        public async Task GetAllAdsWithUnapprovedAndDeletedTest()
        {
            EfDeletableEntityRepository<Car> carRepository;
            AdministrationService administrationService;
            Car car, car2;
            string firstCarId;
            AdministrationServiceBuildWithCars(out carRepository, out administrationService, out car, out car2, out firstCarId);
            car.IsDeleted = true;
            await carRepository.AddAsync(car);
            await carRepository.AddAsync(car2);
            await carRepository.SaveChangesAsync();

            
            var allCars = await administrationService.GetAllAdsAsync();
            Assert.Equal(2, allCars.Count);
        }

        [Fact]
        public async Task DeleteAdTest()
        {
            EfDeletableEntityRepository<Car> carRepository;
            AdministrationService administrationService;
            Car car, car2;
            string firstCarId;
            AdministrationServiceBuildWithCars(out carRepository, out administrationService, out car, out car2, out firstCarId);
            await carRepository.AddAsync(car);
            await carRepository.AddAsync(car2);
            await carRepository.SaveChangesAsync();

            await administrationService.DeleteAsync(firstCarId);
            var firstCar = await carRepository.AllWithDeleted().FirstOrDefaultAsync(x => x.Id == firstCarId);
            Assert.True(firstCar.IsDeleted);
        }

        [Fact]
        public async Task UndeleteAdTest()
        {
            EfDeletableEntityRepository<Car> carRepository;
            AdministrationService administrationService;
            Car car, car2;
            string firstCarId;
            AdministrationServiceBuildWithCars(out carRepository, out administrationService, out car, out car2, out firstCarId);
            car.IsDeleted = true;
            await carRepository.AddAsync(car);
            await carRepository.AddAsync(car2);
            await carRepository.SaveChangesAsync();

            await administrationService.UnDeleteAsync(firstCarId);

            var deletedCar = await carRepository.AllWithDeleted().FirstOrDefaultAsync(x => x.Id == firstCarId);
            Assert.False(deletedCar.IsDeleted);
        }

        private static void AdministrationServiceBuildWithCars(out EfDeletableEntityRepository<Car> carRepository, out AdministrationService administrationService, out Car car, out Car car2, out string firstCarId)
        {
            DbContextOptionsBuilder<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            carRepository = new EfDeletableEntityRepository<Car>(new ApplicationDbContext(options.Options));
            var adServiceMock = new Mock<IAdService>();

            administrationService = new AdministrationService(carRepository, adServiceMock.Object);
            car = new Car
            {
                Cc = 1,
                Color = Color.Chameleon,
                Condition = Condition.New,
                Door = Doors.Three,
                EuroStandart = EuroStandart.Euro1,
                Extras = "4x4",
                Fuel = Fuel.Diesel,
                Gearbox = Gearbox.Automatic,
                Horsepowers = 1,
                ImgsPaths = GlobalConstants.DefaultImgCar,
                Km = 100,
                Location = Location.Sofia,
                Make = Make.Audi,
                Model = "test",
                Modification = "test",
                MoreInformation = "test test",
                Price = 100,
                Type = Types.Convertible,
                TypeOfVeichle = TypeOfVeichle.Car,
                YearOfProduction = DateTime.ParseExact("01.1999", "mm.yyyy", CultureInfo.InvariantCulture),
            };
            car2 = new Car
            {
                Cc = 1000,
                Color = Color.Black,
                Condition = Condition.New,
                Door = Doors.Three,
                EuroStandart = EuroStandart.Euro1,
                Extras = "4x4",
                Fuel = Fuel.Diesel,
                Gearbox = Gearbox.Automatic,
                Horsepowers = 1,
                ImgsPaths = GlobalConstants.DefaultImgCar,
                Km = 100,
                Location = Location.Sofia,
                Make = Make.Bmw,
                Model = "test2",
                Modification = "test2",
                MoreInformation = "test2 test2",
                Price = 100,
                Type = Types.Convertible,
                TypeOfVeichle = TypeOfVeichle.Car,
                YearOfProduction = DateTime.ParseExact("01.1999", "mm.yyyy", CultureInfo.InvariantCulture),
            };
            firstCarId = car.Id;
        }
    }
}
