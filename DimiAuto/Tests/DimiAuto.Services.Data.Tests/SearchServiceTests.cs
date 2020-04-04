using DimiAuto.Data;
using DimiAuto.Data.Models;
using DimiAuto.Data.Models.CarModel;
using DimiAuto.Data.Repositories;
using DimiAuto.Web.ViewModels.Home;
using Microsoft.EntityFrameworkCore;
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
            var service = new SearchService(searchRepository);

            var searchModel = new SearchInputModel
            {
                Condition = Condition.New,
                GearBox = GearBox.Automatic,
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
            Assert.Equal(searchModel.GearBox, savedSearchModel.GearBox);
        }

        [Fact]
        public async Task DeleteSearchModelTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var searchRepository = new EfDeletableEntityRepository<SearchModel>(new ApplicationDbContext(options.Options));
            var service = new SearchService(searchRepository);

            var searchModel = new SearchInputModel
            {
                Condition = Condition.New,
                GearBox = GearBox.Automatic,
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
        public async Task GetSearchModelByIdTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var searchRepository = new EfDeletableEntityRepository<SearchModel>(new ApplicationDbContext(options.Options));
            var service = new SearchService(searchRepository);

            var searchModel = new SearchInputModel
            {
                Condition = Condition.New,
                GearBox = GearBox.Automatic,
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
            Assert.Equal(searchModel.GearBox, result.GearBox);
            Assert.Equal(searchModel.Fuel, result.Fuel);

            var wrongIdResult = await service.GetSearchModelByIdAsync("test");
            Assert.Null(wrongIdResult);
        }
    }
}
