using Microsoft.AspNetCore.Mvc;
using Movie.Models;
using Movie.Services;
using System.Diagnostics;
using System.Dynamic;

namespace Movie.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult FilmekDTO()
        {
            return View(FilmService.GetFilmekDTO());
        }

        public IActionResult RendezoDTO()
        {
            return View(RendezoService.GetRendezoDTO());
        }

        public IActionResult BoritokepView(int id)
        {
            return View(FilmService.GetBoritokep(id));
        }

        public async Task<IActionResult> FilmKarbantartas(int id)
        {
            await Task.Delay(500);
            Film film = FilmService.GetFilm(id);
            if (film == null)
            {
                ViewBag.Film = new Film { Id = 0, IndexKep = new byte[0], Kep = new byte[0] };
            }
            else
            {
                ViewBag.Film = film;
            }
            ViewBag.Rendezok = RendezoService.GetRendezok();
            ViewBag.Mufajok = MufajService.GetMufajok();
            return View(ViewBag);
        }

        public async Task<IActionResult> RendezoKarbantartas(int id)
        {
            await Task.Delay(500);
            Rendezo rendezo = RendezoService.GetRendezo(id);
            if (rendezo == null)
            {
                ViewBag.Rendezo = new Rendezo { Id = 0 };
            }
            else
            {
                ViewBag.Rendezo = rendezo;
            }
            ViewBag.Filmek = FilmService.GetFilmek();
            ViewBag.Mufajok = MufajService.GetMufajok();
            return View(ViewBag);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
