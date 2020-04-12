namespace DimiAuto.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using DimiAuto.Common;
    using DimiAuto.Data;
    using DimiAuto.Data.Models.CarModel;
    using DimiAuto.Data.Repositories;
    using DimiAuto.Models.CarModel;
    using DimiAuto.Web.ViewModels.Img;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Xunit;

    public class ImgServiceTests
    {



        [Fact]
        public async Task AddImgToAdTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var carRepository = new EfDeletableEntityRepository<Car>(new ApplicationDbContext(options.Options));
            var cloudinaryAccount = new CloudinaryDotNet.Account("name", "apiKey", "apiSecret");
            var cloudinary = new Cloudinary(cloudinaryAccount);
            var service = new ImgService(cloudinary, carRepository);

            var newCar = new Car
            {
                Cc = 1,
                Color = 0,
                Condition = DimiAuto.Data.Models.CarModel.Condition.New,
                Door = Doors.Three,
                EuroStandart = EuroStandart.Euro1,
                Extras = "4x4",
                Fuel = Fuel.Diesel,
                Gearbox = GearBox.Automatic,
                Horsepowers = 1,
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
                YearOfProduction = DateTime.ParseExact("01.1999", "mm.yyyy", CultureInfo.InvariantCulture),
                UserId = "1",
            };
            var imgUrl = "someImgsUrls";
            await carRepository.AddAsync(newCar);
            await carRepository.SaveChangesAsync();
            var car = await carRepository.All().FirstAsync();
            await service.AddImgToCurrentAdAsync(imgUrl, car.Id);

            Assert.Equal(imgUrl, car.ImgsPaths);
        }

        
    }
}
