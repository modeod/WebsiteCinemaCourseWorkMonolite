using WebsiteCinema.Data.Base;
using WebsiteCinema.Data.ViewModels;
using WebsiteCinema.Models;

namespace WebsiteCinema.Data.Services
{
    public interface IMoviesService : IEntityBaseRepository<Movie>
    {
        Task<Movie> GetMovieByIdAsync(int id);
        Task<VMNewMovieDropdowns> GetNewMovieDropdownsValues();
        Task AddNewMovieAsync(VMNewMovie data);
        Task UpdateMovieAsync(VMNewMovie data);
    }
}
