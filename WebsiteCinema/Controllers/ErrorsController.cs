using Microsoft.AspNetCore.Mvc;

namespace WebsiteCinema.Controllers
{
    [Route("Errors")]
    public class ErrorsController : Controller
    {
        [Route("{code:int}")]
        public IActionResult HandleError(int code)
        {
            if(code == 404)
                return View("~/Views/Shared/NotFound.cshtml");
            else
                return View("Error " + code);
        }
    }
}
