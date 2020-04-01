namespace DimiAuto.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using DimiAuto.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    public class AdministratorUserSeeder : ISeeder
    {


        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {


            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();
            var user = await userManager.FindByNameAsync("AdminUser@admin.bg");
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
                }, "123456");
        }
    }
}
