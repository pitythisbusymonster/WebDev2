using System.Runtime.InteropServices;
using AmberTurnerSite.Models;
using AmberTurnerSite.Repos;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;



namespace AmberTurnerSite
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
            // This service allows me to edit .cshtml views and see the result without restarting
            // This service requires the Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation NuGet package
            //services.AddRazorPages().AddRazorRuntimeCompilation();

            services.AddControllersWithViews();

            //inject our repos into controllers
            services.AddTransient<IPosts, ForumRepository>();

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                // Assuming that SQL Server is installed on Windows
                services.AddDbContext<ForumContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:SQLServerConnection"]));
            }
            else
            {
                // Assuming SQLite is installed on all other operating systems
                services.AddDbContext<ForumContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:SQLServerConnection"]));
            }

            //added for Identity
            services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<ForumContext>()
                .AddDefaultTokenProviders();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ForumContext context)
        {
            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
                context.Response.Headers.Add("X-Frame-Options", "SAMEORIGIN");
                context.Response.Headers.Add("Pragma", "no-cache");
                context.Response.Headers.Add("Cache-Control", "no-cache");
                //context.Response.Headers.Add("Cache-Control", "no-store");
                //context.Response.Headers.Add("Cache-Control", "must-revalidate");
                await next();
            });

            app.UseCookiePolicy(new CookiePolicyOptions { HttpOnly = HttpOnlyPolicy.Always, Secure = CookieSecurePolicy.Always });


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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

            app.UseAuthentication();    //added 1.12.21

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            

            var serviceProvider = app.ApplicationServices;
            var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            SeedData.Seed(context, userManager, roleManager);
        }
    }
}
