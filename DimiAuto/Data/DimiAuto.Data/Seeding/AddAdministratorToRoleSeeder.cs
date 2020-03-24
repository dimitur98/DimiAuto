

namespace DimiAuto.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using DimiAuto.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    public class AddAdministratorToRoleSeeder : ISeeder
    {

        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetService<RoleManager<ApplicationRole>>();

            var user = await userManager.FindByNameAsync("AdminUser");
            var role = await roleManager.FindByNameAsync("Administrator");
            var exist = dbContext.UserRoles.Any(x => x.UserId == user.Id && x.RoleId == role.Id);
            if (exist)
            {
                return;
            }
             
            await dbContext.UserRoles.AddAsync(new IdentityUserRole<string>
            {
                RoleId = role.Id,
                UserId = user.Id,
            });


        }
    }
}
