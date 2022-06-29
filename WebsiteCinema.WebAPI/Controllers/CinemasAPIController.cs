using Microsoft.AspNetCore.Mvc;
using WebsiteCinema.WebAPI.Base;
using WebsiteCinema.WebAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebsiteCinema.WebAPI.Controllers
{
    [Route("api/cinemas")]
    [ApiController]
    public class CinemasAPIController : ControllerBase
    {
        private readonly IEntityBaseRepository<Cinema> _service;

        public CinemasAPIController(IEntityBaseRepository<Cinema> service)
        {
            _service = service;
        }

        // GET: api/<CinemasAPIController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _service.AllAsync());
        }

        // GET api/<CinemasAPIController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            Cinema cinemaDetails = await _service.GetByIdAsync(id);

            if (cinemaDetails == null)
                return NotFound();

            return Ok(cinemaDetails);
        }

        // POST api/<CinemasAPIController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Cinema cinema)
        {
            await _service.AddAsync(cinema);

            return Created("", cinema);
        }

        // PUT api/<CinemasAPIController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Cinema cinema)
        {
            await _service.UpdateAsync(id, cinema);
            return Ok();
        }

        // DELETE api/<CinemasAPIController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Cinema cinemaDetails = await _service.GetByIdAsync(id);

            if (cinemaDetails == null)
                return NotFound();

            await _service.DeleteAsync(id);

            return Ok();
        }
    }
}
