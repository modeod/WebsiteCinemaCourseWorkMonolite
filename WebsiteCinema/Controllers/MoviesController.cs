using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebsiteCinema.Data;
using WebsiteCinema.Data.Base;
using WebsiteCinema.Data.Services;
using WebsiteCinema.Data.Static;
using WebsiteCinema.Models;

namespace WebsiteCinema.Controllers
{
    [Route("Movies")]
    [Authorize(Roles = UserRoles.Admin)]
    public class MoviesController : Controller
    {
        private readonly IMoviesService _service;

        public MoviesController(IMoviesService service)
        {
            _service = service;
        }

        [HttpGet("/")]
        [HttpGet("/Movies/")]
        [HttpGet("/Movies/Index")]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var data = await _service.AllAsync(n => n.Cinema);
            return View(data);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Filter(string searchString)
        {
            var movies = await _service.AllAsync(n => n.Cinema);

            if (!string.IsNullOrEmpty(searchString))
            {
                var filteredResult = movies
                    .Where(n => n.Name.ToLower().Contains(searchString.ToLower()) || n.Description.ToLower().Contains(searchString.ToLower()))
                    .ToList();
                return View("Index", filteredResult);
            }

            return View("Index", movies);
        }

        [HttpGet("Details/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            Movie actorDetails = await _service.GetMovieByIdAsync(id);

            if (actorDetails == null)
                return NotFound();

            return View(actorDetails);
        }

        [HttpGet("Create")]
        public async Task<IActionResult> Create()
        {
            var dropdownsData = await _service.GetNewMovieDropdownsValues();

            ViewBag.Cinemas = new SelectList(dropdownsData.Cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(dropdownsData.Producers, "Id", "FullName");
            ViewBag.Actors = new SelectList(dropdownsData.Actors, "Id", "FullName");

            return View(new VMNewMovie());
        } 

        [HttpPost("Create")]
        public async Task<IActionResult> Create(VMNewMovie movie)
        {
            if (!ModelState.IsValid || movie.StartDate > movie.EndDate)
            {
                var dropdownsData = await _service.GetNewMovieDropdownsValues();

                ViewBag.Cinemas = new SelectList(dropdownsData.Cinemas, "Id", "Name");
                ViewBag.Producers = new SelectList(dropdownsData.Producers, "Id", "FullName");
                ViewBag.Actors = new SelectList(dropdownsData.Actors, "Id", "FullName");
                return View(movie);
            }



            await _service.AddNewMovieAsync(movie);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var movieDetails = await _service.GetMovieByIdAsync(id);
            if (movieDetails == null)
                return NotFound();

            var responce = new VMNewMovie()
            {
                Id = movieDetails.Id,
                Name = movieDetails.Name,
                Description = movieDetails.Description,
                Price = movieDetails.Price,
                StartDate = movieDetails.StartDate,
                EndDate = movieDetails.EndDate,
                ImageURL = movieDetails.ImageURL,
                MovieCategory = movieDetails.MovieCategory,
                CinemaId = movieDetails.CinemaId,
                ProducerId = movieDetails.ProducerId,
                ActorIds = movieDetails.Actor_Movies.Select(n => n.ActorId).ToList()
            };
            
            var dropdownsData = await _service.GetNewMovieDropdownsValues();
            ViewBag.Cinemas = new SelectList(dropdownsData.Cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(dropdownsData.Producers, "Id", "FullName");
            ViewBag.Actors = new SelectList(dropdownsData.Actors, "Id", "FullName");

            return View(responce);
        }

        [HttpPost("Edit/{id}")]
        public async Task<IActionResult> Edit(int id, VMNewMovie movie)
        {
            if (id != movie.Id) return NotFound();

            if (!ModelState.IsValid || movie.StartDate > movie.EndDate)
            {
                var dropdownsData = await _service.GetNewMovieDropdownsValues();

                ViewBag.Cinemas = new SelectList(dropdownsData.Cinemas, "Id", "Name");
                ViewBag.Producers = new SelectList(dropdownsData.Producers, "Id", "FullName");
                ViewBag.Actors = new SelectList(dropdownsData.Actors, "Id", "FullName");

                return View(movie);
            }

            await _service.UpdateMovieAsync(movie);
            return RedirectToAction(nameof(Index));
        }

    }
}
