using Microsoft.AspNetCore.Mvc;
using WebsiteCinema.WebAPI.Base;
using WebsiteCinema.WebAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebsiteCinema.WebAPI.Controllers
{
    [Route("api/producers")]
    [ApiController]
    public class ProducersAPIController : ControllerBase
    {
        private readonly IEntityBaseRepository<Producer> _service;

        public ProducersAPIController(IEntityBaseRepository<Producer> service)
        {
            _service = service;
        }

        // GET: api/<ProducersAPIController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await _service.AllAsync();
            return Ok(data);
        }

        // GET api/<ProducersAPIController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            Producer producerDetails = await _service.GetByIdAsync(id);
            if (producerDetails == null) return NotFound();
            return Ok(producerDetails);
        }

        // POST api/<ProducersAPIController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Producer producer)
        {
            await _service.AddAsync(producer);
            return Created("", producer);
        }

        // PUT api/<ProducersAPIController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Producer producer)
        {
            await _service.UpdateAsync(id, producer);
            return Ok();
        }

        // DELETE api/<ProducersAPIController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Producer producerDetails = await _service.GetByIdAsync(id);

            if (producerDetails == null)
                return NotFound();

            await _service.DeleteAsync(id);

            return Ok();
        }
    }
}
