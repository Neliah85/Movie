using Microsoft.EntityFrameworkCore;
using Movie.DTOs;
using Movie.Models;

namespace Movie.Services
{
    public class RendezoService
    {
        // Rendezők listázása
        public static List<Rendezo> GetRendezok()
        {
            using (var context = new MovieContext())
            {
                try
                {
                    var response = context.Rendezok.ToList();
                    return response;
                }
                catch (Exception ex)
                {
                    List<Rendezo> response = new List<Rendezo>();
                    response.Add(new Rendezo { Id = -1, Nev = ex.Message });
                    return response;
                }
            }
        }

        // Rendezők DTO listázása
        public static List<RendezoDTO> GetRendezokDTO()
        {
            using (var context = new MovieContext())
            {
                try
                {
                    var response = context.Rendezok.Select(r => new RendezoDTO
                    {
                        Id = r.Id,
                        Nev = r.Nev,
                        Nemzetiseg = r.Nemzetiseg,
                        Szuletesidatum = r.Szuletesidatum
                    }).ToList();
                    return response;
                }
                catch (Exception ex)
                {
                    List<RendezoDTO> response = new List<RendezoDTO>();
                    response.Add(new RendezoDTO { Id = -1, Nev = ex.Message });
                    return response;
                }
            }
        }

        // Egyedi rendező lekérdezése ID alapján
        public static Rendezo GetRendezo(int id)
        {
            using (var context = new MovieContext())
            {
                try
                {
                    var response = context.Rendezok.FirstOrDefault(r => r.Id == id);
                    return response;
                }
                catch
                {
                    return new Rendezo { Id = 0 };
                }
            }
        }

        // Egyedi rendező DTO lekérdezése ID alapján
        public static RendezoDTO? GetRendezoDTO(int id)
        {
            using (var context = new MovieContext())
            {
                try
                {
                    var response = context.Rendezok.Where(r => r.Id == id).Select(r => new RendezoDTO
                    {
                        Id = r.Id,
                        Nev = r.Nev,
                        Nemzetiseg = r.Nemzetiseg,
                        Szuletesidatum = r.Szuletesidatum
                    }).FirstOrDefault();
                    return response;
                }
                catch
                {
                    return null;
                }
            }
        }
    }
}
