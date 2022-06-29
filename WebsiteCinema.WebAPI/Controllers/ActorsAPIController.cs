using Microsoft.AspNetCore.Mvc;
using WebsiteCinema.WebAPI.Base;
using WebsiteCinema.WebAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebsiteCinema.WebAPI.Controllers
{
    [Route("api/actors")]
    [ApiController]
    public class ActorsAPIController : ControllerBase
    {
        private readonly IEntityBaseRepository<Actor> _service;

        public ActorsAPIController(IEntityBaseRepository<Actor> service)
        {
            _service = service;
        }

        // GET: api/<ActorsAPIController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _service.AllAsync();
            return Ok(result);
        }

        // GET api/<ActorsAPIController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            Actor actorDetails = await _service.GetByIdAsync(id);

            if (actorDetails == null)
                return NotFound();

            return Ok(actorDetails);
        }

        // POST api/<ActorsAPIController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Actor actor)
        {
            await _service.AddAsync(actor);
            return Created("", actor);
        }

        // PUT api/<ActorsAPIController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Actor actor)
        {
            await _service.UpdateAsync(id, actor);
            return Ok();
        }

        // DELETE api/<ActorsAPIController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Actor actorDetails = await _service.GetByIdAsync(id);

            if (actorDetails == null)
                return NotFound();

            await _service.DeleteAsync(id);
            return Ok();
        }
    }
}
