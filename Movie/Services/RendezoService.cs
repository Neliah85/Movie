using Movie.Models;

namespace Movie.Services
{
    public class RendezoService
    {
        public static List<Rendezo> GetRendezok()
        {
            using(var context=new MovieContext())
            {
                try
                {
                    return context.Rendezos.ToList();
                }
                catch
                {
                    return new List<Rendezo>();
                }
            }
        }
    }
}
