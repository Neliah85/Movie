using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movie.Models;
using Newtonsoft.Json;

namespace Movie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmController : ControllerBase
    {
        [HttpDelete("{id}")]
        //http?://localhost:xxxx/api/film/id
        public IActionResult Delete(int id)
        {
            using (var context = new MovieContext())
            {
                try
                {
                    Film film = new Film { Id = id };
                    context.Films.Remove(film);
                    context.SaveChanges();
                    return Ok("A film sikeresen törölve.");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpPut]
        //http?://localhost:xxxx/api/film
        public async Task<IActionResult> Put([FromForm] string Json, [FromForm] IFormFile indexKep, [FromForm] IFormFile kep)
        {
            using (var context = new MovieContext())
            {
                try
                {
                    Film film = JsonConvert.DeserializeObject<Film>(Json);
                    byte[] IndexKep;
                    using (var ms=new MemoryStream())
                    {
                        await indexKep.CopyToAsync(ms);
                        IndexKep= ms.ToArray();
                    }
                    byte[] Kep;
                    using (var ms = new MemoryStream())
                    {
                        await kep.CopyToAsync(ms);
                        Kep = ms.ToArray();
                    }
                    film.IndexKep = IndexKep;
                    film.Kep = Kep;
                    context.Films.Update(film);
                    await context.SaveChangesAsync();
                    return Ok("A film adatainak a módosítása sikeresen megtörtént.");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpPost]
        //http?://localhost:xxxx/api/film
        public async Task<IActionResult> Post([FromForm] string Json, [FromForm] IFormFile indexKep, [FromForm] IFormFile kep)
        {
            using (var context = new MovieContext())
            {
                try
                {
                    Film film = JsonConvert.DeserializeObject<Film>(Json);
                    byte[] IndexKep;
                    using (var ms = new MemoryStream())
                    {
                        await indexKep.CopyToAsync(ms);
                        IndexKep = ms.ToArray();
                    }
                    byte[] Kep;
                    using (var ms = new MemoryStream())
                    {
                        await kep.CopyToAsync(ms);
                        Kep = ms.ToArray();
                    }
                    film.Id = 0;
                    film.IndexKep = IndexKep;
                    film.Kep = Kep;
                    context.Films.Add(film);
                    await context.SaveChangesAsync();
                    return Ok("Az új film felvétele sikeresen megtörtént.");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }
    }
}
