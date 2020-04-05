namespace DimiAuto.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using DimiAuto.Data;
    using DimiAuto.Data.Models;
    using DimiAuto.Data.Repositories;
    using DimiAuto.Models.CarModel;
    using DimiAuto.Services.Mapping;
    using DimiAuto.Web.ViewModels.Ad;
    using DimiAuto.Web.ViewModels.Ad.Comment;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class CommentServiceTests
    {
        [Fact]
        public async Task CreateCommentTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var carRepository = new EfDeletableEntityRepository<Car>(new ApplicationDbContext(options.Options));
            var commentRepository = new EfDeletableEntityRepository<Comment>(new ApplicationDbContext(options.Options));
            var commentService = new CommentService(commentRepository, carRepository);
            var userId = "userId";
            var carId = "carId";
            var comment = new CarCommentsInputModel
            {
                UserId = userId,
                CarId = carId,
                Content = "test comment",
                Title = "test",
            };
            await commentService.CreateAsync(comment);
            var createdComment = await commentRepository.All().FirstOrDefaultAsync(x => x.UserId == userId && x.CarId == carId && x.Title == "test");
            Assert.NotNull(createdComment);
        }

         [Fact]
        public async Task GetComments()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var carRepository = new EfDeletableEntityRepository<Car>(new ApplicationDbContext(options.Options));
            var commentRepository = new EfDeletableEntityRepository<Comment>(new ApplicationDbContext(options.Options));
            var commentService = new CommentService(commentRepository, carRepository);
            var userId = "userId";
            var car = new Car
            {
                ImgsPaths = "asd",
                
            };
            await carRepository.AddAsync(car);
            await carRepository.SaveChangesAsync();
            var addedCar = await carRepository.All().FirstAsync();
            var carId = addedCar.Id;
            var comment = new CarCommentsInputModel
            {
                UserId = userId,
                CarId = carId,
                Content = "test comment",
                Title = "test",
            };
            var secComment = new CarCommentsInputModel
            {
                UserId = userId,
                CarId = carId,
                Content = "Second comment",
                Title = "SecCom",
            };
            await commentService.CreateAsync(comment);
            await commentService.CreateAsync(secComment);
            AutoMapperConfig.RegisterMappings(typeof(CarCommentViewModel).Assembly);
            var addedComment = await commentService.GetComments<CarCommentViewModel>(carId);
            Assert.Equal(2, addedComment.Count);
        }

    }
}
