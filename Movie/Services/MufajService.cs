using Movie.Models;

namespace Movie.Services
{
    public class MufajService
    {
        public static List<Mufaj> GetMufajok()
        {
            using (var context = new MovieContext())
            {
                try
                {
                    return context.Mufajs.ToList();
                }
                catch
                {
                    return new List<Mufaj>();
                }
            }
        }
    }
}
