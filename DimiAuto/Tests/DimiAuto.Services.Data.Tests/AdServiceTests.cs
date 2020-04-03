﻿namespace DimiAuto.Services.Data.Tests
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
    using DimiAuto.Web.ViewModels.FavoriteAds;
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
                Location = "Sofia",
                Make = Make.Audi,
                Model = "test",
                Modification = "test",
                MoreInformation = "test test",
                Price = 100,
                Type = Types.Convertible,
                TypeOfVeichle = TypeOfVeichle.Truck,
                YearOfProduction = DateTime.Parse("01.01.1999"),
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
                Location = "Sofia",
                Make = Make.Audi,
                Model = "test",
                Modification = "test",
                MoreInformation = "test test",
                Price = 100,
                Type = Types.Convertible,
                TypeOfVeichle = TypeOfVeichle.Truck,
                YearOfProduction = DateTime.Parse("01.01.1999"),
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
                Location = "Sofia",
                Make = Make.Audi,
                Model = "test",
                Modification = "test",
                MoreInformation = "test test",
                Price = 100,
                Type = Types.Convertible,
                TypeOfVeichle = TypeOfVeichle.Truck,
                YearOfProduction = DateTime.Parse("01.01.1999"),
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
                Id = "id=" + car.Id,
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
                YearOfProduction = car.YearOfProduction,
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
        public async Task AddToFavAsyncTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var carRepository = new EfDeletableEntityRepository<Car>(new ApplicationDbContext(options.Options));
            var userCarFavRepository = new EfDeletableEntityRepository<UserCarFavorite>(new ApplicationDbContext(options.Options));

            var service = new AdService(carRepository, userCarFavRepository);
            var userId = "UserId";
            var carId = "CarId";
            await service.AddAdToFavAsync(carId, userId);

            var userCarFavRecord = await userCarFavRepository.All().FirstAsync();

            Assert.NotNull(userCarFavRecord);
            Assert.Equal(carId, userCarFavRecord.CarId);
            Assert.Equal(userId, userCarFavRecord.UserId);

        }

        [Fact]
        public async Task RemoveFavAdAsync()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var carRepository = new EfDeletableEntityRepository<Car>(new ApplicationDbContext(options.Options));
            var userCarFavRepository = new EfDeletableEntityRepository<UserCarFavorite>(new ApplicationDbContext(options.Options));

            var service = new AdService(carRepository, userCarFavRepository);
            var userId = "UserId";
            var carId = "CarId";
            await service.AddAdToFavAsync(carId, userId);
            await service.RemoveFavAdAsync(carId, userId);
            var userCarFavRecord = await userCarFavRepository.All().FirstOrDefaultAsync(x => x.UserId == userId);
            Assert.Null(userCarFavRecord);
        }
     
    }
}
