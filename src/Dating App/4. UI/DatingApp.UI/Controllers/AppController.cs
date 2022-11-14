using Microsoft.AspNetCore.Mvc;

namespace DatingApp.WebApi.Controllers
{
    [Route("[controller]/")]
    public class AppController : Controller
    {
        [HttpGet("start")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
