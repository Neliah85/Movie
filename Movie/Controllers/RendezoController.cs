using Microsoft.AspNetCore.Mvc;

namespace Movie.Controllers
{
    public class RendezoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
