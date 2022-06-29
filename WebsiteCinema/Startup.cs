 using Microsoft.AspNetCore.Builder;
using WebsiteCinema.Data;
using Microsoft.EntityFrameworkCore;
using WebsiteCinema.Data.Services;
using WebsiteCinema.Data.Base;
using WebsiteCinema.Models;
using WebsiteCinema.Data.Cart;
using WebsiteCinema.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace WebsiteCinema
{
    public class Startup
    {
        IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(_configuration.GetConnectionString("Default"));
            });

            services.AddMvc();
            services.AddControllers();
            services.AddControllersWithViews();

            
            services.AddScoped<IEntityBaseRepository<Actor>, ActorsService>();
            services.AddScoped<IEntityBaseRepository<Producer>, ProducerService>();
            services.AddScoped<IMoviesService, MoviesService>();
            services.AddScoped<IEntityBaseRepository<Cinema>, CinemasService>();
            services.AddScoped<IOrdersService, OrdersService>();
            services.AddSingleton<ICharacherGeneratorApi, CharacherGeneratorApi>();

            services.AddSession();
            services.AddMemoryCache();

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>();
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped(sc => ShoppingCart.GetShoppingCart(sc));

            
        }

        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            //app.UseStatusCodePagesWithReExecute("/Errors/{0}");
            app.UseStaticFiles();

            app.UseRouting();
            app.UseSession();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute(name: "default", pattern: "{controller=Movies}/{action=Index}");
            });

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });

            ApplicatonDbInitializer.Seed(app);
            ApplicatonDbInitializer.SeedUsersAndRoles(app).Wait();
        }
    }
}
