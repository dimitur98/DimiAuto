namespace DimiAuto.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using DimiAuto.Common;
    using DimiAuto.Data.Common.Repositories;
    using DimiAuto.Data.Models;
    using DimiAuto.Data.Repositories;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public class AdministratorUserSeeder : ISeeder
    {


        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {

            var userRepository = serviceProvider.GetService<IDeletableEntityRepository<ApplicationUser>>();
            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();
            var user = await userRepository.AllWithDeleted().FirstOrDefaultAsync(x => x.Email == GlobalConstants.AdminUser);
            if (user != null)
            {
                return;
            }

            await userManager.CreateAsync(
                new ApplicationUser
                {
                    UserName = "AdminUser@admin.bg",
                    Email = "AdminUser@admin.bg",
                    Adress = "Lulin 10",
                    City = "Sofia",
                    FirstName = "Admin",
                    LastName = "Adminchev",
                    EmailConfirmed = true,
                    PhoneNumber = "0888888888",
                }, "123456");
        }
    }
}
