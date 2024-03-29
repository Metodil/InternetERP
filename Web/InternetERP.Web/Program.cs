﻿namespace InternetERP.Web
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using CloudinaryDotNet;
    using InternetERP.Data;
    using InternetERP.Data.Common;
    using InternetERP.Data.Common.Repositories;
    using InternetERP.Data.Models;
    using InternetERP.Data.Repositories;
    using InternetERP.Data.Seeding;
    using InternetERP.Services;
    using InternetERP.Services.Contracts;
    using InternetERP.Services.Data;
    using InternetERP.Services.Data.Administration;
    using InternetERP.Services.Data.Administration.Contracts;
    using InternetERP.Services.Data.ContactUs;
    using InternetERP.Services.Data.ContactUs.Contracts;
    using InternetERP.Services.Data.Contracts;
    using InternetERP.Services.Data.Employee;
    using InternetERP.Services.Data.Employee.Contracts;
    using InternetERP.Services.Data.Home;
    using InternetERP.Services.Data.Home.Contracts;
    using InternetERP.Services.Mapping;
    using InternetERP.Services.Messaging;
    using InternetERP.Web.Areas.Identity.Pages.Account;
    using InternetERP.Web.ErrorHandlingMiddleware;
    using InternetERP.Web.ErrorHandlingMiddleware.Exceptions;
    using InternetERP.Web.ViewModels;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using SendGrid;

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            ConfigureServices(builder.Services, builder.Configuration);
            var app = builder.Build();
            Configure(app);

            app.Run();
        }

        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<ApplicationUser>(IdentityOptionsProvider.GetIdentityOptions)
                .AddRoles<ApplicationRole>().AddEntityFrameworkStores<ApplicationDbContext>();

            services.Configure<CookiePolicyOptions>(
                options =>
                {
                    options.CheckConsentNeeded = context => true;
                    options.MinimumSameSitePolicy = SameSiteMode.None;
                });

            services.AddControllersWithViews(
                options =>
                {
                    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                }).AddRazorRuntimeCompilation();
            services.AddRazorPages();
            services.AddDatabaseDeveloperPageExceptionFilter();

            // Facebook login
            services.AddAuthentication().AddFacebook(facebookOptions =>
            {
                facebookOptions.AppId = configuration["Authentication:Facebook:AppId"];
                facebookOptions.AppSecret = configuration["Authentication:Facebook:AppSecret"];
            });

            // Google login
            services.AddAuthentication()
                .AddGoogle(options =>
                {
                    options.ClientId = configuration["Authentication:Google:client_id"];
                    options.ClientSecret = configuration["Authentication:Google:client_secret"];
                });

            services.AddSingleton(configuration);

            // Add clodinary
            var cloudinary = new Cloudinary(new Account()
            {
                Cloud = configuration["Cloudinary:CloudName"],
                ApiKey = configuration["Cloudinary:ApiKey"],
                ApiSecret = configuration["Cloudinary:ApiSecret"],
            });
            services.AddSingleton(cloudinary);

            // Data repositories
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IDbQueryRunner, DbQueryRunner>();

            // Application services
 //           services.AddTransient<IEmailSender, NullMessageSender>();
            services.AddTransient<IEmailSender>(
                serviceProvider => new SendGridEmailSender(configuration["SendGrid:ApiKey"]));
            services.AddTransient<ISettingsService, SettingsService>();
            services.AddTransient<ITownsService, TownsService>();
            services.AddTransient<ICustomUsersService, CustomUsersService>();
            services.AddTransient<IProfileService, ProfileService>();
            services.AddTransient<ICloudinaryService, CloudinaryService>();
            services.AddTransient<IRolesService, RolesService>();
            services.AddTransient<IProductsService, ProductsService>();
            services.AddTransient<IFileService, FileLocalService>();
            services.AddTransient<IFailureTeamsService, FailureTeamsService>();
            services.AddTransient<ISaleGoodsService, SaleGoodsService>();
            services.AddTransient<IFailuresService, FailuresService>();
            services.AddTransient<ITechniciansService, TechniciansService>();
            services.AddTransient<IPaypalService, PaypalService>();
            services.AddTransient<IBillService, BillService>();
            services.AddTransient<IInvoiceService, InvoiceService>();
            services.AddTransient<IContactUsService, ContactUsService>();
            services.AddTransient<IMailKitSender, MailKitSender>();
            services.AddTransient<IHomeService, HomeService>();

            // Add HttpContextAccessor to access cookies
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Add extra cliams when login
            services.AddTransient<IClaimsTransformation, AddExtraClaims>();
        }

        private static void Configure(WebApplication app)
        {
            // Seed data on application startup
            using (var serviceScope = app.Services.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.Migrate();
                new ApplicationDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
            }

            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseStatusCodePagesWithReExecute("/error/{0}");

                // add custom globabal Error Handling Middleware
                app.UseMiddleware(typeof(GlobalErrorHandlingMiddleware));

                // app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();


            app.UseRouting();

            if (app.Environment.IsDevelopment())
            {
                app.MapGet("/debug/routes", (IEnumerable<EndpointDataSource> endpointSources) =>
                    string.Join("\n", endpointSources.SelectMany(source => source.Endpoints)));
            }

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute("areaRoute", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
            app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();
        }
    }
}
