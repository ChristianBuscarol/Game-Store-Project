using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Videogames_Store.Data;

namespace Videogames_Store
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<IdentityUser>(options => {
                options.SignIn.RequireConfirmedAccount = false;
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddControllersWithViews();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdministratorRole", policy => policy.RequireRole("Admin"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //string adminEmail = "adminExample@gmail.com";
            //string adminPass = "Admin-123";

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            //using (var scope = app.ApplicationServices.CreateScope())
            //{
            //    var rolManagment = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            //    var rollingRol = new[] { "Admin", "User" };

            //    foreach (var rol in rollingRol)
            //    {
            //        if (!await rolManagment.RoleExistsAsync(rol))
            //        {
            //            await rolManagment.CreateAsync(new IdentityRole(rol));
            //        }
            //    }
            //}

            //using (var scope = app.ApplicationServices.CreateScope())
            //{
            //    var userRol = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

            //    if (await userRol.FindByEmailAsync(adminEmail) == null)
            //    {
            //        var administrator = new IdentityUser();

            //        administrator.UserName = "Administrador";
            //        administrator.Email = adminEmail;

            //        await userRol.CreateAsync(administrator, adminPass);
            //        await userRol.AddToRoleAsync(administrator, "Admin");
            //    }
            //}

            //using (var scope = app.ApplicationServices.CreateScope())
            //{
            //    var userRol = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

            //    string userEmail = "userExample@gmail.com";
            //    string userPass = "User-321";

            //    if (await userRol.FindByEmailAsync(userEmail) == null)
            //    {
            //        var usuary = new IdentityUser();

            //        usuary.UserName = "Usuario 'X'";
            //        usuary.Email = userEmail;

            //        await userRol.CreateAsync(usuary, userPass);
            //        await userRol.AddToRoleAsync(usuary, "User");
            //    }
            //}

            //using (var scope = app.ApplicationServices.CreateScope())
            //{
            //    var userConnection = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
            //    //var searchResult = await userConnection.FindByEmailAsync(adminEmail);

            //    if(await userConnection.FindByEmailAsync(adminEmail) != null)
            //    {
            //        var appUser = new IdentityUser();
            //        var searchResult = await userConnection.FindByEmailAsync(adminEmail);

            //        if(string.Equals(searchResult, adminEmail))
            //        {
            //            appUser.UserName = "Administrador";
            //            appUser.Email = adminEmail;
            //            await userConnection.AddToRoleAsync(appUser, "Admin");
            //        }
            //    }
            //}
        }
    }
}
