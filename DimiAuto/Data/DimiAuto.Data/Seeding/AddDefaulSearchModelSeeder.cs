using DimiAuto.Common;
using DimiAuto.Data.Common.Repositories;
using DimiAuto.Data.Models;
using DimiAuto.Data.Models.CarModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DimiAuto.Data.Seeding
{
    public class AddDefaulSearchModelSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.SearchModels.Any())
            {
                return;
            }

            await dbContext.SearchModels.AddAsync(new SearchModel 
            {
                Condition = Condition.All,
                Fuel = Fuel.All,
                TypeOfVeichle = TypeOfVeichle.All,
                Location = Location.All,
                Gearbox = Gearbox.All,
                Make = Make.All,
                Model = null,
                YearTo = null,
                YearFrom = null,
                PriceFrom = null,
                PriceTo = null,
                UserId = GlobalConstants.DefaultSearchModelUserId,
            });
        }
    }
}
