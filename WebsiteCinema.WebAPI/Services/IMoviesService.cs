using WebsiteCinema.WebAPI.Base;
using WebsiteCinema.WebAPI.ViewModels;
using WebsiteCinema.WebAPI.Models;

namespace WebsiteCinema.WebAPI.Services
{
    public interface IMoviesService : IEntityBaseRepository<Movie>
    {
        Task<Movie> GetMovieByIdAsync(int id);
        Task<VMNewMovieDropdowns> GetNewMovieDropdownsValues();
        Task AddNewMovieAsync(VMNewMovie data);
        Task UpdateMovieAsync(VMNewMovie data);
    }
}
