using DimiAuto.Data;
using DimiAuto.Data.Models;
using DimiAuto.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DimiAuto.Services.Data.Tests
{
    public class ViewServiceTests
    {
        [Fact]
        public async Task CreateViewTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var viewAdRepository = new EfDeletableEntityRepository<AdView>(new ApplicationDbContext(options.Options));
            var viewService = new ViewService(viewAdRepository);
            
            await viewService.AddViewAsync("1", "Test");
            await viewService.AddViewAsync("2", "Test");

            var result = await viewAdRepository.All().CountAsync();
            Assert.Equal(2, result);
        }

        [Fact]
        public async Task GetViewsCountTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var viewAdRepository = new EfDeletableEntityRepository<AdView>(new ApplicationDbContext(options.Options));
            var viewService = new ViewService(viewAdRepository);

            await viewService.AddViewAsync("1", "firstCar");
            await viewService.AddViewAsync("2", "firstCar");
            await viewService.AddViewAsync("1", "secCar");

            var firstCarViews = await viewService.GetViewsCountAsync("firstCar");
            var secCarViews = await viewService.GetViewsCountAsync("secCar");
            Assert.Equal(2, firstCarViews);
            Assert.Equal(1, secCarViews);
        }
    }
}
