namespace DimiAuto.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using DimiAuto.Data.Models;
    using DimiAuto.Data.Models.CarModel;
    using DimiAuto.Models.CarModel;
    using Microsoft.AspNetCore.Identity;

   public class AddAdsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Cars.Any())
            {
                return;
            }

            var user = dbContext.Users.FirstOrDefault();
            var yearOfProduction = DateTime.ParseExact("01.2000", "MM.yyyy", CultureInfo.InvariantCulture);
            for (int i = 0; i < 12; i++)
            {

                var car = new Car
                {
                    Cc = 1000 + i,
                    Color = Color.Black,
                    Condition = Condition.New,
                    Door = Doors.Five,
                    EuroStandart = EuroStandart.Euro4,
                    Fuel = Fuel.Diesel,
                    Gearbox = Gearbox.Automatic,
                    Horsepowers = 100 + i,
                    IsApproved = true,
                    Km = 10000 + (i * i),
                    Location = Location.Sofia,
                    Make = Make.Bmw,
                    Model = $"{i}",
                    Modification = "test" + i,
                    MoreInformation = "seeded ads" + i,
                    Price = i * i * i,
                    Type = Types.Sedan,
                    TypeOfVeichle = TypeOfVeichle.Car,
                    YearOfProduction = yearOfProduction.AddYears(i),
                    UserId = user.Id,
                    User = user,
                    
                };
                await dbContext.Cars.AddAsync(car);
            }
        }
    }
}
