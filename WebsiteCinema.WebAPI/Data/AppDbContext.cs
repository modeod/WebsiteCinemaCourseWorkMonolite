using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebsiteCinema.WebAPI.Models;

namespace WebsiteCinema.WebAPI
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Actor_Movie> Actor_Movie { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Producer> Producers { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Actor_Movie>().HasKey(am => new { am.MovieId, am.ActorId });

            modelBuilder.Entity<Actor_Movie>()
                .HasOne(x => x.Movie)
                .WithMany(x => x.Actor_Movies)
                .HasForeignKey(x => x.MovieId);

            modelBuilder.Entity<Actor_Movie>()
                .HasOne(x => x.Actor)
                .WithMany(x => x.Actor_Movies)
                .HasForeignKey(x => x.ActorId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
