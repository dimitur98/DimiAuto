namespace DimiAuto.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using DimiAuto.Common;
    using DimiAuto.Data;
    using DimiAuto.Data.Models;
    using DimiAuto.Data.Models.CarModel;
    using DimiAuto.Data.Repositories;
    using DimiAuto.Models.CarModel;
    using DimiAuto.Services.Mapping;
    using DimiAuto.Web.ViewModels.Ad;
    using DimiAuto.Web.ViewModels.Ad.CompareAds;
    using DimiAuto.Web.ViewModels.FavoriteAds;
    using DimiAuto.Web.ViewModels.Home;
    using DimiAuto.Web.ViewModels.Sort;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Xunit;

    public class AdServiceTests
    {
        [Fact]
        public async Task CreatAdAsyncTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var carRepository = new EfDeletableEntityRepository<Car>(new ApplicationDbContext(options.Options));
            var userCarFavRepository = new EfDeletableEntityRepository<UserCarFavorite>(new ApplicationDbContext(options.Options));

            var service = new AdService(carRepository, userCarFavRepository);
            var inputModel = new CreateAdInputModel
            {
                Cc = 1,
                Color = 0,
                Condition = Condition.New,
                Door = Doors.Three,
                EuroStandart = EuroStandart.Euro1,
                Extras = "4x4",
                Fuel = Fuel.Diesel,
                GearBox = GearBox.Automatic,
                Hp = 1,
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
                YearOfProduction = "01.1999",
            };
            var result = await service.CreateAdAsync(inputModel, "1");
            var carsInDb = await carRepository.All().CountAsync();
            var car = await carRepository.All().FirstOrDefaultAsync(x => x.Model == "test");
            Assert.NotNull(result);
            Assert.Equal(1, carsInDb);
            Assert.Equal(car.Model, inputModel.Model);
            Assert.Equal(car.Make, inputModel.Make);
            Assert.Equal(car.Price, inputModel.Price);
        }

        [Fact]
        public async Task GetCurrentCarAsyncTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var carRepository = new EfDeletableEntityRepository<Car>(new ApplicationDbContext(options.Options));
            var userCarFavRepository = new EfDeletableEntityRepository<UserCarFavorite>(new ApplicationDbContext(options.Options));

            var service = new AdService(carRepository, userCarFavRepository);

            var inputModel = new CreateAdInputModel
            {
                Cc = 1,
                Color = 0,
                Condition = Condition.New,
                Door = Doors.Three,
                EuroStandart = EuroStandart.Euro1,
                Extras = "4x4",
                Fuel = Fuel.Diesel,
                GearBox = GearBox.Automatic,
                Hp = 1,
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
                YearOfProduction = "01.1999",
            };

            await service.CreateAdAsync(inputModel, "1");
            var car = await carRepository.All().FirstOrDefaultAsync(x => x.Model == "test");
            var carId = car.Id;
            var carByService = await service.GetCurrentCarAsync(carId);
            Assert.Same(car, carByService);
            Assert.Equal(car.Id, carByService.Id);
        }

        [Fact]
        public async Task EditAdTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var carRepository = new EfDeletableEntityRepository<Car>(new ApplicationDbContext(options.Options));
            var userCarFavRepository = new EfDeletableEntityRepository<UserCarFavorite>(new ApplicationDbContext(options.Options));

            var service = new AdService(carRepository, userCarFavRepository);

            var inputModel = new CreateAdInputModel
            {
                Cc = 1,
                Color = 0,
                Condition = Condition.New,
                Door = Doors.Three,
                EuroStandart = EuroStandart.Euro1,
                Extras = "4x4",
                Fuel = Fuel.Diesel,
                GearBox = GearBox.Automatic,
                Hp = 1,
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
                YearOfProduction = "01.1999",
            };
            await service.CreateAdAsync(inputModel, "1");
            var car = await carRepository.All().FirstOrDefaultAsync(x => x.Model == "test");
            var oldMake = car.Make;
            var oldMoreInfo = car.MoreInformation;
            var oldCc = car.Cc;
            var editCC = 3;
            var editMake = Make.Bmw;
            var editMoreInfo = "edit test";
            var editAd = new EditAddInputModel
            {
                Id = car.Id,
                Cc = editCC,
                Color = car.Color,
                Door = car.Door,
                EuroStandart = car.EuroStandart,
                Extras = car.Extras,
                Fuel = car.Fuel,
                GearBox = car.Gearbox,
                Hp = car.Horsepowers,
                Km = car.Km,
                Location = car.Location,
                Make = editMake,
                Model = car.Model,
                Modification = car.Modification,
                MoreInformation = editMoreInfo,
                Price = car.Price,
                Type = car.Type,
                Condition = car.Condition,
                YearOfProduction = car.YearOfProduction.ToString("mm.yyyy"),
                TypeOfVeichle = car.TypeOfVeichle,
            };
            var edittedCar = await service.EditAd(editAd);
            Assert.Equal(edittedCar.Make, editMake);
            Assert.Equal(edittedCar.Cc, editCC);
            Assert.Equal(edittedCar.MoreInformation, editMoreInfo);
            Assert.NotEqual(edittedCar.Make, oldMake);
            Assert.NotEqual(edittedCar.Cc, oldCc);
            Assert.NotEqual(edittedCar.MoreInformation, oldMoreInfo);

        }

        [Fact]
        public async Task EditAdWithWrongIdShouldRedturnNullReferenceException()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var carRepository = new EfDeletableEntityRepository<Car>(new ApplicationDbContext(options.Options));
            var userCarFavRepository = new EfDeletableEntityRepository<UserCarFavorite>(new ApplicationDbContext(options.Options));

            var service = new AdService(carRepository, userCarFavRepository);
            var editAd = new EditAddInputModel
            {
                Id = "fakeId",
                Cc = 0,
                Color = Color.Beige,
                Door = Doors.Five,
                EuroStandart = EuroStandart.Euro5,
                Extras = "asd",
                Fuel = Fuel.Diesel,
                GearBox = GearBox.Automatic,
                Hp = 2,
                Km = 2,
                Location = Location.Sofia,
                Make = Make.AstonMartin,
                Model = "tesr",
                Modification = "test",
                MoreInformation = "test",
                Price = 33,
                Type = Types.Combi,
                Condition = Condition.ForParts,
                YearOfProduction = "03.1999",
                TypeOfVeichle = TypeOfVeichle.All,
            };
            Assert.ThrowsAsync<NullReferenceException>(async ()=> await service.EditAd(editAd));

        }

        [Fact]
        public async Task AddToFavAsyncTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var carRepository = new EfDeletableEntityRepository<Car>(new ApplicationDbContext(options.Options));
            var userCarFavRepository = new EfDeletableEntityRepository<UserCarFavorite>(new ApplicationDbContext(options.Options));

            var car = new Car();
            await carRepository.AddAsync(car);
            await carRepository.SaveChangesAsync();
            var service = new AdService(carRepository, userCarFavRepository);
            var userId = "UserId";
            var carId = car.Id;
            await service.AddAdToFavAsync(carId, userId);

            var userCarFavRecord = await userCarFavRepository.All().FirstAsync();

            Assert.NotNull(userCarFavRecord);
            Assert.Equal(carId, userCarFavRecord.CarId);
            Assert.Equal(userId, userCarFavRecord.UserId);

        }

        [Fact]
        public async Task AddToFavWithNotFoundCarShouldReturnNullRefferenceException()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var carRepository = new EfDeletableEntityRepository<Car>(new ApplicationDbContext(options.Options));
            var userCarFavRepository = new EfDeletableEntityRepository<UserCarFavorite>(new ApplicationDbContext(options.Options));
            var service = new AdService(carRepository, userCarFavRepository);

            var userId = "UserId";
            var carId = "notFoundId";
           Assert.ThrowsAsync<NullReferenceException>(async () => await service.AddAdToFavAsync(carId, userId));
        }

        [Fact]
        public async Task RemoveFavAdAsync()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var carRepository = new EfDeletableEntityRepository<Car>(new ApplicationDbContext(options.Options));
            var userCarFavRepository = new EfDeletableEntityRepository<UserCarFavorite>(new ApplicationDbContext(options.Options));

            var service = new AdService(carRepository, userCarFavRepository);

            var car = new Car();
            await carRepository.AddAsync(car);
            await carRepository.SaveChangesAsync();

            var userId = "UserId";
            var carId = car.Id;

            await service.AddAdToFavAsync(carId, userId);
            await service.RemoveFavAdAsync(carId, userId);

            var userCarFavRecord = await userCarFavRepository.All().FirstOrDefaultAsync(x => x.UserId == userId);

            Assert.Null(userCarFavRecord);
        }

        [Fact]
        public async Task GetAllFavAdsOnCurrentUserTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var carRepository = new EfDeletableEntityRepository<Car>(new ApplicationDbContext(options.Options));
            var userCarFavRepository = new EfDeletableEntityRepository<UserCarFavorite>(new ApplicationDbContext(options.Options));

            var service = new AdService(carRepository, userCarFavRepository);

            var firstCar = new CreateAdInputModel
            {
                Cc = 1,
                Color = 0,
                Condition = Condition.New,
                Door = Doors.Three,
                EuroStandart = EuroStandart.Euro1,
                Extras = "4x4",
                Fuel = Fuel.Diesel,
                GearBox = GearBox.Automatic,
                Hp = 1,
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
                YearOfProduction = "01.1999",
            };
            var secondCar = new CreateAdInputModel
            {
                Cc = 1,
                Color = 0,
                Condition = Condition.New,
                Door = Doors.Three,
                EuroStandart = EuroStandart.Euro1,
                Extras = "4x4",
                Fuel = Fuel.Gasoline,
                GearBox = GearBox.Manual,
                Hp = 1,
                ImgsPaths = GlobalConstants.DefaultImgCar,
                Km = 100,
                Location = Location.Sofia,
                Make = Make.Bmw,
                Model = "test",
                Modification = "test",
                MoreInformation = "test test",
                Price = 100,
                Type = Types.Convertible,
                TypeOfVeichle = TypeOfVeichle.Car,
                YearOfProduction = "01.1999",
            };
            var thirdCar = new CreateAdInputModel
            {
                Cc = 1,
                Color = 0,
                Condition = Condition.New,
                Door = Doors.Three,
                EuroStandart = EuroStandart.Euro1,
                Extras = "4x4",
                Fuel = Fuel.Diesel,
                GearBox = GearBox.Automatic,
                Hp = 1,
                ImgsPaths = GlobalConstants.DefaultImgCar,
                Km = 100,
                Location = Location.Sofia,
                Make = Make.Ford,
                Model = "test",
                Modification = "test",
                MoreInformation = "test test",
                Price = 100000,
                Type = Types.Convertible,
                TypeOfVeichle = TypeOfVeichle.Car,
                YearOfProduction = "01.2000",
            };
            var firstCarId = await service.CreateAdAsync(firstCar, "1");
            var secondCarId = await service.CreateAdAsync(secondCar, "1");
            await service.CreateAdAsync(thirdCar, "1");

            await service.AddAdToFavAsync(firstCarId, "1");
            await service.AddAdToFavAsync(secondCarId, "1");
            AutoMapperConfig.RegisterMappings(typeof(UserAdViewModel).Assembly);

            var result = await service.GetAllFavAdsOnCurrentUserAsync<UserAdViewModel>("1");

            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void EnumParserTestsShouldReturnStringModel()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var carRepository = new EfDeletableEntityRepository<Car>(new ApplicationDbContext(options.Options));
            var userCarFavRepository = new EfDeletableEntityRepository<UserCarFavorite>(new ApplicationDbContext(options.Options));

            var service = new AdService(carRepository, userCarFavRepository);

            // should return - 80
            var audi = service.EnumParser("Audi", "1");

            // should return - 640
            var bmw = service.EnumParser("Bmw", "40");

            // should return - Marea
            var fiat = service.EnumParser("Fiat", "12");

            // should return - iMiEV
            var mitsubishi = service.EnumParser("Mitsubishi", "10");

            Assert.Equal("80", audi);
            Assert.Equal("640", bmw);
            Assert.Equal("Marea", fiat);
            Assert.Equal("iMiEV", mitsubishi);
        }

        [Fact]
        public void EnumParserTestShouldRetunNull()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var carRepository = new EfDeletableEntityRepository<Car>(new ApplicationDbContext(options.Options));
            var userCarFavRepository = new EfDeletableEntityRepository<UserCarFavorite>(new ApplicationDbContext(options.Options));

            var service = new AdService(carRepository, userCarFavRepository);


            Assert.Throws<NullReferenceException>(() => service.EnumParser("Lada", "1"));
            Assert.Throws<IndexOutOfRangeException>(() => service.EnumParser("Audi", "100"));

        }
    }
}
