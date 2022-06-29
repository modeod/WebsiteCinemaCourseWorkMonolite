using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebsiteCinema.Data;
using WebsiteCinema.Data.Base;
using WebsiteCinema.Data.Static;
using WebsiteCinema.Models;

namespace WebsiteCinema.Controllers
{
    [Route("Producers")]
    [Authorize(Roles = UserRoles.Admin)]
    public class ProducersController : Controller
    {
        private readonly IEntityBaseRepository<Producer> _service;

        public ProducersController(IEntityBaseRepository<Producer> service)
        {
            _service = service;
        }

        //[HttpGet("Index")]
        [HttpGet("/{controller}/")]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var data = await _service.AllAsync();
            return View(data);
        }

        [HttpGet("Details/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            Producer producerDetails = await _service.GetByIdAsync(id);
            if(producerDetails == null) return NotFound();
            return View(producerDetails);
        }

        [HttpGet("Create")]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([Bind("FullName,ProfilePicturURL,Bio")] Producer producer)
        {
            if (!ModelState.IsValid)
                return View(producer);

            await _service.AddAsync(producer);

            return RedirectToAction(nameof(Index));
        }

        
        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            Producer producerDetails = await _service.GetByIdAsync(id);

            if (producerDetails == null)
                return NotFound();


            return View(producerDetails);
        }

        [HttpPost("Edit/{id}")]
        public async Task<IActionResult> Edit(int id, Producer producer)
        {
            if (!ModelState.IsValid || id != producer.Id)
                return View(producer);

            await _service.UpdateAsync(id, producer);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Producer producerDetails = await _service.GetByIdAsync(id);

            if (producerDetails == null)
                return NotFound();


            return View(producerDetails);
        }

        [HttpPost("Delete/{id}"), ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Producer producerDetails = await _service.GetByIdAsync(id);

            if (producerDetails == null)
                return NotFound();

            await _service.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }

    }
}
