using Microsoft.AspNetCore.Mvc;

namespace DatingApp.WebApi.Controllers
{
    [Route("[controller]/")]
    public class AppController : Controller
    {
        [HttpGet("Start")]
        public IActionResult Start()
        {
            return View();
        }
    }
}
