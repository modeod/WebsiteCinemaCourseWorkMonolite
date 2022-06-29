using Microsoft.AspNetCore.Mvc;
using WebsiteCinema.WebAPI.Models;
using WebsiteCinema.WebAPI.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebsiteCinema.WebAPI.Controllers
{
    [Route("api/movies")]
    [ApiController]
    public class MoviesAPIController : ControllerBase
    {
        private readonly IMoviesService _service;

        public MoviesAPIController(IMoviesService service)
        {
            _service = service;
        }

        // GET: api/<MoviesAPIController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await _service.AllAsync(n => n.Cinema);
            return Ok(data);
        }

        // GET api/<MoviesAPIController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            Movie actorDetails = await _service.GetMovieByIdAsync(id);

            if (actorDetails == null)
                return NotFound();

            return Ok(actorDetails);
        }

        // GET api/<MoviesAPIController>/5
        [HttpGet("dropdown")]
        public async Task<IActionResult> GetDropdowns()
        {
            var dropdownsData = await _service.GetNewMovieDropdownsValues();
            return Ok(dropdownsData);
        }

        // POST api/<MoviesAPIController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] VMNewMovie movie)
        {
            await _service.AddNewMovieAsync(movie);
            return Ok();
        }

        // PUT api/<MoviesAPIController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] VMNewMovie movie)
        {
            if (id != movie.Id) return NotFound();
            await _service.UpdateMovieAsync(movie);
            return Ok();
        }
    }
}
