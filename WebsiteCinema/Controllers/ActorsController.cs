using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebsiteCinema.Data.Base;
using WebsiteCinema.Data.Services;
using WebsiteCinema.Data.Static;
using WebsiteCinema.Models;
using WebsiteCinema.Services;

namespace WebsiteCinema.Controllers
{
    
    [Route("Actors")]
    [Authorize(Roles = UserRoles.Admin)]
    public class ActorsController : Controller
    {
        private readonly IEntityBaseRepository<Actor> _service;
        private readonly ICharacherGeneratorApi _generatorApi;

        public ActorsController(IEntityBaseRepository<Actor> service, ICharacherGeneratorApi generatorApi)
        {
            _service = service;
            _generatorApi = generatorApi;
        }

        [HttpGet("/Actors/")]
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
            Actor actorDetails = await _service.GetByIdAsync(id);

            if (actorDetails == null)
                return NotFound();

            return View(actorDetails);
        }

        [HttpGet("Generate")]
        [AllowAnonymous]
        public async Task<IActionResult> Generate()
        {
            ActorApiModel actorFromApi = await _generatorApi.Generate();
            Actor actorForView = new Actor()
            {
                ProfilePicturURL = actorFromApi.url,
                FullName = actorFromApi.character,
                Bio = actorFromApi.quote
            };
            return View("~/Views/Actors/Create.cshtml", actorForView);
        }

        [HttpGet("Create")]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([Bind("FullName,ProfilePicturURL,Bio")] Actor actor)
        {
            if (!ModelState.IsValid)
                return View(actor);

            await _service.AddAsync(actor);

            return RedirectToAction(nameof(Index));
        }


        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            Actor actorDetails = await _service.GetByIdAsync(id);

            if (actorDetails == null)
                return NotFound();
            
            return View(actorDetails);
        }

        [HttpPost("Edit/{id}")]
        public async Task<IActionResult> Edit(int id, Actor actor)
        {
            if (!ModelState.IsValid || id != actor.Id)
                return View(actor);

            await _service.UpdateAsync(id, actor);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("Delete/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Delete(int id)
        {
            Actor actorDetails = await _service.GetByIdAsync(id);

            if (actorDetails == null)
                return NotFound();


            return View(actorDetails);
        }

        [HttpPost("Delete/{id}"), ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed (int id)
        {
            Actor actorDetails = await _service.GetByIdAsync(id);

            if (actorDetails == null)
                return NotFound();

            await _service.DeleteAsync(id);
            
            return RedirectToAction(nameof(Index));
        }
    }
}
