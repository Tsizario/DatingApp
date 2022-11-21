using Microsoft.AspNetCore.Mvc;

namespace DatingApp.WebApi.Controllers
{
    public class AppController : Controller
    {
        [Route("~/")]
        [HttpGet("home")]
        public IActionResult Home()
        {
            return View();
        }
    }
}
