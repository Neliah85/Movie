using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movie.Models;
using Newtonsoft.Json;

namespace Movie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RendezoController : ControllerBase
    {
        [HttpDelete("{id}")]
        //http?://localhost:xxxx/api/rendezo/id
        public IActionResult Delete(int id)
        {
            using (var context = new MovieContext())
            {
                try
                {
                    Rendezo rendezo = new Rendezo { Id = id };
                    context.Rendezos.Remove(rendezo);
                    context.SaveChanges();
                    return Ok("A rendező sikeresen törölve.");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpPut]
        //http?://localhost:xxxx/api/rendezo
        public async Task<IActionResult> Put([FromForm] string Json)
        {
            using (var context = new MovieContext())
            {
                try
                {
                    // Deszerializaljuk a rendezot tartalmazo JSON-t
                    Rendezo rendezo = JsonConvert.DeserializeObject<Rendezo>(Json);

                    // Megnezzuk, hogy letezik-e a rendezo az adatbazisban az id alapjan
                    var existingRendezo = await context.Rendezos.FindAsync(rendezo.Id);

                    if (existingRendezo != null)
                    {
                        // Frissitjuk a rendezo adatait
                        existingRendezo.Nev = rendezo.Nev;
                        existingRendezo.Nemzetiseg = rendezo.Nemzetiseg;
                        existingRendezo.SzulDatum = rendezo.SzulDatum;

                        // A rendezo frissitese az adatbazisban
                        context.Rendezos.Update(existingRendezo);
                        await context.SaveChangesAsync();

                        return Ok("A rendezo adatainak modosítasa sikeresen megtörtént.");
                    }
                    else
                    {
                        return NotFound("A megadott ID-val nem találtunk rendezőt.");
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }




        [HttpPost]
        //http?://localhost:xxxx/api/film
        public async Task<IActionResult> Post([FromForm] string Json)
        {
            using (var context = new MovieContext())
            {
                try
                {
                    Rendezo rendezo = JsonConvert.DeserializeObject<Rendezo>(Json); using (var ms = new MemoryStream())

                        rendezo.Id = 0;
                    context.Rendezos.Add(rendezo);
                    await context.SaveChangesAsync();
                    return Ok("Az új rendező felvétele sikeresen megtörtént.");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }
    }
}
