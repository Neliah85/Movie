namespace Movie.DTOs
{
    public class FilmDTO
    {
        public int? Id { get; set; }
        public string? Cim { get; set; }
        public string? Rendezo { get; set; }
        public TimeSpan? Hossz { get; set; }
        public string? Mufaj { get; set; }
        public bool OscarE { get; set; }
        public int? Korhatar { get; set; }
        public byte[]? IndexKep { get; set; }
    }
}
