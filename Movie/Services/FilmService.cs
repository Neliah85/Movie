using Microsoft.EntityFrameworkCore;
using Movie.DTOs;
using Movie.Models;

namespace Movie.Services
{
    public class FilmService
    {
        //nagy és ronda lekérdezés
        public static List<Film> GetFilmek()
        {
            using (var context = new MovieContext())
            {
                try
                {
                    var response = context.Films.Include(f => f.Mufaj).Include(f => f.Rendezo).ToList();
                    return response;
                }
                catch (Exception ex)
                {
                    List<Film> response = new List<Film>();
                    response.Add(new Film { Id = -1, Cim = ex.Message });
                    return response;
                }
            }
        }

        public static List<FilmDTO> GetFilmekDTO()
        {
            using (var context = new MovieContext())
            {
                try
                {
                    var response = context.Films.Include(f => f.Mufaj).Include(f => f.Rendezo).Select(f => new FilmDTO
                    {
                        Id = f.Id,
                        Rendezo = f.Rendezo.Nev,
                        Cim = f.Cim,
                        Hossz = f.Hossz,
                        Mufaj = f.Mufaj.Nev,
                        OscarE = f.OscarE,
                        Korhatar = f.Korhatar,
                        IndexKep = f.IndexKep
                    }).ToList();
                    return response;
                }
                catch (Exception ex)
                {
                    List<FilmDTO> response = new List<FilmDTO>();
                    response.Add(new FilmDTO { Id = -1, Cim = ex.Message });
                    return response;
                }
            }
        }

        public static Boritokep GetBoritokep(int id)
        {
            using(var context = new MovieContext())
            {
                try
                {
                    var response=context.Films.Where(f=>f.Id==id).Select(f => new Boritokep { Kep=f.Kep}).ToList();
                    return response[0];
                }
                catch
                {
                    return null;
                }
            }
        }

        public static Film GetFilm(int id)
        {
            using (var context = new MovieContext())
            {
                try
                {
                    var response = context.Films.FirstOrDefault(f => f.Id == id);
                    return response;
                }
                catch
                {
                    return new Film { Id=0};
                }
            }
        }
    }
}
