﻿namespace DimiAuto.Web
{
    using System;
    using System.Reflection;

    using CloudinaryDotNet;
    using DimiAuto.Data;
    using DimiAuto.Data.Common;
    using DimiAuto.Data.Common.Repositories;
    using DimiAuto.Data.Models;
    using DimiAuto.Data.Repositories;
    using DimiAuto.Data.Seeding;
    using DimiAuto.Services.Data;
    using DimiAuto.Services.Data.AreaServices;
    using DimiAuto.Services.Mapping;
    using DimiAuto.Services.Messaging;
    using DimiAuto.Web.ViewModels;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Caching.SqlServer;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(this.configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<ApplicationUser>(IdentityOptionsProvider.GetIdentityOptions)
                .AddRoles<ApplicationRole>().AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddAuthentication().AddFacebook(option =>
            {
                option.AppId = this.configuration["Facebook:AppKey"];
                option.AppSecret = this.configuration["Facebook:AppSecret"];
                }
            );

            services.Configure<CookiePolicyOptions>(
                options =>
                    {
                        options.CheckConsentNeeded = context => true;
                        options.MinimumSameSitePolicy = SameSiteMode.None;
                    });

            services.AddControllersWithViews(configure =>
                    {
                        configure.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                    });
            services.AddAntiforgery(options =>
            {
                options.HeaderName = "X-CSRF-TOKEN";
            });
            services.AddRazorPages();

            services.AddDistributedSqlServerCache(options =>
            {
                options.ConnectionString = this.configuration.GetConnectionString("DefaultConnection");
                options.SchemaName = "dbo";
                options.TableName = "CacheRecords";
            });
            services.AddSession(options =>
            {
                options.IdleTimeout = new TimeSpan(365, 0, 0, 0);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;

            });
            Account account = new Account(
                                 this.configuration["Cloudinary:AppName"],
                                 this.configuration["Cloudinary:AppKey"],
                                 this.configuration["Cloudinary:AppSecret"]);

            Cloudinary cloudinary = new Cloudinary(account);
            services.AddSingleton(cloudinary);

            services.AddSingleton(this.configuration);

            // Data repositories
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IDbQueryRunner, DbQueryRunner>();
            // Application services
            services.AddTransient<IEmailSender, SendGridEmailSender>(provider =>
                new SendGridEmailSender(this.configuration["SendGrid:AppKey"], this.configuration["SendGrid:Email"], this.configuration["SendGrid:Username"]));
            services.AddTransient<ISettingsService, SettingsService>();
            services.AddTransient<IAdService, AdService>();
            services.AddTransient<IHomeService, HomeService>();
            services.AddTransient<IImgService, ImgService>();
            services.AddTransient<ICommentService, CommentService>();
            services.AddTransient<IMyAccountService, MyAccountService>();
            services.AddTransient<IAdministrationService, AdministrationService>();
            services.AddTransient<ISearchService, SearchService>();
            services.AddTransient<IViewService, ViewService>();
            services.AddTransient<IFavoriteService, FavoriteService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            // Seed data on application startup
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                //if (env.IsDevelopment())
                //{
                    dbContext.Database.Migrate();
                //}

                new ApplicationDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
            }

            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
                //app.UseDatabaseErrorPage();
                // app.UseExceptionHandler("/Home/Error");
                app.UseExceptionHandler("/Home/Error");
                app.UseStatusCodePagesWithRedirects("/Home/Error?statusCode={0}");
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseStatusCodePagesWithRedirects("/Home/Error?statusCode={0}");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSession();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(
                endpoints =>
                    {
                        endpoints.MapControllerRoute("areaRoute", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapRazorPages();
                    });
        }
    }
}
