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

        // Filmek list�z�sa DTO form�tumban
        public IActionResult FilmekDTO()
        {
            return View(FilmService.GetFilmekDTO());
        }

        // Bor�t�k�p megjelen�t�se adott film ID alapj�n
        public IActionResult BoritokepView(int id)
        {
            return View(FilmService.GetBoritokep(id));
        }

        // Film karbantart� n�zet
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

        // Rendez�k list�z�sa DTO form�tumban
        public IActionResult RendezokDTO()
        {
            return View(RendezoService.GetRendezokDTO());
        }

        // Egyedi rendez� megjelen�t�se ID alapj�n
        public IActionResult RendezoReszletek(int id)
        {
            var rendezo = RendezoService.GetRendezoDTO(id);
            if (rendezo == null)
            {
                return NotFound($"A(z) {id} azonos�t�j� rendez� nem tal�lhat�.");
            }
            return View(rendezo);
        }

        // Rendez�k karbantart� n�zet
        public async Task<IActionResult> RendezoKarbantartas(int id)
        {
            await Task.Delay(500);
            var rendezo = RendezoService.GetRendezo(id);
            if (rendezo == null)
            {
                ViewBag.Rendezo = new Rendezo { Id = 0 };
            }
            else
            {
                ViewBag.Rendezo = rendezo;
            }
            return View(ViewBag);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
