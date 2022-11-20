using Microsoft.AspNetCore.Mvc;

namespace DatingApp.WebApi.Controllers
{
    public class AppController : Controller
    {
        [HttpGet("home")]
        public IActionResult Home()
        {
            return View();
        }
    }
}
