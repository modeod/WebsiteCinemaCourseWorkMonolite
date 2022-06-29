using Microsoft.EntityFrameworkCore;
using WebsiteCinema.WebAPI.Base;
using WebsiteCinema.WebAPI.ViewModels;
using WebsiteCinema.WebAPI.Models;

namespace WebsiteCinema.WebAPI.Services
{
    public class MoviesService : EntityBaseRepository<Movie>, IMoviesService
    {
        public MoviesService(AppDbContext context) : base(context) { }

        public async Task AddNewMovieAsync(VMNewMovie data)
        {
            var newMovie = new Movie
            {
                Name = data.Name,
                Description = data.Description,
                Price = data.Price,
                ImageURL = data.ImageURL,
                CinemaId = data.CinemaId,
                StartDate = data.StartDate,
                EndDate = data.EndDate,
                MovieCategory = data.MovieCategory,
                ProducerId = data.ProducerId
            };

            await _context.Movies.AddAsync(newMovie);
            await _context.SaveChangesAsync();

            foreach (var actiorId in data.ActorIds)
            {
                var newActorMovie = new Actor_Movie()
                {
                    MovieId = newMovie.Id,
                    ActorId = actiorId
                };
                await _context.Actor_Movie.AddAsync(newActorMovie);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            var movieDetails = await _context.Movies
                .Include(c => c.Cinema)
                .Include(p => p.Producer)
                .Include(am => am.Actor_Movies).ThenInclude(a => a.Actor)
                .FirstOrDefaultAsync(n => n.Id == id);

            return movieDetails;

        }

        public async Task<VMNewMovieDropdowns> GetNewMovieDropdownsValues()
        {
            var response = new VMNewMovieDropdowns();
            response.Actors = await _context.Actors.OrderBy(n => n.FullName).ToListAsync();
            response.Cinemas = await _context.Cinemas.OrderBy(n => n.Name).ToListAsync();
            response.Producers = await _context.Producers.OrderBy(n => n.FullName).ToListAsync();

            return response;
        }

        public async Task UpdateMovieAsync(VMNewMovie data)
        {
            var dbMovie = await _context.Movies.FirstOrDefaultAsync(n => n.Id == data.Id);
            if(dbMovie == null) { dbMovie = new Movie(); }

            dbMovie.Name = data.Name;
            dbMovie.Description = data.Description;
            dbMovie.Price = data.Price;
            dbMovie.ImageURL = data.ImageURL;
            dbMovie.CinemaId = data.CinemaId;
            dbMovie.StartDate = data.StartDate;
            dbMovie.EndDate = data.EndDate;
            dbMovie.MovieCategory = data.MovieCategory;
            dbMovie.ProducerId = data.ProducerId;
            await _context.SaveChangesAsync();

            var extistingActorsToDelete = _context.Actor_Movie.Where(n => n.MovieId == data.Id).ToList();
            _context.Actor_Movie.RemoveRange(extistingActorsToDelete);
            await _context.SaveChangesAsync();

            foreach (var actiorId in data.ActorIds)
            {
                var newActorMovie = new Actor_Movie()
                {
                    MovieId = data.Id,
                    ActorId = actiorId
                };
                await _context.Actor_Movie.AddAsync(newActorMovie);
            }

            await _context.SaveChangesAsync();
        }
    }
}
