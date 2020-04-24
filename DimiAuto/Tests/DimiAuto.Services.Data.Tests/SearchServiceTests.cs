using DimiAuto.Data;
using DimiAuto.Data.Models;
using DimiAuto.Data.Models.CarModel;
using DimiAuto.Data.Repositories;
using DimiAuto.Services.Mapping;
using DimiAuto.Web.ViewModels.Home;
using DimiAuto.Web.ViewModels.SearchHistory;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DimiAuto.Services.Data.Tests
{
    public class SearchServiceTests
    {
        [Fact]
        public async Task SaveSearchModelIfNotExistTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var searchRepository = new EfDeletableEntityRepository<SearchModel>(new ApplicationDbContext(options.Options));
            var adService = new Mock<IAdService>();
            var service = new SearchService(searchRepository, adService.Object);

            var searchModel = new SearchInputModel
            {
                Condition = Condition.New,
                Gearbox = Gearbox.Automatic,
                TypeOfVeichle = TypeOfVeichle.Car,
                Model = "test",
                Make = Make.All,
                Fuel = Fuel.Diesel,
            };

            await service.SaveSearchModelAsync("1", searchModel);
            await service.SaveSearchModelAsync("1", searchModel);
            await service.SaveSearchModelAsync("1", searchModel);

            var dbCounts = await searchRepository.All().CountAsync();
            var savedSearchModel = await searchRepository.All().FirstAsync();
            Assert.Equal(1, dbCounts);
            Assert.Equal(searchModel.Make, savedSearchModel.Make);
            Assert.Equal(searchModel.Model, savedSearchModel.Model);
            Assert.Equal(searchModel.Gearbox, savedSearchModel.Gearbox);
        }

        [Fact]
        public async Task DeleteSearchModelTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var searchRepository = new EfDeletableEntityRepository<SearchModel>(new ApplicationDbContext(options.Options));
            var adService = new Mock<IAdService>();

            var service = new SearchService(searchRepository, adService.Object);

            var searchModel = new SearchInputModel
            {
                Condition = Condition.New,
                Gearbox = Gearbox.Automatic,
                TypeOfVeichle = TypeOfVeichle.Car,
                Model = "test",
                Make = Make.All,
                Fuel = Fuel.Diesel,
            };

            await service.SaveSearchModelAsync("1", searchModel);
            var searchModelFromDb = await searchRepository.All().FirstAsync();

            await service.DeleteSearchModelByIdAsync(searchModelFromDb.Id);
            var dbRecords = await searchRepository.All().CountAsync();
            Assert.Equal(0, dbRecords);
        }

        [Fact]
        public void DeleteNotFindSearchServiceShouldReturnNullReferenceExceptionTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var searchRepository = new EfDeletableEntityRepository<SearchModel>(new ApplicationDbContext(options.Options));
            var adService = new Mock<IAdService>();
            var service = new SearchService(searchRepository, adService.Object);

            Assert.ThrowsAsync<NullReferenceException>(async () => await service.DeleteSearchModelByIdAsync("NotFindModelId"));
        }

        [Fact]
        public async Task GetSearchmodelByWrongIdShouldReturnNullReferenceException()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var searchRepository = new EfDeletableEntityRepository<SearchModel>(new ApplicationDbContext(options.Options));
            var adService = new Mock<IAdService>();

            var service = new SearchService(searchRepository, adService.Object);


            await Assert.ThrowsAsync<NullReferenceException>(async () => await service.GetSearchModelByIdAsync("wrongId"));
        }

        [Fact]
        public async Task GetSearchModelByIdTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var searchRepository = new EfDeletableEntityRepository<SearchModel>(new ApplicationDbContext(options.Options));
            var adService = new Mock<IAdService>();

            var service = new SearchService(searchRepository, adService.Object);

            var searchModel = new SearchInputModel
            {
                Condition = Condition.New,
                Gearbox = Gearbox.Automatic,
                TypeOfVeichle = TypeOfVeichle.Car,
                Model = "test",
                Make = Make.All,
                Fuel = Fuel.Diesel,
            };

            await service.SaveSearchModelAsync("1", searchModel);
            var searchModelFromDb = await searchRepository.All().FirstAsync();
            var result = await service.GetSearchModelByIdAsync(searchModelFromDb.Id);
            Assert.Equal(searchModel.Make, result.Make);
            Assert.Equal(searchModel.Model, result.Model);
            Assert.Equal(searchModel.Gearbox, result.Gearbox);
            Assert.Equal(searchModel.Fuel, result.Fuel);
        }

        [Fact]
        public async Task GetSearchModelsByUserIdTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var searchRepository = new EfDeletableEntityRepository<SearchModel>(new ApplicationDbContext(options.Options));
            var adService = new Mock<IAdService>();

            var service = new SearchService(searchRepository, adService.Object);

            var searchModel = new SearchInputModel
            {
                Condition = Condition.New,
                Gearbox = Gearbox.Automatic,
                TypeOfVeichle = TypeOfVeichle.Car,
                Model = "test",
                Make = Make.All,
                Fuel = Fuel.Diesel,
            };
            var secondSearchModel = new SearchInputModel
            {
                Condition = Condition.New,
                Gearbox = Gearbox.Automatic,
                TypeOfVeichle = TypeOfVeichle.Car,
                Model = "test2",
                Make = Make.Audi,
                Fuel = Fuel.Electricity,
            };
            await service.SaveSearchModelAsync("1", searchModel);
            await service.SaveSearchModelAsync("1", secondSearchModel);

            AutoMapperConfig.RegisterMappings(typeof(SearchViewModel).Assembly);
            var result = await service.GetSearchModelsAsync("1");

            Assert.Equal(2, result.Count);
        }
    }
}
