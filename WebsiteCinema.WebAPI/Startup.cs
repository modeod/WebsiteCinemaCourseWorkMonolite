using Microsoft.EntityFrameworkCore;
using WebsiteCinema.WebAPI.Base;
using WebsiteCinema.WebAPI.Data.Cart;
using WebsiteCinema.WebAPI.Models;
using WebsiteCinema.WebAPI.Services;

namespace WebsiteCinema.WebAPI
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

            services.AddScoped<IEntityBaseRepository<Actor>, ActorsService>();
            services.AddScoped<IEntityBaseRepository<Producer>, ProducerService>();
            services.AddScoped<IMoviesService, MoviesService>();
            services.AddScoped<IEntityBaseRepository<Cinema>, CinemasService>();
            services.AddScoped<IOrdersService, OrdersService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ShoppingCart>();

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRouting();
            app.UseHttpsRedirection();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //app.Run();
        }
    }
}
