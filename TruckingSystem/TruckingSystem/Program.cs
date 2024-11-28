using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TruckingSystem.Data;
using TruckingSystem.Infrastructure.Repositories;
using TruckingSystem.Infrastructure.Repositories.Contracts;
using TruckingSystem.Services.Data;
using TruckingSystem.Services.Data.Contracts;

namespace TruckingSystem.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            string? connectionString = builder.Configuration
                .GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            builder.Services
                .AddDbContext<TruckingSystemDbContext>(options =>
                {
                    options.UseSqlServer(connectionString);
                });

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            })
                .AddEntityFrameworkStores<TruckingSystemDbContext>(); 

            builder.Services.ConfigureApplicationCookie(cfg =>
            {
                cfg.LoginPath = "/Identity/Account/Login";
            });

            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            builder.Services.AddScoped<IDriverRepository, DriverRepository>();
            builder.Services.AddScoped<IDriverService, DriverService>();

            builder.Services.AddScoped<IDriverManagerRepository, DriverManagerRepository>();
            builder.Services.AddScoped<ITruckRepository, TruckRepository>();

            builder.Services.AddScoped<ITrailerRepository, TrailerRepository>();
            builder.Services.AddScoped<ITrailerService, TrailerService>();

            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapRazorPages();

            app.Run();
        }
    }
}
