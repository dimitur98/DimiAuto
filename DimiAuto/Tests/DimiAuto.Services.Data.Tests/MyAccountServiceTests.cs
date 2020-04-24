namespace DimiAuto.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text;
    using System.Threading.Tasks;

    using DimiAuto.Common;
    using DimiAuto.Data;
    using DimiAuto.Data.Common.Repositories;
    using DimiAuto.Data.Models;
    using DimiAuto.Data.Models.CarModel;
    using DimiAuto.Data.Repositories;
    using DimiAuto.Models.CarModel;
    using DimiAuto.Services.Mapping;
    using DimiAuto.Web.ViewModels.Ad;
    using DimiAuto.Web.ViewModels.FavoriteAds;
    using DimiAuto.Web.ViewModels.MyAccount;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Xunit;

    public class MyAccountServiceTests
    {
        [Fact]
        public async Task GetMyCarsTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString());
            var carRepository = new EfDeletableEntityRepository<Car>(new ApplicationDbContext(options.Options));
            var viewsRepository = new EfDeletableEntityRepository<AdView>(new ApplicationDbContext(options.Options));
            var commentRepository = new EfDeletableEntityRepository<Comment>(new ApplicationDbContext(options.Options));
            var searchModelRepository = new EfDeletableEntityRepository<SearchModel>(new ApplicationDbContext(options.Options));
            var favoriteRepository = new EfDeletableEntityRepository<UserCarFavorite>(new ApplicationDbContext(options.Options));
            var adService = new Mock<IAdService>();

            var service = new MyAccountService(carRepository, adService.Object, viewsRepository, commentRepository, searchModelRepository, favoriteRepository);
            await SeedData(carRepository);

            AutoMapperConfig.RegisterMappings(typeof(MyCarsViewModel).Assembly);
            var result = await service.GetMyCarsAsync("1");
            Assert.Equal(1, result.Count);
        }

        [Fact]
        public async Task DeleteAccountDataTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString());
            var carRepository = new EfDeletableEntityRepository<Car>(new ApplicationDbContext(options.Options));
            var viewsRepository = new EfDeletableEntityRepository<AdView>(new ApplicationDbContext(options.Options));
            var commentRepository = new EfDeletableEntityRepository<Comment>(new ApplicationDbContext(options.Options));
            var searchModelRepository = new EfDeletableEntityRepository<SearchModel>(new ApplicationDbContext(options.Options));
            var favoriteRepository = new EfDeletableEntityRepository<UserCarFavorite>(new ApplicationDbContext(options.Options));
            var userManager = new EfDeletableEntityRepository<ApplicationUser>(new ApplicationDbContext(options.Options));
            var adService = new Mock<IAdService>();

            var service = new MyAccountService(carRepository, adService.Object, viewsRepository, commentRepository, searchModelRepository, favoriteRepository);
            var user = new ApplicationUser { FirstName = "test" };
            await userManager.AddAsync(user);
            await userManager.SaveChangesAsync();
            var car = new Car
            {
                Cc = 1,
                Color = 0,
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
                YearOfProduction = DateTime.Parse("01.01.1999"),
                UserId = user.Id,
            };

            await carRepository.AddAsync(car);
            await carRepository.SaveChangesAsync();
            await viewsRepository.AddAsync(new AdView { CarId = car.Id, User = user.Id });
            await viewsRepository.SaveChangesAsync();
            await commentRepository.AddAsync(new Comment { CarId = car.Id, Title = "test", Content = "test",UserId = user.Id });
            await commentRepository.SaveChangesAsync();
            await searchModelRepository.AddAsync(new SearchModel { UserId = user.Id });
            await searchModelRepository.SaveChangesAsync();
            await favoriteRepository.AddAsync(new UserCarFavorite { CarId = car.Id, UserId = user.Id });
            await favoriteRepository.SaveChangesAsync();

            await service.DeleteAccountDataAsync(user.Id);

            Assert.Equal(0, await carRepository.AllWithDeleted().CountAsync());
            Assert.Equal(0, await viewsRepository.AllWithDeleted().CountAsync());
            Assert.Equal(0, await commentRepository.AllWithDeleted().CountAsync());
            Assert.Equal(0, await favoriteRepository.AllWithDeleted().CountAsync());
            Assert.Equal(0, await searchModelRepository.AllWithDeleted().CountAsync());
        }

        [Fact]
        public async Task AddToFavAsyncTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString());
            var carRepository = new EfDeletableEntityRepository<Car>(new ApplicationDbContext(options.Options));
            var viewsRepository = new EfDeletableEntityRepository<AdView>(new ApplicationDbContext(options.Options));
            var commentRepository = new EfDeletableEntityRepository<Comment>(new ApplicationDbContext(options.Options));
            var searchModelRepository = new EfDeletableEntityRepository<SearchModel>(new ApplicationDbContext(options.Options));
            var favoriteRepository = new EfDeletableEntityRepository<UserCarFavorite>(new ApplicationDbContext(options.Options));
            var adService = new Mock<IAdService>();

            var service = new MyAccountService(carRepository, adService.Object, viewsRepository, commentRepository, searchModelRepository, favoriteRepository);

            var car = new Car();
            await carRepository.AddAsync(car);
            await carRepository.SaveChangesAsync();
            var userId = "UserId";
            var carId = car.Id;
            await service.AddAdToFavAsync(carId, userId);

            var userCarFavRecord = await favoriteRepository.All().FirstAsync();

            Assert.NotNull(userCarFavRecord);
            Assert.Equal(carId, userCarFavRecord.CarId);
            Assert.Equal(userId, userCarFavRecord.UserId);

        }

        [Fact]
        public async Task AddToFavWithNotFoundCarShouldReturnNullRefferenceException()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString());
            var carRepository = new EfDeletableEntityRepository<Car>(new ApplicationDbContext(options.Options));
            var viewsRepository = new EfDeletableEntityRepository<AdView>(new ApplicationDbContext(options.Options));
            var commentRepository = new EfDeletableEntityRepository<Comment>(new ApplicationDbContext(options.Options));
            var searchModelRepository = new EfDeletableEntityRepository<SearchModel>(new ApplicationDbContext(options.Options));
            var favoriteRepository = new EfDeletableEntityRepository<UserCarFavorite>(new ApplicationDbContext(options.Options));
            var userManager = new EfDeletableEntityRepository<ApplicationUser>(new ApplicationDbContext(options.Options));
            var adService = new Mock<IAdService>();

            var service = new MyAccountService(carRepository, adService.Object, viewsRepository, commentRepository, searchModelRepository, favoriteRepository);

            var userId = "UserId";
            var carId = "notFoundId";
            await Assert.ThrowsAsync<NullReferenceException>(async () => await service.AddAdToFavAsync(carId, userId));
        }

        [Fact]
        public async Task RemoveFavAdAsync()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString());
            var carRepository = new EfDeletableEntityRepository<Car>(new ApplicationDbContext(options.Options));
            var viewsRepository = new EfDeletableEntityRepository<AdView>(new ApplicationDbContext(options.Options));
            var commentRepository = new EfDeletableEntityRepository<Comment>(new ApplicationDbContext(options.Options));
            var searchModelRepository = new EfDeletableEntityRepository<SearchModel>(new ApplicationDbContext(options.Options));
            var favoriteRepository = new EfDeletableEntityRepository<UserCarFavorite>(new ApplicationDbContext(options.Options));
            var userManager = new EfDeletableEntityRepository<ApplicationUser>(new ApplicationDbContext(options.Options));
            var adService = new Mock<IAdService>();

            var service = new MyAccountService(carRepository, adService.Object, viewsRepository, commentRepository, searchModelRepository, favoriteRepository);

            var car = new Car();
            await carRepository.AddAsync(car);
            await carRepository.SaveChangesAsync();

            var userId = "UserId";
            var carId = car.Id;

            await service.AddAdToFavAsync(carId, userId);
            await service.RemoveFavAdAsync(carId, userId);

            var userCarFavRecord = await favoriteRepository.All().FirstOrDefaultAsync(x => x.UserId == userId);

            Assert.Null(userCarFavRecord);
        }

        [Fact]
        public async Task GetAllFavAdsOnCurrentUserTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString());
            var carRepository = new EfDeletableEntityRepository<Car>(new ApplicationDbContext(options.Options));
            var viewsRepository = new EfDeletableEntityRepository<AdView>(new ApplicationDbContext(options.Options));
            var commentRepository = new EfDeletableEntityRepository<Comment>(new ApplicationDbContext(options.Options));
            var searchModelRepository = new EfDeletableEntityRepository<SearchModel>(new ApplicationDbContext(options.Options));
            var favoriteRepository = new EfDeletableEntityRepository<UserCarFavorite>(new ApplicationDbContext(options.Options));
            var userManager = new EfDeletableEntityRepository<ApplicationUser>(new ApplicationDbContext(options.Options));
            var adService = new Mock<IAdService>();

            var service = new MyAccountService(carRepository, adService.Object, viewsRepository, commentRepository, searchModelRepository, favoriteRepository);

            await SeedData(carRepository);

            var car1 = await carRepository.All().FirstOrDefaultAsync(x => x.MoreInformation == "firstCar");
            var car2 = await carRepository.All().FirstOrDefaultAsync(x => x.MoreInformation == "secondCar");

            await service.AddAdToFavAsync(car1.Id, "1");
            await service.AddAdToFavAsync(car2.Id, "1");
            AutoMapperConfig.RegisterMappings(typeof(UserAdViewModel).Assembly);

            var result = await service.GetAllFavAdsOnCurrentUserAsync<UserAdViewModel>("1");

            Assert.Equal(2, result.Count);
        }

        private static async Task SeedData(EfDeletableEntityRepository<Car> carRepository)
        {
            var firstCar = new Car
            {
                Cc = 1,
                Color = 0,
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
                MoreInformation = "firstCar",
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
                Gearbox = Gearbox.Manual,
                Horsepowers = 1,
                ImgsPaths = GlobalConstants.DefaultImgCar,
                Km = 100,
                Location = Location.Sofia,
                Make = Make.Bmw,
                Model = "test",
                Modification = "test",
                MoreInformation = "secondCar",
                Price = 100,
                Type = Types.Convertible,
                TypeOfVeichle = TypeOfVeichle.Car,
                YearOfProduction = DateTime.Parse("01.01.1999"),
                UserId = "2",
            };

            await carRepository.AddAsync(firstCar);
            await carRepository.AddAsync(secondCar);
            await carRepository.SaveChangesAsync();
        }
    }
}
