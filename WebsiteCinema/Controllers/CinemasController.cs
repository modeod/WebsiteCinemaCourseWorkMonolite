using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebsiteCinema.Data;
using WebsiteCinema.Data.Base;
using WebsiteCinema.Data.Static;
using WebsiteCinema.Models;

namespace WebsiteCinema.Controllers
{
    [Route("Cinemas")]
    [Authorize(Roles = UserRoles.Admin)]
    public class CinemasController : Controller
    {
        private readonly IEntityBaseRepository<Cinema> _service;
        public CinemasController(IEntityBaseRepository<Cinema> service)
        {
            _service = service;
        }

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
            Cinema cinemaDetails = await _service.GetByIdAsync(id);

            if (cinemaDetails == null)
                return NotFound();

            return View(cinemaDetails);
        }

        [HttpGet("Create")]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([Bind("Logo,Name,Description")] Cinema cinema)
        {
            if (!ModelState.IsValid)
                return View(cinema);

            await _service.AddAsync(cinema);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            Cinema cinemaDetails = await _service.GetByIdAsync(id);

            if (cinemaDetails == null)
                return NotFound();

            return View(cinemaDetails);
        }

        [HttpPost("Edit/{id}")]
        public async Task<IActionResult> Edit(int id, Cinema cinema)
        {
            if (!ModelState.IsValid || id != cinema.Id)
                return View(cinema);

            await _service.UpdateAsync(id, cinema);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Cinema cinemaDetails = await _service.GetByIdAsync(id);

            if (cinemaDetails == null)
                return NotFound();

            return View(cinemaDetails);
        }

        [HttpPost("Delete/{id}"), ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Cinema cinemaDetails = await _service.GetByIdAsync(id);

            if (cinemaDetails == null)
                return NotFound();

            await _service.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
