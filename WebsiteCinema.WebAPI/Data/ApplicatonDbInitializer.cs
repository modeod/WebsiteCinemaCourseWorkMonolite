using Microsoft.AspNetCore.Identity;
using WebsiteCinema.WebAPI.Data.Enums;
using WebsiteCinema.WebAPI.Data.Static;
using WebsiteCinema.WebAPI.Models;

namespace WebsiteCinema.WebAPI
{
    public class ApplicatonDbInitializer
    {
        public static void Seed(IApplicationBuilder builder)
        {
            using(var serviceScope = builder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
                //context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                if (!context.Cinemas.Any())
                {
                    context.Add(new Cinema()
                    {
                        Name = "Motyas Cinema",
                        Description = "LMAO",
                        Logo = ""
                    });
                    context.SaveChanges();


                }
                if (!context.Actors.Any())
                {
                    context.Add(new Actor()
                    {
                        FullName = "Maple",
                        Bio = "Top",
                        ProfilePicturURL = @"https://i.pinimg.com/564x/d6/d0/e0/d6d0e04572641a919eaf794072ee24b2.jpg"
                    });
                    context.Add(new Actor()
                    {
                        FullName = "Cinnamon",
                        Bio = "Top",
                        ProfilePicturURL = @"https://i.pinimg.com/564x/31/72/9e/31729eba5b81a83b212158dd7109f586.jpg"
                    });
                    context.SaveChanges();

                }
                if (!context.Producers.Any())
                {

                    context.Add(new Producer()
                    {
                        FullName = "Motya's Produser Stephan",
                        Bio = ". _ .",
                        ProfilePicturURL = ""
                    });
                    context.SaveChanges();
                }
                if (!context.Movies.Any())
                {
                    context.Add(new Movie()
                    {
                        Name = "Motya's Film",
                        Description = "LMAO x3",
                        Price = 300.00,
                        ImageURL = "",
                        StartDate = DateTimeOffset.Now,
                        EndDate = DateTimeOffset.Now.AddDays(10),
                        CinemaId = 1,
                        ProducerId = 1,
                        MovieCategory = MovieCategoryEnum.Action
                    });
                    context.SaveChanges();
                }
                if (!context.Actor_Movie.Any())
                {

                    context.Add(new Actor_Movie()
                    {
                        ActorId = 1,
                        MovieId = 1 
                    });
                    context.SaveChanges();

                }

            } 
        }

        public static async Task SeedUsersAndRoles(IApplicationBuilder builder)
        {
            using (var serviceScope = builder.ApplicationServices.CreateScope())
            {
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                if(!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));

                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                var adminUser = await userManager.FindByEmailAsync("admin@mysite.com");
                if(adminUser == null)
                {
                    var newAdminUser = new ApplicationUser()
                    {
                        FullName = "Admin user",
                        UserName = "admin",
                        Email = "admin@mysite.com",
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAdminUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                var user = await userManager.FindByEmailAsync("user@mysite.com");
                if (user == null)
                {
                    var newUser = new ApplicationUser()
                    {
                        FullName = "App User",
                        UserName = "user",
                        Email = "user@mysite.com",
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newUser, UserRoles.User);
                }
            }
        }

    }
}
